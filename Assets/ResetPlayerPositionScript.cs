using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerPositionScript : MonoBehaviour {

	public GameObject player;
	Vector3 initialPosition;
	// Use this for initialization
	void Start () {
		initialPosition = player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.R))
			player.transform.position = initialPosition;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{

			other.transform.position = initialPosition;

		}
	}
}
