using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerShooting : MonoBehaviour {

	private Camera cam;

	private float fireRate = 15f;
	private float nextTimeToFire = 0f;
	public GameObject Bullet;
	public Transform shootPoint;

	[SerializeField]
	private GameObject Bullet_smoke;

	// Use this for initialization
	void Start () {
		cam = Camera.main;

	}
	
	// Update is called once per frame
	void Update () {
		Shoot ();
	}

	void Shoot(){
		if (Input.GetMouseButtonDown (0) && Time.time > nextTimeToFire) {
			nextTimeToFire = Time.time + 1f / fireRate;

			RaycastHit hit;
			Debug.Log ("Shake");
			//cam.gameObject.GetComponent<PlayerViewController> ().UptiltCamera(50f);
			//Debug.DrawRay (cam.transform.position, cam.transform.forward, Color.yellow);

			if (Physics.Raycast (cam.transform.position, cam.transform.forward, out hit)) {
				Debug.Log ("shoot" + hit.collider.gameObject.name + hit.transform.position);

				//GameObject currentBullet = Instantiate (Bullet, shootPoint.position, shootPoint.rotation) as GameObject;
				//currentBullet.GetComponent<Rigidbody>().velocity = currentBullet.transform.forward * 50f;

				

				if (hit.transform.tag == "target") {
					Instantiate (Bullet_smoke, hit.point, Quaternion.LookRotation (hit.normal));
					//Invoke (instantiateSth (), 1f);
					hit.transform.gameObject.GetComponent<Renderer> ().material.color = Color.red;

				} else if (hit.transform.tag == "Reset"){
					SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);

				}else{
				Instantiate (Bullet_smoke, hit.point, Quaternion.LookRotation(hit.normal));
				//Instantiate(Bullet_smoke, hit.point, Quaternion.());
			}
			}
		}
	
	}
}
