using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBarControll : MonoBehaviour {
	public Slider healthSlider;
	public Transform thePlayer;
	public float currentHealth;
	public float maxHealth = 100f;
	// Use this for initialization
	void Start () {
		

		healthSlider.maxValue = maxHealth;
		healthSlider.minValue = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		currentHealth = thePlayer.GetComponent<PlayerHealth> ().playerHealth;
		healthSlider.value = currentHealth;
	}
}
