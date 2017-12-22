using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventScript : MonoBehaviour {

	bool isPlaying = false;
	bool isFinished = false;
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

	Coroutine currentCoroutine;

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player" && !isFinished && !isPlaying)
		{
			isPlaying = true; 
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

			if (setText != null) {
				msgText.gameObject.SetActive (true);
				pressEnterText.gameObject.SetActive (true);
				msgTextImage.gameObject.SetActive (true);
				currentCoroutine = StartCoroutine (TextCoroutine());
			}
		}
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

			if (setText != null) {
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
}
