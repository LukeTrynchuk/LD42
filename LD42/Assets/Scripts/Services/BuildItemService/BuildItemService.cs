using UnityEngine;
using RoboCorp.Services.General;
using RoboCorp.Construction;
using RoboCorp.Core.Services;

namespace RoboCorp.Services
{
    /// <summary>
    /// Build item service.
    /// </summary>
    public class BuildItemService : RegisterManager<BuildItem>, IBuildItemService
    {
        #region Main Methods    
        void Awake() => RegisterService();

        public void RegisterService()
        {
            ServiceLocator.Register<IBuildItemService>(this);
        }

        public void SetCurrentSelectedItem(BuildItem buildItem)
        {
            foreach(BuildItem item in m_valueList)
            {
                if (item == buildItem) continue;
                item.SetState(BuildItemState.UNSELECTED);
            }
        }
        #endregion
    }
}
