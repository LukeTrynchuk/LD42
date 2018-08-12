using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoboCorp.Core.Services;
using RoboCorp.Services;


public class EnableDisableTick : MonoBehaviour {

    private ServiceReference<ITickService> tickService = new ServiceReference<ITickService>();

    private void OnEnable()
    {
        if (tickService.isRegistered()) tickService.Reference.DisableTick();
    }
    private void OnDisable()
    {
        if (tickService.isRegistered()) tickService.Reference.EnableTick();
    }
}
