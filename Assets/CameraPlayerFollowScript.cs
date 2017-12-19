using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerFollowScript : MonoBehaviour {

	public GameObject player;
	public Vector3 pos;

	// Use this for initialization
	void Start () {
		pos = new Vector3 (player.transform.position.x - gameObject.transform.position.x, player.transform.position.y - gameObject.transform.position.y, player.transform.position.z - gameObject.transform.position.z);
		transform.LookAt (player.transform);
	}
	
	// Update is called once per frame
	void LateUpdate () {
		gameObject.transform.position = player.transform.position - pos;
	}
}
