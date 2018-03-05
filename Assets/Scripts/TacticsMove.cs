using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticsMove : MonoBehaviour {

	int health = 15;

	public int playerNorthCover;
	public int playerEastCover;
	public int playerSouthCover;
	public int playerWeastCover;

	public bool killed;

	public bool turn = false;

	List<Tile> selectableTiles = new List<Tile>();
	GameObject[] tiles;

	Stack<Tile> path = new Stack<Tile>();
	public Tile currentTile;

	public bool moving = false;
	public int move = 5;
	public float jumpHeight = 2;
	public float moveSpeed = 2;

	Vector3 velocity = new Vector3();
	Vector3 heading = new Vector3();

	float halfHeight = 0;

	bool fallingDown = false;
	bool jumpingUp = false;
	bool movingEdge = false;
	Vector3 jumpTarget;
	public float jumpVelocity = 4.5f;

	public Tile actualTargetTile;


	public int moves = 2;
	public int moveCount = 0;

	protected void Init(){
		tiles = GameObject.FindGameObjectsWithTag("Tile");

		halfHeight = GetComponent<Collider>().bounds.extents.y;

		TurnManager.AddUnit(this);
	}

	public void GetCurrentTile(){
		currentTile = GetTargetTile(gameObject);
		currentTile.current = true;
	}

	public Tile GetTargetTile(GameObject target){
		RaycastHit hit;
		Tile tile = null;
		if(Physics.Raycast(target.transform.position, -Vector3.up, out hit, 1)){
			tile = hit.collider.GetComponent<Tile>();
		}
		return tile;
	}

	public void ComputeAdjacencyList(float jumpHeight, Tile target){
		foreach(GameObject tile in tiles){
			Tile t = tile.GetComponent<Tile>();
			t.FindNeighbors(jumpHeight, target);
		}
	}

	public void FindSelectableTiles(){
		ComputeAdjacencyList(jumpHeight, null);
		GetCurrentTile();

		Queue<Tile> process = new Queue<Tile>();

		process.Enqueue(currentTile);
		currentTile.visited = true;

		while(process.Count > 0){
			Tile t = process.Dequeue();

			selectableTiles.Add(t);
			t.selectable = true;

			if(t.distance < move){
				foreach(Tile tile in t.adjacencyList){
					if(!tile.visited){
						tile.parent = t;
						tile.visited = true;
						tile.distance = 1 + t.distance;
						process.Enqueue(tile);
					}
				}
			}
		}
	}

	public void MoveToTile(Tile tile){
		path.Clear();
		tile.target = true;
		moving = true;

		Tile next = tile;
		while(next != null){
			path.Push(next);
			next = next.parent;
		}
	}

	public void Move(){
		if(path.Count > 0){
			Tile t = path.Peek();
			Vector3 target = t.transform.position;

			target.y += halfHeight + t.GetComponent<Collider>().bounds.extents.y;

			if(Vector3.Distance(transform.position, target) >= 0.05f){

				bool jump = transform.position.y != target.y;

				if(jump){
					Jump(target);
				}
				else{
					CalculateHeading(target);
					SetHorizontalVelocity();
				}
				//locomotion
				transform.forward = heading;
				transform.position += velocity * Time.deltaTime;
			}
			else{
				transform.position = target;
				path.Pop();
			}
		}
		else{
			//todo: remove the selectable tiles

			moveCount++;

			RemoveSelectableTiles();
			moving = false;

			
		}
	}

	protected void RemoveSelectableTiles(){
		if(currentTile != null){
			currentTile.current = false;
			currentTile = null;
		}

		foreach(Tile tile in selectableTiles){
			tile.Reset();
		}

		selectableTiles.Clear();
	}

	void CalculateHeading(Vector3 target){
		heading = target - transform.position;
		heading.Normalize();
	}

	void SetHorizontalVelocity(){
		velocity = heading * moveSpeed;
	}

	void Jump(Vector3 target){
		if(fallingDown){
			FallDownward(target);
		}
		else if(jumpingUp){
			JumpUpward(target);
		}
		else if(movingEdge){
			MoveToEdge();
		}
		else{
			PrepareJump(target);
		}
	}

	void PrepareJump(Vector3 target){
		float targetY = target.y;
		target.y = transform.position.y;

		CalculateHeading(target);

		if(transform.position.y > targetY){
			fallingDown = false;
			jumpingUp = false;
			movingEdge = true;

			jumpTarget = transform.position + (target - transform.position) / 2f;
		}
		else{
			fallingDown = false;
			jumpingUp = true;
			movingEdge = false;

			velocity = heading * moveSpeed / 3f;

			float difference = targetY - transform.position.y;

			velocity.y = jumpVelocity * (0.5f + difference / 2.0f);
		}
	}

	void FallDownward(Vector3 target){
		velocity += Physics.gravity * Time.deltaTime;

		if(transform.position.y <= target.y){
			fallingDown = false;

			Vector3 p = transform.position;
			p.y = target.y;
			transform.position = p;

			velocity = new Vector3();
		}
	}

	void JumpUpward(Vector3 target){
		velocity += Physics.gravity * Time.deltaTime;

		if(transform.position.y > target.y){
			jumpingUp = false;
			fallingDown = true;
		}
	}

	void MoveToEdge(){
		if(Vector3.Distance(transform.position, jumpTarget) >= 0.05f){
			SetHorizontalVelocity();
		}
		else{
			movingEdge = false;
			fallingDown = true;

			velocity /= 3.0f;
			velocity.y = 1.5f;
		}
	}

	public void BeginTurn(){
		turn = true;
		moveCount = 0;
	}

	public void EndTurn(){
		turn = false;
	}

	protected Tile FindLowestF(List<Tile> list){
		Tile lowest = list[0];

		foreach(Tile t in list){
			if(t.f < lowest.f){
				lowest = t;
			}
		}

		list.Remove(lowest);

		return lowest;
	}

	protected Tile FindEndTile(Tile t){
		Stack<Tile> tempPath = new Stack<Tile>();

		Tile next = t.parent;
		while (next != null){
			tempPath.Push(next);
			next = next.parent;
		}

		if(tempPath.Count <= move){
			return t.parent;
		}

		Tile endTile = null;
		for(int i = 0; i <= move; i++){
			endTile = tempPath.Pop();
		}

		return endTile;
	}

	protected void FindPath(Tile target){
		ComputeAdjacencyList(jumpHeight, target);
		GetCurrentTile();

		List<Tile> openList = new List<Tile>();
		List<Tile> closedList = new List<Tile>();

		openList.Add(currentTile);
		currentTile.h = Vector3.Distance(currentTile.transform.position, target.transform.position);
		currentTile.f = currentTile.h;

		while(openList.Count > 0){
			Tile t = FindLowestF(openList);

			closedList.Add(t);

			if(t == target){
				//todo
				actualTargetTile = FindEndTile(t);
				MoveToTile(actualTargetTile);
				return;
			}

			foreach(Tile tile in t.adjacencyList){
				if(closedList.Contains(tile)){

				}
				else if(openList.Contains(tile)){
					float tempG = t.g + Vector3.Distance(tile.transform.position, t.transform.position);

					if(tempG < tile.g){
						tile.parent = t;

						tile.g = tempG;
						tile.f = tile.g + tile.h;
					}
				}
				else{
					tile.parent = t;

					tile.g = t.g + Vector3.Distance(tile.transform.position, t.transform.position);
					tile.h = Vector3.Distance(tile.transform.position, target.transform.position);
					tile.f = tile.g + tile.h;

					openList.Add(tile);
				}
			}
		}

		//todo - what if there is no path to the target tile?
		Debug.Log("Path not found");
	}

	public void Kill(){
		Debug.Log("GOT EM");
		killed = true;
	}

	public void Shoot(Vector3 p, int acc, int dam){
		if(p.x > transform.position.x){
			acc = acc - playerEastCover * 25;
		}else if(p.x < transform.position.x){
			acc = acc - playerWeastCover * 25;
		}else if(p.y > transform.position.y){
			acc = acc - playerNorthCover * 25;
		}else if(p.y < transform.position.y){
			acc = acc - playerSouthCover * 25;
		}

		int hitCheck = Random.Range(0,100);

		if(acc > hitCheck){
			health = health - dam;

			if(health <= 0){
				Kill();
			}
		}
	}

	public void CheckCover(){
		Tile CT;

		RaycastHit hit;

		if (Physics.Raycast(transform.position, -Vector3.up, out hit, 1.0f)){
			CT = hit.collider.GetComponent<Tile>(); 

			playerNorthCover = CT.northCover;
			playerEastCover = CT.eastCover;
			playerSouthCover = CT.southCover;
			playerWeastCover = CT.weastCover;
		}
	}
}
