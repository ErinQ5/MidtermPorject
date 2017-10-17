using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
	public float enemyHealth = 100f;
	public float playerAttackPower = 10f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(enemyHealth <= 0){
			Destroy (this.gameObject);
		}
	}
}
