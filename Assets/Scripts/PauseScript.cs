using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour {

    public GameObject PauseCanvas;
	public bool Paused = false;
	public DockManagementScript dmScript;


    private void Start()
    {
        Pause();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            /*Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;*/
            Paused = !Paused;
            Pause();
        }
	}

    private void Pause()
    {
        /*Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;*/
        Time.timeScale = Paused ? 0 : 1;
		if (Paused) {
			dmScript.addEvent (gameObject, true);
		} else {
			dmScript.removeEvent (gameObject);
		}
        PauseCanvas.SetActive(Paused);
    }
}
