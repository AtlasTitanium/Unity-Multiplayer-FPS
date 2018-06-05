using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {
	private Camera sceneCamera;
	void Start(){
		sceneCamera = Camera.main;
		if (sceneCamera != null){
			sceneCamera.gameObject.SetActive(false);
		}
	}
}
