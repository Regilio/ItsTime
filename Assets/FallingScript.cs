using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingScript : MonoBehaviour {

    public Vector3 EndPosition;
    public Vector3 StartPosition;

    float timer = 0;
    public float speed;
    public float playSpeed = 0.1f;
    public float forwardSpeed = 0.2f;

    public DockManagementScript dmScript;

    int lastFrameState;

    void Start () {

        speed = playSpeed;
        StartCoroutine(FallingCoroutine());
        GameObject.("DockManagementScript");
	}
	

	// Update is called once per frame
	void Update () {

        if (dmScript.currentState != lastFrameState)
        {
            switch (dmScript.currentState)
            {
                /*
                case (int)DockManagementScript.states.backwards:
                    StopAllCoroutines();
                    StartCoroutine(ElevateCoroutine());
                    speed = playSpeed;
                    break;*/
                case (int)DockManagementScript.states.pause:
                    speed = 0;
                    StopAllCoroutines();
                    break;
                case (int)DockManagementScript.states.play:
                    StopAllCoroutines();
                    StartCoroutine(FallingCoroutine());
                    speed = playSpeed;
                    break;
                case (int)DockManagementScript.states.forward:
                    StopAllCoroutines();
                    StartCoroutine(FallingCoroutine());
                    speed = forwardSpeed;
                    break;

            }
            lastFrameState = dmScript.currentState;
        }
    }


    IEnumerator FallingCoroutine()
    {
        while (true)
        {
            
            timer += Time.deltaTime * speed;
        
            transform.localPosition = Vector3.Lerp(StartPosition, EndPosition, timer);
            if (timer >= 1)
                Destroy(gameObject);
            yield return 0;
        }

    }
    /*
    IEnumerator ElevateCoroutine()
    {
        while(true)
        {
            Debug.Log(timer);
            timer -= Time.deltaTime * speed;
            transform.localPosition = Vector3.Lerp(EndPosition, StartPosition, timer);
            if (timer <= 1)
                Destroy(gameObject);
            yield return 0;
        }
    }
    */
}
