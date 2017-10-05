using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour {

	public LayerMask collisionLayer;

	void OnCollisionEnter(Collision collision){
		Debug.Log ("222");
		if(collision.gameObject.tag == "player"){
			
			Debug.Log ("1111");
		}else{
			Destroy (gameObject, 0.1f);
		}


	}
}
