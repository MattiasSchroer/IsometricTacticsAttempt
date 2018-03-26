using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mk18 : WeaponStats {

	// Use this for initialization
	void Start () {
		dam = 20;
		acc = 80;
		magSize = 30;
		ammo = 30;
		crit = 10;
		rof = 4;
		shootAnim = "ShootRifle";
		idleAnim = "Idle";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
