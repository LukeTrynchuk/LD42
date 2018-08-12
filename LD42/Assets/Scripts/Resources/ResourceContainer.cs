using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoboCorp.Resources
{
    /// <summary>
    /// A resource is a class that contains all
    /// the resources on one entity. 
    /// </summary>
    public class ResourceContainer 
    {
        private List<Resource> resourceList = new List<Resource>();
        public List<Resource> ResourceList => resourceList;

        public void TransferResource(ResourceContainer container)
        {
            if(!(ResourceList.Count>0)) return;
            container.TransferTo(ResourceList[0]);
            ResourceList.RemoveAt(0);
        }
        public void TransferTo(Resource resource)
        {
            resourceList.Add(resource);
        }
        
    }
}
