using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Movement : NetworkBehaviour {
	private Rigidbody robbob;
	private Camera camera;
	public Transform cameraLookObj;
	public float speed = 1f;
	public float MouseSpeed = 1f;
	public float JumpSpeed = 1f;
	Vector3 m_EulerAngleVelocity;

	private float MouseXPos;
	private float MouseYPos;
	private float XPos;
	private float ZPos;

	public float MouseRange;
	
	void Start () {
		if(!isLocalPlayer){
			return;
		}
		MouseXPos = 0;
		MouseYPos = 0;
		XPos = 0;
		ZPos = 0;
		robbob = GetComponent<Rigidbody>();
		camera = GetComponentInChildren<Camera>();
		Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
	}
	

	void Update () {
		if(!isLocalPlayer){
			return;
		}
		float x = Input.GetAxis("Horizontal") * speed;
		float z = Input.GetAxis("Vertical") * speed;
		float Mousex = Input.GetAxis("Mouse X") * MouseSpeed;
		float Mousey = Input.GetAxis("Mouse Y") * MouseSpeed;

		if(Input.GetKeyDown(KeyCode.Space)){
			robbob.AddForce(Vector3.up * JumpSpeed * 100);
		}

		//XPos += x;
		//ZPos += z;
		//transform.position = new Vector3(XPos,transform.position.y,ZPos);

        //camera.transform.Rotate(-Mousey,Mousex,0);
		MouseXPos += Mousex;
		MouseYPos -= Mousey;

		if(MouseYPos <= -MouseRange){
			MouseYPos = -MouseRange;
		}
		if(MouseYPos >= MouseRange){
			MouseYPos = MouseRange;
		}

		camera.transform.rotation = Quaternion.Euler(MouseYPos, MouseXPos, 0);
		cameraLookObj.rotation = Quaternion.Euler(0, MouseXPos, 0);

		robbob.AddForce(cameraLookObj.forward * z, ForceMode.Force);
		robbob.AddForce(cameraLookObj.right * x, ForceMode.Force);
	}
}
