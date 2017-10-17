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
	// Use this for initialization
	void Start () {
		endText = EndTextObject.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		bedToPlayerDis = Vector3.Distance (thePlayer.position, transform.position);
		if(bedToPlayerDis <= 7){
			endText.text = "You Finally Fall Asleep :), to restart press B";
			if(Input.GetKeyDown(KeyCode.B)){
				LoadScene ("StartScene");
			}
		}
	}

	public void LoadScene(string SceneName){
		SceneManager.LoadScene (SceneName);
	}
}
