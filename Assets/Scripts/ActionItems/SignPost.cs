using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignPost : ActionItem {

	public string[] dialogue;

	public override void Interact(){
		DialogueSystem.Instance.AddNewDialogue(dialogue, "sign");
		Debug.Log("interacting with Sign Post");
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per fram
}
