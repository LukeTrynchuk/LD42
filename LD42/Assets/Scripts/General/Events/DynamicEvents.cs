using UnityEngine;
using UnityEngine.Events;

namespace RoboCorp.General
{
    // These dynamic events can be used to
    // setup events in a serialized format
    // in the editor.

    [System.Serializable]
    public class DynamicBoolEvent : UnityEvent<bool> {}

    [System.Serializable]
    public class DynamicStringEvent : UnityEvent<string> {}

    [System.Serializable]
    public class DynamicVector2Event : UnityEvent<Vector2> {}

    [System.Serializable]
    public class DynamicVector3Event : UnityEvent<Vector3> {}
}
