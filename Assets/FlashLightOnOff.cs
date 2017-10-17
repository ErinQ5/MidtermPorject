using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightOnOff : MonoBehaviour {
	public Light myFlashLight;
	// Use this for initialization
	void Start () {
		myFlashLight = GetComponent<Light> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.F)) {
			myFlashLight.enabled = !myFlashLight.enabled;
		}
	}
}