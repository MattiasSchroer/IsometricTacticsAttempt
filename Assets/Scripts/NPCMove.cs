using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMove : TacticsMove {

	GameObject target;

	// Use this for initialization
	void Start () {
		Init();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawRay(transform.position, transform.forward);

		if(killed){
			GetComponent<Renderer>().material.color = Color.black;
		}

		if(!turn || killed){//returns before unit can move if turn == false
			return;
		}

		if(!moving){
			FindNearestTarget();
			CalculatePath();
			FindSelectableTiles();
			actualTargetTile.target = true;
		}
		else{
			Move();
		}

		if(moveCount == moves){
			moveCount = 0;
			TurnManager.EndTurn();//todo: This will end the unit's turn when it is done moving, needs to change when combat is added
		}
	}

	void CalculatePath(){
		Tile targetTile = GetTargetTile(target);
		FindPath(targetTile); 
	}

	void FindNearestTarget(){
		GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");

		GameObject nearest = null;

		float distance = Mathf.Infinity;

		foreach(GameObject obj in targets){
			float d = Vector3.Distance(transform.position, obj.transform.position);
			
			if(d < distance){
				distance = d;
				nearest = obj;
			}
		}

		target = nearest;
	}	
}
