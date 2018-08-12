using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using RoboCorp.Core.Services;
using RoboCorp.Services;

public class SoundManagerService : MonoBehaviour,ISoundManagerService {

    [SerializeField]
    private List<ScritableAudioObject> audioList = new List<ScritableAudioObject>();
    private List<AudioSource> audioSourceList = new List<AudioSource>();

    public void Play(string id)
    {
        ScritableAudioObject audio = FindObject(id);
        if (audio == null) return;

        AudioSource source = FindAvailibleAudiSource();
        if (source == null) return;

        source.clip = audio.clip;
        source.Play();
    }

    public void RegisterService()
    {
        ServiceLocator.Register<ISoundManagerService>(this);
    }

    // Use this for initialization
    void Start () {
        RegisterService();
        audioSourceList = gameObject.GetComponentsInChildren<AudioSource>().ToList();
	}

    private ScritableAudioObject FindObject(string id)
    {
        for(int i = 0;i<audioList.Count;i++)
        {
            if(id.Equals(audioList[i].id))
            {
                return audioList[i];
            }
        }
        return null;
    }
    private AudioSource FindAvailibleAudiSource()
    {
        for(int i = 0;i<audioSourceList.Count;i++)
        {
            if (!audioSourceList[i].isPlaying) return audioSourceList[i];
        }

        return null;
    }
	
}
