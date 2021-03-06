﻿using System;
using UnityEngine;
using RoboCorp.Core.Services;
using RoboCorp.Services;
using RoboCorp.Resources;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace RoboCorp.Gameboard
{
    /// <summary>
    /// The entity base class represents 
    /// any kind of object that can be
    /// placed, moved and manipulated 
    /// on the game board.
    /// </summary>
    public abstract class Entity : MonoBehaviour
    {
        #region Public Variable
        public ResourceContainer ResourcesContainer => resourceContainer;

        public Transform TransportTransform => m_transportTransform;
        #endregion

        #region Protected Variables
        [SerializeField]
        protected Entity m_backInput = null;
        [SerializeField]
        protected Entity m_forwardInput = null;
        [SerializeField]
        protected Entity m_leftInput = null;
        [SerializeField]
        protected Entity m_rightInput = null;
        [SerializeField]
        protected Entity m_backOutput = null;
        [SerializeField]
        protected Entity m_forwardOutput = null;
        [SerializeField]
        protected Entity m_leftOutput = null;
        [SerializeField]
        protected Entity m_rightOutput = null;
		
        protected Vector3 LeftPosition => gameObject.transform.position + gameObject.transform.TransformDirection(Vector3.left);
		protected Vector3 RightPosition => gameObject.transform.position + gameObject.transform.TransformDirection(Vector3.right);
		protected Vector3 BackPosition => gameObject.transform.position + gameObject.transform.TransformDirection(Vector3.back);
		protected Vector3 ForwardPosition => gameObject.transform.position + gameObject.transform.TransformDirection(Vector3.forward);
        protected ResourceContainer resourceContainer;
        #endregion

        #region PrivateVariables
        [Header("Visual Aids")]
        [SerializeField]
        private GameObject m_inputHelper;

        [SerializeField]
        private GameObject m_outputHelper;

        [SerializeField]
        private GameObject m_placementHelper;

        [Space]
        [Header("Settings")]
        [SerializeField]
        private InputOutputSetting m_inputOutputSettings;

		[SerializeField]
		private EntityResourceSetting m_resourceSetting;

        [Space]
        [Header("Miscellaneous")]
        [SerializeField]
        protected Transform m_transportTransform;

        private bool IsPlacing = false;

        private ServiceReference<IGameboardService> m_gameboardService = new ServiceReference<IGameboardService>();
        private ServiceReference<ITickService> m_tickService = new ServiceReference<ITickService>();
        #endregion

        #region Main Methods
        public virtual void Awake()
        {
            resourceContainer = new ResourceContainer(m_transportTransform.position);
        }
        public abstract void Tick();
        public abstract void Animate();

        public virtual void TickOutputs()
        {
            if (m_backOutput != null) m_backOutput.Tick();
            if (m_forwardOutput != null) m_forwardOutput.Tick();
            if (m_leftOutput != null) m_leftOutput.Tick();
            if (m_rightOutput != null) m_rightOutput.Tick();
        }

        public virtual void SetIsPlacing(bool placingValue)
        {
            IsPlacing = placingValue;
            m_placementHelper?.SetActive(IsPlacing);
            m_inputHelper?.SetActive(IsPlacing);
            m_outputHelper?.SetActive(IsPlacing);
        }

        public virtual void SetConnections()
        {
            AttemptConnectionLeft();
            AttemptConnectionRight();
            AttemptConnectionBack();
            AttemptConnectionForward();
        }

        public virtual bool PositionIsValidOutput(Vector3 position)
        {
            if (position == LeftPosition && m_inputOutputSettings.OutputLeft) return true;
            if (position == RightPosition && m_inputOutputSettings.OutputRight) return true;
            if (position == ForwardPosition && m_inputOutputSettings.OutputForward) return true;
            if (position == BackPosition && m_inputOutputSettings.OutputBack) return true;

            return false;
        }

        public virtual bool PositionIsValidInput(Vector3 position)
        {
            if (position == LeftPosition && m_inputOutputSettings.InputLeft) return true;
            if (position == RightPosition && m_inputOutputSettings.InputRight) return true;
            if (position == ForwardPosition && m_inputOutputSettings.InputForward) return true;
            if (position == BackPosition && m_inputOutputSettings.InputBack) return true;

            return false;
        }

        public virtual void MakeConnection(Entity entity, ConnectionType connectionType)
        {
            Vector3 position = entity.gameObject.transform.position;

            if(connectionType == ConnectionType.INPUT)
            {
                if(position == LeftPosition && m_inputOutputSettings.InputLeft)
                {
                    m_leftInput = entity;
                }

                if(position == RightPosition && m_inputOutputSettings.InputRight)
                {
                    m_rightInput = entity;
                }

                if(position == ForwardPosition && m_inputOutputSettings.InputForward)
                {
                    m_forwardInput = entity;
                }

                if(position == BackPosition && m_inputOutputSettings.InputBack)
                {
                    m_backInput = entity;
                }
            }

            if(connectionType == ConnectionType.OUTPUT)
            {
                if (position == LeftPosition && m_inputOutputSettings.OutputLeft)
                {
                    m_leftOutput = entity;
                }

                if (position == RightPosition && m_inputOutputSettings.OutputRight)
                {
                    m_rightOutput = entity;
                }

                if (position == ForwardPosition && m_inputOutputSettings.OutputForward)
                {
                    m_forwardOutput = entity;
                }

                if (position == BackPosition && m_inputOutputSettings.OutputBack)
                {
                    m_backOutput = entity;
                }
            }
        }

        public virtual void Setup()
        {
            m_gameboardService.Reference?.RegisterEntity(this);
            SetIsPlacing(false);
            SetConnections();
        }

        public virtual void TransportResource() {}

		private void OnDrawGizmos()
		{
            Handles.Label(transform.position + Vector3.up, resourceContainer.NewResourceList.Count.ToString());
            Handles.Label(transform.position + Vector3.up * 2, resourceContainer.OldResourceList.Count.ToString());
		}
		#endregion

		#region Utility Methods

		private void AttemptConnectionForward()
        {
            AttemptConnection(ForwardPosition, m_inputOutputSettings.InputForward, m_inputOutputSettings.OutputForward, ref m_forwardInput, ref m_forwardOutput);
        }

        private void AttemptConnectionBack()
        {
            AttemptConnection(BackPosition, m_inputOutputSettings.InputBack, m_inputOutputSettings.OutputBack, ref m_backInput, ref m_backOutput);
        }

        private void AttemptConnectionRight()
        {
            AttemptConnection(RightPosition, m_inputOutputSettings.InputRight, m_inputOutputSettings.OutputRight, ref m_rightInput, ref m_rightOutput);
        }

        private void AttemptConnectionLeft()
        {
            AttemptConnection(LeftPosition, m_inputOutputSettings.InputLeft, m_inputOutputSettings.OutputRight, ref m_leftInput, ref m_leftOutput);
        }

        private void AttemptConnection(Vector3 position, bool input, bool output,ref  Entity directionInputEntity, ref Entity directionOutputEntity)
        {
            if (!input && !output) return;

            Entity connectionEntity = m_gameboardService.Reference.GetEntityAt(position);
            if (connectionEntity == null) return;

            if(output) 
            {
                if(connectionEntity.PositionIsValidInput(gameObject.transform.position))
                {
					directionOutputEntity = connectionEntity;
					connectionEntity.MakeConnection(this, ConnectionType.INPUT);
                }
            }

            if(input)
            {
                if(connectionEntity.PositionIsValidOutput(gameObject.transform.position))
                {
                    directionInputEntity = connectionEntity;
                    connectionEntity.MakeConnection(this, ConnectionType.OUTPUT);
                }
            }
        }
        protected void DestroyResource()
        {
            for (int i = resourceContainer.OldResourceList.Count - 1; i >= 0; i--)
            {
                Destroy(resourceContainer.OldResourceList[i].gameObject);
            }
            resourceContainer.OldResourceList.Clear();
        }
        #endregion
    }

    public enum ConnectionType
    {
        INPUT,
        OUTPUT
    }
}
