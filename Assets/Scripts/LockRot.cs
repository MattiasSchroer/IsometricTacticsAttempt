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
        float speed = 10;
        float rotSpeed = 30;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0, rotSpeed * Time.deltaTime, 0, Space.World);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0, -rotSpeed * Time.deltaTime,0, Space.World);
        }

        //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 45, lockPos);
    }
}
