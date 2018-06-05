using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Shooting : NetworkBehaviour {

	public GameObject bullet;
	public GameObject gunOrigin;
	public float bulletSpeed;
	void Update() {
		if(!isLocalPlayer){
			return;
		}

		if(Input.GetMouseButtonDown(0)){
			Cmd_UpdateHost(bullet,gunOrigin,bulletSpeed);
		}
	}

	[Command]
	void Cmd_UpdateHost(GameObject bulletC,GameObject gunOriginC,float bulletSpeedC){
		Rpc_UpdateClients(bulletC,gunOriginC,bulletSpeedC);
	}

	[ClientRpc]
	void Rpc_UpdateClients(GameObject bulletR,GameObject gunOriginR,float bulletSpeedR){
		var newBullet = Instantiate(bulletR, gunOriginR.transform.position, gunOrigin.transform.rotation);
		newBullet.GetComponent<Rigidbody>().AddForce(gunOriginR.transform.forward * bulletSpeedR);
	}
}
