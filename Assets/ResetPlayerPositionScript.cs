using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerPositionScript : MonoBehaviour {

	public GameObject player;
	Vector3 initialPosition;
	GameObject[] manaPotions;
	public DockManagementScript dmScript;
	// Use this for initialization
	void Start () {
		initialPosition = player.transform.position;
		manaPotions = GameObject.FindGameObjectsWithTag ("Potion");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.R)) {
			player.transform.position = initialPosition;
			resetManaPotions ();
			dmScript.Play ();
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			other.transform.position = initialPosition;
			resetManaPotions ();
			dmScript.Play ();
		}
	}

	void resetManaPotions(){
		foreach (GameObject potion in manaPotions) {
			potion.SetActive (true);
		}
	}
}
