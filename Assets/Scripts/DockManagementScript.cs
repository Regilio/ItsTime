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
	public enum states{backwards,pause,play,forward};

	Vector3 RotationA;
	Vector3 RotationB;
	public float timer = 0;
	public float globalStopWatchSpeed = 1.0f;
	float currentStopWatchSpeed;

	public ManaScript ControllerManaScript;

	public int currentState;

	public bool[] enableButtons = {true, true, true, true};

	List<GameObject> ongoingEvents;

	CanvasGroup canvasGroup;
	void Awake(){
		ongoingEvents = new List<GameObject> ();
		canvasGroup = gameObject.GetComponent<CanvasGroup> ();
	}

	// Use this for initialization
	void Start () {
		StartCoroutine (TimeManagement ());
		Play ();
		RotationA = new Vector3(0, 0, 360);
		RotationB = new Vector3(0, 0, 0);
	}

	// Update is called once per frame
	void Update () {
		if (ongoingEvents.Count == 0) {
			if (Input.GetKeyDown (KeyCode.Alpha1) && enableButtons [0]) {
				Backwards ();
			}
			if (Input.GetKeyDown (KeyCode.Alpha2) && enableButtons [1]) {
				Pause ();
			}
			if (Input.GetKeyDown (KeyCode.Alpha3) && enableButtons [2]) {
				Play ();
			}
			if (Input.GetKeyDown (KeyCode.Alpha4) && enableButtons [3]) {
				Forward ();
			}
		}

	}

	IEnumerator TimeManagement(){
		while (true) {

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
			yield return 0;
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
			ControllerManaScript.consuming(true);
			break;
		case (int)states.pause:
			pauseImg.color = Color.white;
			currentStopWatchSpeed = 0;
			ControllerManaScript.consuming(true);
			break;
		case (int)states.play:
			playImg.color = Color.white;
			currentStopWatchSpeed = 0.5f*globalStopWatchSpeed;
			ControllerManaScript.consuming(false);
			break;
		case (int)states.forward:
			forwardImg.color = Color.white;
			currentStopWatchSpeed = 1.0f*globalStopWatchSpeed;
			ControllerManaScript.consuming(true);
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

	public void Play(){
		currentState = (int)states.play;
		SetColorsAndSpeed ();
	}

	void Forward(){
		currentState = (int)states.forward; 
		SetColorsAndSpeed ();
	}

	public void EnableButtons(bool[] buttonBools){

		enableButtons = buttonBools;

		backwardsImg.gameObject.SetActive(enableButtons [0]);
		pauseImg.gameObject.SetActive(enableButtons [1]);
		playImg.gameObject.SetActive(enableButtons [2]);
		forwardImg.gameObject.SetActive(enableButtons [3]);

	}

	public void addEvent(GameObject UIEvent, bool hideDock = true){
		ongoingEvents.Add (UIEvent);
		if (hideDock) {
			canvasGroup.alpha = 0f;
			canvasGroup.blocksRaycasts = false;

		}
	}



	public void removeEvent(GameObject UIEvent){
		ongoingEvents.Remove (UIEvent);
		if (ongoingEvents.Count == 0) {
			canvasGroup.alpha = 1f;
			canvasGroup.blocksRaycasts = true;
		}
	}
}