using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventScript : MonoBehaviour {

	bool isPlaying = false;
	bool isFinished = false;
	public bool replayable = false;
	public bool disableMovement = true;
	public bool disableTime = true;
	public bool hideDock = true;

	public bool[] enableDockButtons = {true, true, true, true};

	public bool changeMana = false;
	public float setManaTo = 0;

	public string[] setText = null;

	public ManaScript manaScript;
	public DockManagementScript dmScript;
	public MovementScript movScript;

	public Text msgText;
	public Text pressEnterText;
	public Image msgTextImage;

	public Camera playerCamera;
	public float cameraZoom = 0;

	Coroutine currentCoroutine;

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player" && !isPlaying && (!isFinished || replayable) )
		{
			isPlaying = true; 
			isFinished = false;
			if (disableMovement) {
				movScript.addEvent (gameObject);
			}
			if (disableTime) {
				dmScript.addEvent (gameObject, hideDock);
				dmScript.Play ();
			}

			dmScript.EnableButtons (enableDockButtons);

			if (changeMana) {
				manaScript.ChangeMana (setManaTo);
			}

			if (setText.Length != 0) {
				msgText.gameObject.SetActive (true);
				pressEnterText.gameObject.SetActive (true);
				msgTextImage.gameObject.SetActive (true);
				currentCoroutine = StartCoroutine (TextCoroutine());
			}

			if (cameraZoom != 0) {
				StartCoroutine (CameraZoom ());
			}
		}
	}

	IEnumerator CameraZoom(){
		float timer = 0.0f;
		float currentCameraSize = playerCamera.orthographicSize;
		if(currentCameraSize != cameraZoom)
			while (timer < 1.0f) {
				playerCamera.orthographicSize = Mathf.Lerp (currentCameraSize, cameraZoom, timer);
				timer += Time.deltaTime *2;
				yield return 0;
			}
		yield return 0;
	}

	void StopEvent(){
		if (isPlaying) {
			isPlaying = false;
			isFinished = true;
			if (disableMovement) {
				movScript.removeEvent (gameObject);
			}

			if (disableTime) {
				dmScript.removeEvent (gameObject);
				dmScript.Play ();
			}

			if (setText.Length != 0) {
				msgText.gameObject.SetActive (false);
				msgTextImage.gameObject.SetActive (false);
				pressEnterText.gameObject.SetActive (false);
				StopCoroutine (currentCoroutine);
			}

		}

	}

	IEnumerator TextCoroutine(){
		foreach (string message in setText) {
			msgText.text = message.Replace("\\n","\n");
			while (!Input.GetKeyDown (KeyCode.Return) && !Input.GetKeyDown (KeyCode.KeypadEnter)) {
				yield return 0;
			}
			yield return 0;
		}
		StopEvent ();
	}


	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player" && isPlaying)
			StopEvent ();

	}
}
