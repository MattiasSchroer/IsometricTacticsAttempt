using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextRot : MonoBehaviour {
    public Transform camera;
	// Use this for initialization
	void Start () {
        float startAngle = 0;
	}
    float lockPos = 0;
    // Update is called once per frame
    void Update()
    {
        /**float rotSpeed = 30;
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0, rotSpeed * Time.deltaTime, 0, Space.World);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0, -rotSpeed * Time.deltaTime, 0, Space.World);
        }*/
        transform.LookAt(camera);
    }
}
