using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthShow : MonoBehaviour {
	
	public Text healthText;
	public PlayerHealth healthCurrent;
	public float myHealth;

	// Use this for initialization
	void Start () {
		healthCurrent = GetComponent<PlayerHealth> ();

	}
	
	// Update is called once per frame
	void Update () {
		myHealth = healthCurrent.health;
		healthText.text = myHealth.ToString();
	}
}
