using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoboCorp.Core.Services;
using RoboCorp.Services;

public class BeginTickOnStart : MonoBehaviour {

    ServiceReference<ITickService> tickService = new ServiceReference<ITickService>();

	// Use this for initialization
	void Start () {
        tickService.AddRegistrationHandle(RegisterTickStarter);
	}
	
    void RegisterTickStarter()
    {
        tickService.Reference.EnableTick();
    }

}
