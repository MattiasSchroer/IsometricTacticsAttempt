using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glock : WeaponStats {

	// Use this for initialization
	void Start () {
		dam = 15;
		acc = 0;
		magSize = 20;
		ammo = 20;
		crit = 10;
		rof = 2;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
