using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable {

	public string[] dialogue;

	//dialogue[0] = "ass";
	public string NPCname;
	// Use this for initialization
	public override void Interact(){
		DialogueSystem.Instance.AddNewDialogue(dialogue, name);
		Debug.Log("interacting with NPC");
	}

	void Start () {
		
	}
	
	// Update is called once per frame

}
