using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoboCorp.Core.Services;
using RoboCorp.Services;

public class PLayAudioOnStart : MonoBehaviour {

    private ServiceReference<ISoundManagerService> soundManager = new ServiceReference<ISoundManagerService>();
	// Use this for initialization

	void Start () {

        soundManager.AddRegistrationHandle(RegisterSoundManager);

        soundManager.Reference.Play("Music1");
	}

    private void RegisterSoundManager()
    {
        soundManager.Reference.RegisterService();
    }
	// Update is called once per frame
	void Update () {
		
	}
}
