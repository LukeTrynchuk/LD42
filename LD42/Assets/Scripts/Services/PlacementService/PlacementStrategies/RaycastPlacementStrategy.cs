using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoboCorp.Services;
using RoboCorp.Core.Services;
using UnityEngine.SceneManagement;
using RoboCorp.Gameboard;

namespace RoboCorp.Services
{
    /// <summary>
    /// The Raycast placement strategy is 
    /// a placement strategy that uses
    /// the mouse position and raycast
    /// physics to place a object.
    /// </summary>
    public class RaycastPlacementStrategy : IPlace
    {
        #region Private Variables
        private float m_gridSize = 1f;
        private Camera m_rayCamera;
        private string CAMERA_TAG = "MainGameCamera";

        private ServiceReference<IInputService> m_inputService 
                    = new ServiceReference<IInputService>();
        #endregion

        #region Main Methods
        public RaycastPlacementStrategy()
        {
            SceneManager.sceneLoaded -= SceneLoaded;
            SceneManager.sceneLoaded += SceneLoaded;
        }

        ~RaycastPlacementStrategy()
        {
            SceneManager.sceneLoaded -= SceneLoaded;
        }

        public void Move(ref GameObject PlaceObject)
        {
            if (!m_inputService.isRegistered()) return;

            RaycastHit hit;
            Ray ray = m_rayCamera.ScreenPointToRay(m_inputService.Reference.GetPointerScreenPosition());

            if (Physics.Raycast(ray, out hit))
            {
                PlaceObject.transform.position = SnapToGrid(hit.point);
            }
        }

        public void Place(GameObject PlaceObject)
        {
            if (m_rayCamera == null) return;

            Entity entity = InstantiatePlaceObject(PlaceObject).GetComponent<Entity>();
            entity.Setup();
        }

        public void SetGridSize(float gridSize) => m_gridSize = gridSize;

        void SceneLoaded(Scene scene, LoadSceneMode mode) => FindRayCamera();
        #endregion

        #region Utility Methods
		private void FindRayCamera()
		{
			m_rayCamera = GameObject.FindWithTag(CAMERA_TAG)?.GetComponent<Camera>();
		}

        private Vector3 SnapToGrid(Vector3 initialPosition)
        {
            initialPosition.x -= initialPosition.x % m_gridSize;
            initialPosition.z -= initialPosition.z % m_gridSize;
            return initialPosition;
        }

        private GameObject InstantiatePlaceObject(GameObject prefab)
        {
            GameObject placedObject = GameObject.Instantiate(prefab);
            placedObject.transform.position = prefab.transform.position;
            placedObject.transform.rotation = prefab.transform.rotation;
            return placedObject;
        }
        #endregion
    }
}
