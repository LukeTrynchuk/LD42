using UnityEngine;
using RoboCorp.Services.General;
using RoboCorp.Construction;
using RoboCorp.Core.Services;

namespace RoboCorp.Services
{
    /// <summary>
    /// The build item service is responsible
    /// for keeping track of all the build items
    /// available in the build item store.
    /// </summary>
    public class BuildItemService : RegisterManager<BuildItem>, IBuildItemService
    {
        #region Main Methods    
        void Awake() => RegisterService();
        public void RegisterService() => ServiceLocator.Register<IBuildItemService>(this);

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
