using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RoboCorp.Gameboard;

[RequireComponent(typeof(InputEntity))]
public class SetInputResourceOnStart : MonoBehaviour {

    [SerializeField]
    private GameObject startResource;
	// Use this for initialization
	void Start () {
        this.gameObject.GetComponent<InputEntity>().SetCurrentResource(startResource);
	}

}
