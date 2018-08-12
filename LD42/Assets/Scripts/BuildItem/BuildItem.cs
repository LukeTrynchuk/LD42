﻿using UnityEngine;
using RoboCorp.Core.Services;
using RoboCorp.Services;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RoboCorp.Construction
{
    /// <summary>
    /// Build item is responsible for registering
    /// itself with the build item service.
    /// </summary>
    public class BuildItem : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        #region Private Variables
        [SerializeField]
        private GameObject m_representingBuildItem;

        [SerializeField]
        private Color m_unselectedColor;

        [SerializeField]
        private Color m_selectedColor;

		[SerializeField]
        private bool m_defaultBuildItem;
  
        private ServiceReference<IBuildItemService> m_buildItemService
                            = new ServiceReference<IBuildItemService>();

        private ServiceReference<IPlacementService> m_placementService 
                            = new ServiceReference<IPlacementService>();

        private BuildItemState m_state = BuildItemState.UNSELECTED;
        private Image m_image;
        #endregion

        #region Main Methods
        private void OnEnable()
        {
            m_buildItemService.AddRegistrationHandle(HandleBuildItemRegistered);
        }

		private void Start()
		{
            m_image = gameObject.GetComponentInChildren<Image>();
            SetDisplayColor();

            if(m_defaultBuildItem)
            {
                SetState(BuildItemState.SELECTED);
                m_placementService.Reference.SetCurrentPlacingEntity(m_representingBuildItem);
            }
		}

		private void OnDisable()
        {
            if(m_buildItemService.isRegistered())
            {
                m_buildItemService.Reference.Unregister(this);
            }
        }
		
        public void OnPointerDown(PointerEventData eventData)
		{
            Debug.Log("Selected");
            m_state = BuildItemState.SELECTED;
            SetDisplayColor();

            m_placementService.Reference?.SetCurrentPlacingEntity(m_representingBuildItem);
		}
		
		public void OnPointerUp(PointerEventData eventData) {}

        public void SetState(BuildItemState state)
        {
            m_state = state;
            SetDisplayColor();
        }
        #endregion

        #region Utility Methods
        private void HandleBuildItemRegistered()
        {
            m_buildItemService.Reference.Register(this);
        }

        private void SetDisplayColor()
        {
            m_image.color = (m_state == BuildItemState.SELECTED) ? m_selectedColor : m_unselectedColor;
        }
        #endregion
    }

    public enum BuildItemState
    {
        UNSELECTED,
        SELECTED
    }
}
