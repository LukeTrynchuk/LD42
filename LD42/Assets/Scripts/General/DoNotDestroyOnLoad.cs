using UnityEngine;

namespace RoboCorp.General
{
    /// <summary>
    /// Do not destroy on load will on awake
    /// make the current gameobject this script
    /// is attached to not be destroyed on load.
    /// </summary>
    public class DoNotDestroyOnLoad : MonoBehaviour
    {
		#region Main Methods
		private void Awake()
		{
            DontDestroyOnLoad(gameObject);
		}
		#endregion
	}
}
