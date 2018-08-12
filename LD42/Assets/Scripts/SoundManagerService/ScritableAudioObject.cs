using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio/Object",fileName = "AudioClip")]
public class ScritableAudioObject : ScriptableObject {

    public string id;
    public AudioClip clip;

}
