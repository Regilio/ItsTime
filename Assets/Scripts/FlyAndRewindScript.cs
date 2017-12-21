using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyAndRewindScript : MonoBehaviour {

	public Vector3 force;
	List<Vector3> velocity;
	List<Vector3> angularVelocity;
	List<Vector3> position;
	List<Quaternion> rotation;

	public int maxRewindFrames = 300;
	public int currentFrame = 0;
	public Image barreDeTemps;
	public Text frameText;

	bool isRewinding = false;


	// Use this for initialization
	void Start () {
		velocity = new List<Vector3> (); 	
		angularVelocity = new List<Vector3> ();
		position = new List<Vector3> ();	
		rotation = new List<Quaternion> ();

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		frameText.text = currentFrame.ToString ();
		barreDeTemps.GetComponent<RectTransform> ().localScale = new Vector3((float)currentFrame / (float)maxRewindFrames, 1,1);
		if (Input.GetMouseButtonDown (0)) {
			if(gameObject.GetComponent<Rigidbody> ().velocity == new Vector3(0,0,0)){
				gameObject.GetComponent<Rigidbody> ().AddForce (force);
			}
		}

		if (Input.GetMouseButtonDown (1)) {
			if (isRewinding) {
				isRewinding = false;
				gameObject.GetComponent<Rigidbody> ().velocity = velocity [currentFrame-1];
				gameObject.GetComponent<Rigidbody> ().angularVelocity = angularVelocity [currentFrame-1];
				gameObject.GetComponent<Transform> ().position = position [currentFrame-1];
				gameObject.GetComponent<Transform> ().rotation = rotation [currentFrame-1];
			} else {
				isRewinding = true;
			}
		}

		if (!isRewinding) {
			velocity.Add (gameObject.GetComponent<Rigidbody> ().velocity);
			angularVelocity.Add (gameObject.GetComponent<Rigidbody> ().angularVelocity);
			position.Add (gameObject.GetComponent<Transform> ().position);
			rotation.Add (gameObject.GetComponent<Transform> ().rotation);
			if(currentFrame < maxRewindFrames) {
				currentFrame++;
			}
			if (currentFrame > maxRewindFrames) {
				velocity.RemoveAt (0);			
				angularVelocity.RemoveAt (0);
				position.RemoveAt (0);			
				rotation.RemoveAt (0);
			} 

		} else {
			currentFrame--;
			Debug.Log (currentFrame);
			if (currentFrame > 1) {
				gameObject.GetComponent<Transform> ().position = position [currentFrame];
				gameObject.GetComponent<Transform> ().rotation = rotation [currentFrame];

				velocity.RemoveAt (currentFrame);			
				angularVelocity.RemoveAt (currentFrame);
				position.RemoveAt (currentFrame);			
				rotation.RemoveAt (currentFrame);	
			} else {
				isRewinding = false;
				gameObject.GetComponent<Rigidbody> ().velocity = velocity [currentFrame];
				gameObject.GetComponent<Rigidbody> ().angularVelocity = angularVelocity [currentFrame];
				gameObject.GetComponent<Transform> ().position = position [currentFrame];
				gameObject.GetComponent<Transform> ().rotation = rotation [currentFrame];
			}
		}
	}
}
