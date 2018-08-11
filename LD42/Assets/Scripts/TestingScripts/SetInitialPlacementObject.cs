using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoboCorp.Services;
using RoboCorp.Core.Services;


public class SetInitialPlacementObject : MonoBehaviour {

    [SerializeField]
    private GameObject placementObject;

    private ServiceReference<IPlacementService> PlacementService = new ServiceReference<IPlacementService>();


	// Use this for initialization
	void Start () {
        PlacementService.AddRegistrationHandle(OnPlacementServiceRegistered);
	}
	
    void OnPlacementServiceRegistered()
    {
        PlacementService.Reference.SetCurrentPlacingEntity(placementObject);
    }
}
