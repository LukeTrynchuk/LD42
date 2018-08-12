using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoboCorp.Core.Services;
using RoboCorp.Construction;

namespace RoboCorp.Services
{
    /// <summary>
    /// IBuild Item Serivce is a contract that all
    /// build item services must implement. A Build
    /// item service is responsible for setting
    /// the current build item of the placement
    /// 
    /// </summary>
    public interface IBuildItemService : IService
    {
        void Register(BuildItem item);
        void Unregister(BuildItem item);

        void SetCurrentSelectedItem(BuildItem buildItem);
    }
}
