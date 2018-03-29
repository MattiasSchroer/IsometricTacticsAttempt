using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMove : TacticsMove {

	GameObject target;

	// Use this for initialization
	void Start () {
		Init();

		anim = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawRay(transform.position, transform.forward);

		// if(killed){
		// 	GetComponent<Renderer>().material.color = Color.black;
		// }

		CheckCover();

		if(!turn || killed){//returns before unit can move if turn == false
			return;
		}

		if(moveCount == 0){
			if(!moving){
				anim.Play("Run");

				FindNearestTarget();
				CalculatePath();
				FindSelectableTiles();
				actualTargetTile.target = true;

			}
			else{
				//anim.Play("Run");

				Move();
			}
		}
		else{
			GameObject[] goodGuys = getShootableTargets("Player");

			goodGuys[0].GetComponent<TacticsMove>().Shoot(transform.position, weapons[0]);

			moveCount++;
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
