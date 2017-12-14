using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DockManagementScript : MonoBehaviour {

	public Image backwards;
	public Image pause;
	public Image play;
	public Image forward;
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
			currentState = (int)states.backwards; 
			SetColorsAndSpeed ();
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			currentState = (int)states.pause;
			SetColorsAndSpeed ();
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			currentState = (int)states.play;
			SetColorsAndSpeed ();
		}
		if (Input.GetKeyDown (KeyCode.Alpha4)) {
			currentState = (int)states.forward;
			SetColorsAndSpeed ();
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

		backwards.color = Color.black;
		pause.color = Color.black;
		play.color = Color.black;
		forward.color = Color.black;

		switch (currentState) {
		case (int)states.backwards:
			backwards.color = Color.white;
			currentStopWatchSpeed = -0.5f * globalStopWatchSpeed;
			manaScript.consumption = true;
			break;
		case (int)states.pause:
			pause.color = Color.white;
			currentStopWatchSpeed = 0;
			manaScript.consumption = true;
			break;
		case (int)states.play:
			play.color = Color.white;
			currentStopWatchSpeed = 0.5f*globalStopWatchSpeed;
			manaScript.consumption = false;
			break;
		case (int)states.forward:
			forward.color = Color.white;
			currentStopWatchSpeed = 1.0f*globalStopWatchSpeed;
			manaScript.consumption = true;
			break;
		}
	}
}
