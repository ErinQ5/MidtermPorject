using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
	public float toPlayerDistance;
	public float enemyLookRange = 20f;
	public float rayAttackDistance = 30f;
	public float enemySpeed = 15f;
	public float damping = 5f;
	public Transform thePlayer;
	Rigidbody rig;
	//Renderer myrender;
	//static Animator GameBoyMoveAnim;

	// Use this for initialization
	void Start () {
		//myrender = GetComponent<Renderer> ();
		rig = GetComponent<Rigidbody> ();
		//GameBoyMoveAnim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		RaycastHit enemyHit;
		Debug.DrawRay (transform.position, transform.forward * rayAttackDistance, Color.red);
		toPlayerDistance = Vector3.Distance (thePlayer.position, transform.position);
		if (toPlayerDistance < enemyLookRange) {
			//myrender.material.color = Color.cyan;
			turnToPlayer ();
			this.transform.GetComponent<Animator>().SetTrigger ("isMoving");
			Debug.Log ("I SEE YOU!");
			enemyAttackFollow ();
			/*if (Physics.Raycast (transform.position, transform.forward * rayAttackDistance, out enemyHit)) {
				if(enemyHit.transform.tag == "Player"){
					enemyAttackFollow ();
					//GetComponent<Animator>().SetTrigger ("isMoving");
					Debug.Log ("ATTACK");
				}
			
			}*/
		}

	}

	void turnToPlayer(){
		Vector3 dir = (thePlayer.position - transform.position).normalized;
		dir.y = 0;
		Quaternion rot = Quaternion.LookRotation (dir);
		transform.rotation = Quaternion.Slerp (transform.rotation, rot, damping * Time.deltaTime);
	}

	void enemyAttackFollow(){
		transform.position = Vector3.Lerp (transform.position, thePlayer.position, Time.deltaTime );
	}
}
