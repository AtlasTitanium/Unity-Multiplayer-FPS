using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : NetworkBehaviour {
	[SerializeField]
	private Camera cam;
	private Vector3 velocity = Vector3.zero;
	private Vector3 rotation = Vector3.zero;
	private Vector3 cameraRotation = Vector3.zero;
	private Rigidbody rb;
	private bool isGrounded;
	public float viewRange = 50f;
	void Start () {
		if (!isLocalPlayer)
        {
            return;
        }
		rb = GetComponent<Rigidbody>();
	}

	void Update(){
		if(isLocalPlayer){
			cam.enabled = true;
		}
	}

	public void Move(Vector3 velocities){
		velocity = velocities;
	}
	public void Rotate(Vector3 rotationes){
		rotation = rotationes;
	}
	public void RotateCamera(Vector3 cameraRotationes){
		cameraRotation = cameraRotationes;
	}
	void FixedUpdate () {
		PerformMovement();
		PerformRotation();
	}

	void PerformMovement(){
		if (velocity != Vector3.zero){
			rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
		}
	}
	void PerformRotation(){
		if(rb != null){
			rb.MoveRotation(rb.rotation * Quaternion.Euler (rotation));
		}
		if (cam != null){
			cam.transform.Rotate(-cameraRotation);
			//Debug.Log("cam x rotation: " + cam.transform.localEulerAngles.x);
			if(cam.transform.localEulerAngles.x < 360 - viewRange && cam.transform.localEulerAngles.x > 350 - viewRange )
			{
				cam.transform.localEulerAngles = new Vector3(360 - viewRange, 0, 0);
			}

			if(cam.transform.localEulerAngles.x > viewRange && cam.transform.localEulerAngles.x < viewRange + 10)
			{
				cam.transform.localEulerAngles = new Vector3(viewRange, 0, 0);
			} 
		}
	}

	public void Jump(float jumpSpeed){
		RaycastHit hit;

        if (Physics.Raycast(transform.position, -Vector3.up, out hit)){
			if(hit.distance < 1.2){
				rb.AddForce(new Vector3(0, jumpSpeed, 0), ForceMode.Impulse);
			}
		}
	}
}
