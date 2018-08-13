using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoboCorp.Resources;

namespace RoboCorp.Gameboard
{
    /// <summary>
    /// The Processor Entity is a processor 
    /// that will process a resource into something
    /// of higher value.
    /// </summary>
    public class ProcessorEntity : Entity
    {
        #region Private Variables
        [SerializeField]
        private GameObject m_refinedMetalPrefab;

        [SerializeField]
        private GameObject m_refinedPlasticPrefab;

        [SerializeField]
        private GameObject m_refinedUraniumPrefab;
        #endregion

        #region Main Methods
        public override void Animate() { }

        public override void Tick()
        {
            TransportResource();
            Animate();
        }

        public override void TransportResource()
        {
            if (m_forwardOutput == null)
            {
                if (resourceContainer.OldResourceList.Count <= 0) return;
                DestroyResource();
                return;
            }

            if (resourceContainer.OldResourceList.Count > 0)
            {
                RefineResources();
            }

            resourceContainer.TransferResource(m_forwardOutput.ResourcesContainer);
            m_forwardOutput.TransportResource();
        }
        #endregion

        #region Utility Methods
        private void RefineResources()
        {
            GameObject refinedResource;

            if(resourceContainer.OldResourceList[0].GetComponent<Resource>() as Metal_Resource)
            {
                refinedResource = Instantiate(m_refinedMetalPrefab);
                refinedResource.transform.position = resourceContainer.OldResourceList[0].transform.position;
                refinedResource.transform.rotation = resourceContainer.OldResourceList[0].transform.rotation;
                refinedResource.GetComponent<Resource>().SetTargetPosition(m_transportTransform.position);
                GameObject oldResource = resourceContainer.OldResourceList[0].gameObject;
                resourceContainer.OldResourceList[0] = refinedResource.GetComponent<Resource>();
				Destroy(oldResource);
            }
             
            if (resourceContainer.OldResourceList[0].GetComponent<Resource>() as Plastic_Resource)
            {
                refinedResource = Instantiate(m_refinedPlasticPrefab);
                refinedResource.transform.position = resourceContainer.OldResourceList[0].transform.position;
                refinedResource.transform.rotation = resourceContainer.OldResourceList[0].transform.rotation;
                refinedResource.GetComponent<Resource>().SetTargetPosition(m_transportTransform.position);
                Destroy(resourceContainer.OldResourceList[0]);
                resourceContainer.OldResourceList[0] = refinedResource.GetComponent<Resource>();
            }

            if (resourceContainer.OldResourceList[0].GetComponent<Resource>() as Uranium_Resource)
            {
                refinedResource = Instantiate(m_refinedUraniumPrefab);
                refinedResource.transform.position = resourceContainer.OldResourceList[0].transform.position;
                refinedResource.transform.rotation = resourceContainer.OldResourceList[0].transform.rotation;
                refinedResource.GetComponent<Resource>().SetTargetPosition(m_transportTransform.position);
                Destroy(resourceContainer.OldResourceList[0]);
                resourceContainer.OldResourceList[0] = refinedResource.GetComponent<Resource>();
            }
        }
        #endregion
    }
}
