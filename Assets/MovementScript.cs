using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;





public class MovementScript : MonoBehaviour {

	public float walkSpeed = 8.0f;
	public float runSpeed = 15.0f;

	Vector3 forward, right;
	private float moveSpeed;

	public GameObject jumpPoint;
	public float jumpForce = 220.0f;

	List<GameObject> ongoingEvents;

	// Use this for initialization
	void Start () {

		ongoingEvents = new List<GameObject> ();

		forward = gameObject.transform.forward;
		forward.y = 0;
		forward = Vector3.Normalize(forward);

		right = Quaternion.Euler(new Vector3(0,90,0)) * forward;

		moveSpeed = walkSpeed;

	}

	// Update is called once per frame
	void Update () {

		if (ongoingEvents.Count == 0) {
			// Movement
			if (Mathf.Abs(Input.GetAxis ("Horizontal")) > 0.1f || Mathf.Abs(Input.GetAxis ("Vertical")) > 0.1f) {
				Move();
			}

			if (Input.GetKeyDown (KeyCode.LeftShift)) {
				moveSpeed = runSpeed;
			}
			if (Input.GetKeyUp (KeyCode.LeftShift)) {
				moveSpeed = walkSpeed;
			}

			if (Input.GetKeyDown (KeyCode.Space)) {

				Ray ray = new Ray(jumpPoint.transform.position, jumpPoint.transform.forward);
				RaycastHit hit;


				if (Physics.Raycast (ray, out hit, 0.15f)) {
					gameObject.GetComponent<Rigidbody> ().AddForce (new Vector3 (0, jumpForce, 0));
				}
			}
		}

	}

	void Move() {

		// Movement speed
		Vector3 rightMovement = right * moveSpeed * Input.GetAxis("Horizontal");
		Vector3 upMovement = forward * moveSpeed * Input.GetAxis("Vertical");
		if (Mathf.Abs(Input.GetAxis ("Horizontal")) > 0.8f && Mathf.Abs(Input.GetAxis ("Vertical")) > 0.8f) {
			rightMovement = right * (moveSpeed-0.3f*moveSpeed) * Input.GetAxis("Horizontal");
			upMovement = forward * (moveSpeed-0.3f*moveSpeed) * Input.GetAxis("Vertical");
		}

		Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

		Vector3 newPosition = transform.position;
		newPosition += rightMovement;
		newPosition += upMovement;

		transform.forward = heading;
		transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime);


	}

	public void addEvent(GameObject UIEvent){
		ongoingEvents.Add (UIEvent);
		moveSpeed = walkSpeed;
	}

	public void removeEvent(GameObject UIEvent){
		ongoingEvents.Remove (UIEvent);
	}

/*	public float speed = 1.0f;
	public float maxSpeed = 5.0f;
	public float deadZone = 0.1f;
	public float slowDownSpeed = 0.95f;
	private float currentSpeed;
	Coroutine SlowingDown;


	// Update is called once per frame
	void FixedUpdate () {

		float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
		float vertical = CrossPlatformInputManager.GetAxis("Vertical");
		float horizontalSpeed = GetComponent<Rigidbody> ().velocity.x;
		float verticalSpeed = GetComponent<Rigidbody> ().velocity.z;
		currentSpeed = Mathf.Sqrt (horizontalSpeed * horizontalSpeed + verticalSpeed * verticalSpeed);

		Debug.Log (currentSpeed);

		if ((Mathf.Abs (horizontal) + Mathf.Abs (vertical)) < deadZone) {
			SlowingDown = StartCoroutine (SlowDown());
			if (currentSpeed < 0.1f) {
				Debug.Log ("StopMoving");
				StopCoroutine (SlowingDown);
				//GetComponent<Rigidbody> ().velocity = new Vector3 (0, GetComponent<Rigidbody> ().velocity.y , 0);
			}
		} else {
			StopAllCoroutines ();

			if (currentSpeed < maxSpeed) {
				GetComponent<Rigidbody> ().AddForce (transform.right * speed * -vertical);
				GetComponent<Rigidbody> ().AddForce (transform.forward * speed * horizontal);
			} 

		}
	}

	IEnumerator SlowDown(){
		
		while (true) {
			Debug.Log ("SlowDown");
			GetComponent<Rigidbody> ().velocity = new Vector3 (GetComponent<Rigidbody> ().velocity.x*slowDownSpeed , GetComponent<Rigidbody> ().velocity.y , GetComponent<Rigidbody> ().velocity.z*slowDownSpeed);
			yield return 0;
		}
	}*/


}
