using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoboCorp.Services;
using RoboCorp.Core.Services;

[RequireComponent(typeof(Camera))]

public class RegisterAsPlacementCamera : MonoBehaviour {

    private ServiceReference<IPlacementService> placementService = new ServiceReference<IPlacementService>();


    private void OnEnable()
    {
        placementService.AddRegistrationHandle(HandlePlacementRegistration);
    }
    private void OnDisable()
    {
        placementService.Reference?.UnregisterCamera(this.GetComponent<Camera>());
    }

    private void HandlePlacementRegistration()
    {
        placementService.Reference.RegisterCamera(this.GetComponent<Camera>());
    }
}
