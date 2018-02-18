using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

	public static Dictionary<string, List<TacticsMove>> units = new Dictionary<string, List<TacticsMove>>();
	public static Queue<string> turnKey = new Queue<string>();
	public static Queue<TacticsMove> turnTeam = new Queue<TacticsMove>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(turnTeam.Count == 0){
			InitTeamTurnQueue();
		}
	}

	static void InitTeamTurnQueue(){
		List<TacticsMove> teamList = units[turnKey.Peek()];

		foreach(TacticsMove unit in teamList){
			if(!unit.killed){
				turnTeam.Enqueue(unit);
			}
		}

		StartTurn();
	}

	static void StartTurn(){
		if(turnTeam.Count > 0){
			turnTeam.Peek().BeginTurn();
		}
	}

	public static void EndTurn(){
		TacticsMove unit = turnTeam.Dequeue();
		unit.EndTurn();

		if(turnTeam.Count > 0){
			StartTurn();
		}
		else{
			string team = turnKey.Dequeue();
			turnKey.Enqueue(team);
			InitTeamTurnQueue();
		}
	}

	public static void AddUnit(TacticsMove unit){
		List<TacticsMove> list;

		if(!units.ContainsKey(unit.tag)){
			list = new List<TacticsMove>();
			units[unit.tag] = list;

			if(!turnKey.Contains(unit.tag)){
				turnKey.Enqueue(unit.tag);
			}
		}
		else{
			list = units[unit.tag];
		}

		list.Add(unit);
	}


}
