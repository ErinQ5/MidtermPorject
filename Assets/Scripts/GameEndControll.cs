using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameEndControll : MonoBehaviour {
	public Text endText;
	public Transform EndTextObject;
	public float bedToPlayerDis;
	public Transform thePlayer;
	public GameObject myLight;
	// Use this for initialization
	void Start () {
		endText = EndTextObject.GetComponent<Text> ();
	}

	void Update(){
		bedToPlayerDis = Vector3.Distance (thePlayer.position, transform.position);
		if(bedToPlayerDis <= 5){
			endText.text = "Finally Fall Asleep :), but itś already morning, to restart press B";
			myLight.transform.rotation = Quaternion.Euler (-185.87f, -30.109f, 1.18299f);
			if(Input.GetKeyDown(KeyCode.B)){
				LoadScene ("StartScene");
			}
		}
	}

	public void LoadScene(string SceneName){
		SceneManager.LoadScene (SceneName);
	}
}
