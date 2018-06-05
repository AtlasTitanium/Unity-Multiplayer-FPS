using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class characterChanger : NetworkBehaviour {
	public Canvas canvas;
	public Button Hero1;
	public Button Hero2;
	private Shooting1 shootingScript;
	private PlayerMotor playerMotor;
	private PlayerController playerController;
	private ChangeCicle changeCicle;
	void Start () {
		if(!isLocalPlayer){
			return;
		}
		Hero1.onClick.AddListener(ChangeToHero1);
		Hero2.onClick.AddListener(ChangeToHero2);
		shootingScript = this.GetComponent<Shooting1>();
		playerMotor = this.GetComponent<PlayerMotor>();
		playerController = this.GetComponent<PlayerController>();
		changeCicle = this.GetComponent<ChangeCicle>();
	}

	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
        	Cursor.lockState = CursorLockMode.None;
			shootingScript.enabled = false;
			playerMotor.enabled = false;
			playerController.enabled = false;
			changeCicle.enabled = false;
			canvas.gameObject.SetActive(true);
		}
	}

	void ChangeToHero1(){
		shootingScript.WichHero = 1;
		shootingScript.enabled = true;
		playerMotor.enabled = true;
		playerController.enabled = true;
		changeCicle.enabled = true;
		CmdCanvasChange();
		Cursor.lockState = CursorLockMode.Locked;
	}
	void ChangeToHero2(){
		shootingScript.WichHero = 2;
		shootingScript.enabled = true;
		playerMotor.enabled = true;
		playerController.enabled = true;
		changeCicle.enabled = true;
		CmdCanvasChange();
		Cursor.lockState = CursorLockMode.Locked;
	}

	[Command]
    public void CmdCanvasChange(){   
        Rpc_CanvasChange();
    }

	[ClientRpc]
	void Rpc_CanvasChange(){
		canvas.gameObject.SetActive(false);
	}
}
