﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoboCorp.Services;
using RoboCorp.Core.Services;

public class PlayOnStart : MonoBehaviour {

    private ServiceReference<ISoundManagerService> soundManager = new ServiceReference<ISoundManagerService>();

	// Use this for initialization
	void Start () {
        soundManager.AddRegistrationHandle(RegisterSoundManager);
        soundManager.Reference.Play("Music1");
	}
	
    void RegisterSoundManager()
    {
        soundManager.Reference.RegisterService();
    }
	// Update is called once per frame
	void Update () {
		
	}
}
