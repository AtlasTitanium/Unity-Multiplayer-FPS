using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Complains : NetworkBehaviour {
	private ServerCollective ServerSystem;
	public Text Textprefab;
	private InputField inputField;
	private InputField NameinputField;
	private Text ComplaintsText;
	private Canvas Canvas;
	private Color color = Color.blue;

	public int maxMessages = 25;

	public GameObject chatPanel, textObject;
	private bool InputInput = false;
	public int PlayerID;
	private string name;

	//public InputField chatBox;

	//public Color playerMessage, info;

	[SerializeField]
	List<Message> messageList = new List<Message>();
	void Start(){
		ServerSystem = GameObject.FindGameObjectWithTag("ServerSystem").GetComponent<ServerCollective>();
		PlayerID = ServerSystem.AmountPlayers;
		ServerSystem.AddPlayer();
		chatPanel = GameObject.FindGameObjectWithTag("TextField");
		chatPanel = GameObject.FindGameObjectWithTag("TextField");
		inputField = GameObject.FindGameObjectWithTag("InputField").GetComponent<InputField>();
		NameinputField = GameObject.FindGameObjectWithTag("NameInputField").GetComponent<InputField>();
		Canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
		//ComplaintsText = GameObject.FindGameObjectWithTag("TextField").GetComponent<Text>();
	}
	void FixedUpdate(){
		if(!isLocalPlayer){
			return;
		}

		if(isServer){
			color = Color.red;
		}

		if(inputField.isFocused == true){
			if(Input.GetKeyDown(KeyCode.Return)){
				//ComplaintsText.text = inputField.text;
				if(inputField.text != ""){Cmd_UpdateHost(inputField.text, color);	Debug.Log("Post");}
				inputField.text = "";
				inputField.DeactivateInputField();
				Debug.Log("DeactivateInputField");
			}
		}

		if(NameinputField.isFocused == true){
			if(Input.GetKeyDown(KeyCode.Return)){
				name = NameinputField.text;
				//ComplaintsText.text = inputField.text;
				if(NameinputField.text != ""){Cmd_UpdateName();	Debug.Log("Post");}
				NameinputField.text = "";
				NameinputField.DeactivateInputField();
				Debug.Log("DeactivateInputField");
			}
		}

		if(Input.GetKeyDown("t")){
			//if(inputField.isFocused == true){
				//inputField.DeactivateInputField();
			//} else {
			inputField.ActivateInputField();
			//}
			//Debug.Log("ActivateInputField");
		}
	}

	[Command]
	void Cmd_UpdateHost(string text, Color color){
		Rpc_UpdateClients(text,color);
	}

	[ClientRpc]
	void Rpc_UpdateClients(string text, Color color){
		if(messageList.Count >= maxMessages){
			Destroy(messageList[0].textObject.gameObject);
			messageList.Remove(messageList[0]);
		}
		Message newMessage = new Message();

		newMessage.text = text;

		GameObject newText = Instantiate(textObject, chatPanel.transform);

		newMessage.textObject = newText.GetComponent<Text>();

		newMessage.textObject.text = name + ": " + newMessage.text;
		
		newMessage.textObject.color = color;		

		messageList.Add(newMessage);


		/*
		Instantiate(Textprefab, inputField.gameObject.transform.localPosition, inputField.gameObject.transform.localRotation);
		Textprefab.transform.SetParent(Canvas.transform,true);
		Textprefab.gameObject.transform.localPosition = new Vector2(0,10);
		Textprefab.color = color;
		Textprefab.text = text;
		ComplaintsText.gameObject.SetActive(false);
		ComplaintsText.color = color;
		ComplaintsText.text = text;
		*/
	}

	[Command]
	void Cmd_UpdateName(){
		Rpc_SendName(name);
	}

	[ClientRpc]
	void Rpc_SendName(string name){
		transform.name = name;
	}
}

[System.Serializable]
		
public class Message{
	public string text;
	public Text textObject;
	public MessageType messageType;
	public enum MessageType{
		playerMessage,
		info
	}
}
