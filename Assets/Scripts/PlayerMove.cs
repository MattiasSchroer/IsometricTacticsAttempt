using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : TacticsMove {

	// Use this for initialization
	void Start () {
		Init();
	}
	
	// Update is called once per frame
	void Update () {
		if(!moving){
			FindSelectableTiles();
			CheckMouse();
		}
		else{
			Move();
		}
	}

	void CheckMouse(){
		if(Input.GetMouseButtonUp(0)){
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
			}
		}
	}
}
