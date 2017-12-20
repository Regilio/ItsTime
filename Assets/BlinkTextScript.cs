using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkTextScript : MonoBehaviour {

	public Text blinkText;
	Color initialColor;
	Color fadeColor;
	float timer = 0;
	bool fadeIn = true;
	// Use this for initialization
	void Awake () {
		initialColor = blinkText.color;
		fadeColor = new Color (initialColor.r, initialColor.g, initialColor.b, 0.2f);
		//Debug.Log ("hey");
		//StartCoroutine (Blinking ());
	}
	
	// Update is called once per frame
	void Update () {
		blinkText.color = Color.Lerp (initialColor, fadeColor, timer);
		if (fadeIn) {
			timer += Time.deltaTime;
		} else {
			timer -= Time.deltaTime;
		}
		if (timer >= 1) {
			fadeIn = false;
		}
		if (timer <= 0) {
			fadeIn = true;
		}
	}

	/*IEnumerator Blinking(){
		while (true) {
			blinkText.color = Color.Lerp (initialColor, fadeColor, timer);
			if (fadeIn) {
				timer += Time.deltaTime;
			} else {
				timer -= Time.deltaTime;
			}
			if (timer >= 1) {
				fadeIn = false;
			}
			if (timer <= 0) {
				fadeIn = true;
			}
			yield return 0;
		}
	}*/

}
