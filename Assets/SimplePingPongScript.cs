using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePingPongScript : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = new Vector3 (transform.position.x, Mathf.PingPong (Time.time, 2), transform.position.z);
	}
}
