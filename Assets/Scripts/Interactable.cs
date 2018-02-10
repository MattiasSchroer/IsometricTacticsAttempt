using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

	public UnityEngine.AI.NavMeshAgent playerAgent;	
	private bool hasInteracted;
	public virtual void MoveToInteraction(UnityEngine.AI.NavMeshAgent playerAgent){
		hasInteracted = false;
		this.playerAgent = playerAgent;
		playerAgent.stoppingDistance = 3f;
		playerAgent.destination = this.transform.position;

		//Interact();
	}

	public virtual void Interact(){
		Debug.Log("interacting with base class");
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(playerAgent != null && !playerAgent.pathPending && !hasInteracted){
			if(playerAgent.remainingDistance <= playerAgent.stoppingDistance){
				Interact();
				hasInteracted = true;
			}
		}
	}
}
