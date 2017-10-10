using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerHealth : NetworkBehaviour {

	[SyncVar]
	public float health = 100f;
	// Use this for initialization
	public void TakeDamage(float damage){
		if(!isServer){
			return;
		}

		health -= damage;
		print ("damage recevided");
		if(health <= 0f){
			Destroy (this.gameObject);
		}
	}
}
