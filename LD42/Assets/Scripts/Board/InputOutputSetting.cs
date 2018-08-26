using UnityEngine;

namespace RoboCorp.Gameboard
{
    /// <summary>
    /// Input Output Setting is a scriptable object 
    /// that can be created to set which sides the 
    /// input and output should be coming from for 
    /// each entity. 
    /// </summary>
    [CreateAssetMenu(menuName = "Entity/Input Output Setting", fileName = "NewInputOutputService")]
    public class InputOutputSetting : ScriptableObject
    {
        public bool InputBack;
        public bool InputForward;
        public bool InputLeft;
        public bool InputRight;

        public bool OutputBack;
        public bool OutputForward;
        public bool OutputLeft;
        public bool OutputRight;
    }
}
