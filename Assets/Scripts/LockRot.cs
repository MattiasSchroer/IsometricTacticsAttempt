using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockRot : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	float lockPos = 0;

	void Update()
	{
		transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 45, lockPos);
	}
}
