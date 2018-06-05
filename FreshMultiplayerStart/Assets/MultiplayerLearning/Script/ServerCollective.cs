using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class ServerCollective : NetworkBehaviour {
    [SyncVar]
    public int AmountPlayers = 0;

	public void AddPlayer(){
		AmountPlayers += 1;
	}
}
