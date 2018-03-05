using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : TacticsMove {



	// Use this for initialization
	void Start () {
		currentWeapon = 0;

		Init();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawRay(transform.position, transform.forward);

		CheckCover();

		if(Input.GetKey("1")){
			currentWeapon = 0;
		}

		if(Input.GetKey("2")){
			currentWeapon = 1;
		}

		for(int i = 0; i < weapons.Length; i++){
			if (i != currentWeapon){
				weapons[i].SetActive(false);
			}
			else{
				weapons[i].SetActive(true);
			}
		}


		if(moveCount >= moves){
			moveCount = 0;
			TurnManager.EndTurn();//todo: This will end the unit's turn when it is done moving, needs to change when combat is added
		}

		if(!turn){//returns before unit can move if turn == false
			return;
		}

		if(!moving){
			
			FindSelectableTiles();
			CheckMouse();
		}
		else{
			Move();
		}

	}

	void CheckMouse(){
		if(Input.GetMouseButtonDown(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			RaycastHit hit;
			if(Physics.Raycast(ray, out hit)){
				if(hit.collider.tag == "Tile"){
					Tile t = hit.collider.GetComponent<Tile>();

					if(t.selectable){
						//todo: move target
						MoveToTile(t);
					}
				}
				if(hit.collider.tag == "NPC"){
					//GameObject Target = hit.collider.GetComponent;

					NPCMove npc = hit.collider.GetComponent<NPCMove>();

					Debug.Log(currentWeapon);

					if(!npc.killed){
						//npc.Kill();
						npc.Shoot(transform.position, weapons[currentWeapon]);

						moveCount++;
					}
					if(moveCount == moves){
						moveCount = 0;
						TurnManager.EndTurn();//todo: This will end the unit's turn when it is done moving, needs to change when combat is added
					}
					//Destroy(npc);
				}
			}
		}
	}
}
