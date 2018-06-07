using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour {
	public int strength = 10;
	void Start(){
		if(!isLocalPlayer){
			return;
		}
	}
    void OnTriggerEnter(Collider collision){
		if(collision.gameObject != transform.parent){
			if(collision.gameObject.tag == "Player"){
				var hit = collision.gameObject;
				var health = hit.GetComponent<Health>();
				if (health  != null){
					health.TakeDamage(strength);
				}

				Destroy(gameObject);
			}	
			
			if(collision.gameObject.tag == "Untagged"){
				Destroy(gameObject);
			}
		}
	}
}
