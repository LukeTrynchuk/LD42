using UnityEngine;

namespace RoboCorp.Gameboard
{
    /// <summary>
    /// The Setup Entity on start component will setup
    /// an entity on start.
    /// </summary>
    [RequireComponent(typeof(Entity))]
    public class SetupEntityOnStart : MonoBehaviour
    {
		#region Main Methods
		private void Start()
		{
            Entity entity = gameObject.GetComponent<Entity>();
            entity.Setup();
		}
		#endregion
	}
}
