using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

	public float playerHealth = 100f;
	public float enemyAttackPower = 15f;
	public Transform DiedText;
	// Use this for initialization

	void Update(){
		Debug.Log (transform.position.y);
		if(transform.position.y < -50){
			Debug.Log ("falling");
			SceneManager.LoadScene ("StartScene");
		}
	}

	void OnCollisionEnter(Collision col){
		if(col.collider.tag == "enemy"){
			Debug.Log ("The enemy Hit you");
			playerHealth -= enemyAttackPower;
		}
		if(playerHealth <= 0f){
			
			DiedText.GetComponent<Text>().text = "You Died";
			SceneManager.LoadScene ("StartScene");
			Invoke ("LoadStartScene", 1f);
			Destroy (gameObject);

		}
	}


	public void LoadStartScene(){
		SceneManager.LoadScene ("StartScene");

	}
}
