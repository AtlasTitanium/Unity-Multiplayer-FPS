using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class ChangeCicle : NetworkBehaviour {
	public Camera cam;
	public GameObject cirvle;
	// Update is called once per frame
	void Start(){
		if (!isLocalPlayer)
        {
            return;
        }
	}
	void Update () {
		RaycastHit hit;
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit)){
			if(hit.transform.tag != "Bullet"){
				cirvle.transform.localPosition = new Vector3(0,0,hit.distance + 1f);
			}
        } else {
			//Debug.Log("nothing found");
			cirvle.transform.localPosition = new Vector3(0,0,100);
		}
	}
}
