using UnityEngine;

namespace RoboCorp.Resources
{
    /// <summary>
    /// The Entity Resource setting is a scriptable object
    /// that contains the settings for a particular entity
    /// in regards to resources.
    /// </summary>
    [CreateAssetMenu(menuName = "Entity/Resource Setting", fileName = "NewResourceService")]
    public class EntityResourceSetting : ScriptableObject 
    {
        public int NumberOfResourcesAtOnce;
    }
}
