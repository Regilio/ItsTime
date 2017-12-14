using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DockManagementScript : MonoBehaviour {

	public Image backwardsImg;
	public Image pauseImg;
	public Image playImg;
	public Image forwardImg;
	public Image aiguille;
	enum states{backwards,pause,play,forward};

	Vector3 RotationA;
	Vector3 RotationB;
	public float timer = 0;
	public float globalStopWatchSpeed = 1.0f;
	float currentStopWatchSpeed;

	public ManaScript manaScript;

	int currentState;

	// Use this for initialization
	void Start () {
		currentState = (int)states.play;
		SetColorsAndSpeed ();
		RotationA = new Vector3(0, 0, 360);
		RotationB = new Vector3(0, 0, 0);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			Backwards ();
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			Pause ();
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			Play ();
		}
		if (Input.GetKeyDown (KeyCode.Alpha4)) {
			Forward ();
		}

		if (currentStopWatchSpeed > 0) {

			timer += Time.deltaTime * currentStopWatchSpeed;
			aiguille.rectTransform.localEulerAngles = Vector3.Lerp(RotationA, RotationB, timer);
			if (timer > 1)
				timer = 0;
		}else if (currentStopWatchSpeed < 0) {
			timer -= Time.deltaTime * -currentStopWatchSpeed;
			aiguille.rectTransform.localEulerAngles = Vector3.Lerp(RotationA, RotationB, timer);
			if (timer <= 0)
				timer = 1;
		}
	}

	void SetColorsAndSpeed(){

		backwardsImg.color = Color.black;
		pauseImg.color = Color.black;
		playImg.color = Color.black;
		forwardImg.color = Color.black;

		switch (currentState) {
		case (int)states.backwards:
			backwardsImg.color = Color.white;
			currentStopWatchSpeed = -0.5f * globalStopWatchSpeed;
			manaScript.consumption = true;
			break;
		case (int)states.pause:
			pauseImg.color = Color.white;
			currentStopWatchSpeed = 0;
			manaScript.consumption = true;
			break;
		case (int)states.play:
			playImg.color = Color.white;
			currentStopWatchSpeed = 0.5f*globalStopWatchSpeed;
			manaScript.consumption = false;
			break;
		case (int)states.forward:
			forwardImg.color = Color.white;
			currentStopWatchSpeed = 1.0f*globalStopWatchSpeed;
			manaScript.consumption = true;
			break;
		}
	}

	void Backwards(){
		currentState = (int)states.backwards; 
		SetColorsAndSpeed ();
	}

	void Pause(){
		currentState = (int)states.pause;
		SetColorsAndSpeed ();
	}

	void Play(){
		currentState = (int)states.play;
		SetColorsAndSpeed ();
	}

	void Forward(){
		currentState = (int)states.forward; 
		SetColorsAndSpeed ();
	}
}
