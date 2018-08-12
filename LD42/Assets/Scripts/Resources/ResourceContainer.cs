using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoboCorp.Core.Services;
using RoboCorp.Services;

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
        private List<Resource> oldResourceList = new List<Resource>();
        private Vector3 position;
        private ServiceReference<ITickService> tickService = new ServiceReference<ITickService>();
        public ResourceContainer(Vector3 startPosition)
        {
            position = startPosition;
            tickService.AddRegistrationHandle(RegisterTick);
        }
        ~ResourceContainer()
        {
            if(tickService.isRegistered())
            {
                tickService.Reference.OnTick -= OnTick;
            }
        }
        public void TransferResource(ResourceContainer container)
        {
            if(!(oldResourceList.Count>0)) return;
            container.TransferTo(oldResourceList[0]);
            oldResourceList.RemoveAt(0);
        }
        public void TransferTo(Resource resource)
        {
            resourceList.Add(resource);
        }
        private void RegisterTick()
        {
            tickService.Reference.OnTick -= OnTick;
            tickService.Reference.OnTick += OnTick;
        }
        private void OnTick()
        {
            foreach(Resource R in resourceList)
            {
                oldResourceList.Add(R);
            }
            resourceList.Clear();
        }
    }
}
