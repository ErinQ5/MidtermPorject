using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {
	private Transform Player_View;
	private Transform Player_Camera;

	private Vector3 player_View_Rotation = Vector3.zero;
	private Vector3 moveDirection = Vector3.zero;

	private float speed;
	private float inputX, inputY;
	private float inputX_S, inputY_S;
	private float inputModify;
	private float antiBump = 0.75f;

	private CharacterController charController;

	private bool is_Grounded, is_Moving, is_Crouching;
	private bool limitDiagonalSpeed = true;

	public float speedWalk = 6.75f;
	public float speedRun = 10f;
	public float speedCrouch = 4f;
	public float speedJump = 8f;
	public float gravity = 20f;

	public LayerMask groundLayer;
	private float myRayDistance;
	private float controllerHeightDefault;
	private Vector3 cameraPositionDefault;
	private float cameraHeight;

	//public GameObject playerHolder, weaponHolder;
	//public GameObject[] weapons_FPS;
	private Camera mainCam;
	public PlayerViewController[] mouseLook;


	// Use this for initialization
	void Start () {
		Player_View = transform.Find ("View Handle").transform;
		charController = GetComponent<CharacterController> ();
		speed = speedWalk;
		is_Moving = false;

		myRayDistance = charController.height * 0.5f + charController.radius;
		controllerHeightDefault = charController.height;
		cameraPositionDefault = Player_View.localPosition;

		/*if(isLocalPlayer){
			playerHolder.layer = LayerMask.NameToLayer ("player");

			foreach (Transform child in playerHolder.transform) {
				child.gameObject.layer = LayerMask.NameToLayer ("player");
			}
		}

		if(!isLocalPlayer){
			playerHolder.layer = LayerMask.NameToLayer ("enemy");

			foreach(Transform child in playerHolder.transform){
				child.gameObject.layer = LayerMask.NameToLayer ("enemy");
			}
				
		}*/

		if(!isLocalPlayer){
			for (int i = 0; i < mouseLook.Length; i++){
				mouseLook [i].enabled = false;
			}
		}

		mainCam = transform.Find ("View Handle").Find ("Player Camera").GetComponent<Camera> ();
		mainCam.gameObject.SetActive (false);
	}

	public override void OnStartLocalPlayer(){
		tag = "Player";
	}
	
	// Update is called once per frame
	void Update () {

		if(isLocalPlayer){
			if(!mainCam.gameObject.activeInHierarchy){
				mainCam.gameObject.SetActive (true);
			}
		}

		if(!isLocalPlayer){
			return;
		}
		PlayerMove ();
	}

	void PlayerMove(){
		if ((Input.GetKey (KeyCode.W)) || (Input.GetKey (KeyCode.S))) {
			if (Input.GetKey (KeyCode.W)) {
				inputY_S = 1f;
			} else {
				inputY_S = -1f;
			}
		} else {
			inputY_S = 0f;
		}

		if ((Input.GetKey (KeyCode.A)) || (Input.GetKey (KeyCode.D))) {
			if (Input.GetKey (KeyCode.A)) {
				inputX_S = -1f;
			} else {
				inputX_S = 1f;
			}
		} else {
			inputX_S = 0f;
		}

		inputY = Mathf.Lerp (inputY, inputY_S, 19f * Time.deltaTime);
		inputX = Mathf.Lerp (inputX, inputX_S, 19f * Time.deltaTime);

		inputModify = Mathf.Lerp(inputModify,
			(inputY_S != 0 && inputX_S != 0 && limitDiagonalSpeed) ? 0.75f : 1.0f,
			19f * Time.deltaTime); //make sure to normalize it

		player_View_Rotation = Vector3.Lerp (player_View_Rotation, Vector3.zero, 5f * Time.deltaTime);
		Player_View.localEulerAngles = player_View_Rotation;

		if(is_Grounded){

			PlayerCrouchRun ();
			moveDirection = new Vector3 (inputX * inputModify, -antiBump, inputY * inputModify);
			moveDirection = transform.TransformDirection (moveDirection) * speed;
			PlayerJump ();
		}
			
		moveDirection.y -= gravity * Time.deltaTime;

		/*if((charController.collisionFlags == CollisionFlags.Below) && (charController.Move(moveDirection * Time.deltaTime) != 0)){
			is_Grounded = true;	
		}

		if(charController.velocity.magnitude > 0.15f){
			is_Moving = true;
		}*/

		is_Grounded = (charController.Move (moveDirection * Time.deltaTime) & CollisionFlags.Below) != 0;
		is_Moving = charController.velocity.magnitude > 0.15f;
	}

	void PlayerCrouchRun(){
		if(Input.GetKeyDown(KeyCode.C)){
			if(!is_Crouching){
				is_Crouching = true;

			} else {
				if (StandUp()){
					is_Crouching = false;
		
				}
			}
		
			StopCoroutine (CameraCrouch ());
			StartCoroutine (CameraCrouch ());
		}

		if(is_Crouching){
			speed = speedCrouch;
		} else {
			if(Input.GetKey(KeyCode.LeftShift)){
				speed = speedRun;
			} else {
				speed = speedWalk;
			}
		}
	}

	bool StandUp(){
		Ray rayToGround = new Ray (transform.position, -transform.up);
		RaycastHit hitGroundData;
		Debug.DrawRay (transform.position, -transform.up, Color.yellow);
		if(Physics.Raycast(rayToGround, out hitGroundData, groundLayer)){
			
			return false;
		/*if(Physics.SphereCast(rayToGround, charController.radius + 0.05f, out hitGroundData, myRayDistance, groundLayer)){
			Debug.Log ("Step1");
			if(Vector3.Distance(transform.position, hitGroundData.point) < 2.3f){
				Debug.Log("Step2");
				return false;
			}*/
		}

		return true;
	}

	IEnumerator CameraCrouch(){

		if(is_Crouching){
			charController.height = controllerHeightDefault / 1.5f;
			cameraHeight = cameraPositionDefault.y / 1.5f;
		} else {
			charController.height = controllerHeightDefault;
			cameraHeight = cameraPositionDefault.y;
		}

		//charController.center = new Vector3 (0f, charController.height / 2f, 0f);

		while(Mathf.Abs(cameraHeight - Player_View.localPosition.y) > 0.01f){
			Player_View.localPosition = Vector3.Lerp (Player_View.localPosition,
				new Vector3 (cameraPositionDefault.x, cameraHeight, cameraPositionDefault.z),
				11f * Time.deltaTime);

			yield return null;
		}
	}

	void PlayerJump(){
		if(Input.GetKeyDown(KeyCode.Space)){
			Debug.Log ("SPACE");
			if(!is_Crouching){
				/*Debug.Log ("crouching");
				if(StandUp()){
					is_Crouching = false;

					StopCoroutine (CameraCrouch ());
					StartCoroutine (CameraCrouch ());

				} else {*/
					moveDirection.y = speedJump;
				//}
			}
		}
	}
}
