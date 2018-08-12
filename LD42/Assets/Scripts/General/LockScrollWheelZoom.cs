using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockScrollWheelZoom : MonoBehaviour {

    public GameObject buildBanner;
    public Camera cam;
	// Use this for initialization
	void Start () {
        cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		if(buildBanner.active) cam.gameObject.GetComponent<RTS_Cam.RTS_Camera>().useScrollwheelZooming = false;

        else cam.gameObject.GetComponent<RTS_Cam.RTS_Camera>().useScrollwheelZooming = true;
    }
}
