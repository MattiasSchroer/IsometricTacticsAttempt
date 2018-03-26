using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Health : MonoBehaviour {

	public Text playerHealth;

	public void UpdateHealth(int health, int dam){
		playerHealth.text = "Health = " + health + "\n" + dam;

	}
}
