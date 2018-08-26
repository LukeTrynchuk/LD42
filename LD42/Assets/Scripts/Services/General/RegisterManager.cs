using System.Collections.Generic;
using UnityEngine;

namespace RoboCorp.Services.General
{
    /// <summary>
    /// Register manager is an abstract class
    /// that can be inherited by any class who's
    /// job is to keep track of a list of objects 
    /// who register and unregister themselves 
    /// from the register manager.
    /// </summary>
    public abstract class RegisterManager<T>  : MonoBehaviour where T : class
    {
        #region protected Variables
        protected List<T> m_valueList = new List<T>();
        #endregion

        #region Main Methods
        public void Register(T valueItem) => m_valueList.Add(valueItem);

        public void Unregister(T valueItem)
        {
            int indexToRemove = FindItemIndex(valueItem);
            if (indexToRemove == -1) return;
            RemoveItemFromListAtIndex(indexToRemove);
        }
        #endregion

        #region Utility Methods
        private int FindItemIndex(T valueItem)
        {
            for (int i = 0; i < m_valueList.Count; i++)
            {
                if (m_valueList[i] == valueItem) return i;
            }

            return -1;
        }

		private void RemoveItemFromListAtIndex(int indexToRemove)
		{
            m_valueList.RemoveAt(indexToRemove);
		}
        #endregion
    }
}
