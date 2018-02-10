using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MenuScript {

	//Material material;

	[MenuItem("Tools/Assign Tile Materlial")]
	public static void AssignTileMaterial(){
		GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
		Material material = Resources.Load<Material>("Tile");

		foreach(GameObject t in tiles){
			t.GetComponent<Renderer>().material = material;
		}
	}

	[MenuItem("Tools/Assign Tile Script")]
	public static void AssignTileScript(){
		GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");

		foreach(GameObject t in tiles){
			t.AddComponent<Tile>();
		}
	}
}
