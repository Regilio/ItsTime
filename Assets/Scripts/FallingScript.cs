using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingScript : MonoBehaviour
{

    public Vector3 EndPosition;
    public Vector3 StartPosition;

    public float timer = 0;
    public float speed;
    public float playSpeed = 0.2f;
    public float forwardSpeed = 0.4f;

    public float depopTimer = 5.0f;
 

    public DockManagementScript dmScript;
    public int lastFrameState = -1;

    void Start()
    {

        speed = playSpeed;
        StartCoroutine(FallingCoroutine());
        dmScript = GameObject.Find("Dock").GetComponent<DockManagementScript>();
        timer = 0;
        
        switch (dmScript.currentState)
        {

            case (int)DockManagementScript.states.backwards:
                StopAllCoroutines();
                speed = playSpeed;
                StartCoroutine(ElevateCoroutine());
                break;
            case (int)DockManagementScript.states.pause:
                speed = 0;
                StopAllCoroutines();
                break;
            case (int)DockManagementScript.states.play:
                StopAllCoroutines();
                speed = playSpeed;
                StartCoroutine(FallingCoroutine());
                break;
            case (int)DockManagementScript.states.forward:
                StopAllCoroutines();
                speed = forwardSpeed;
                StartCoroutine(SpeedFallingCoroutine());
                break;

        }
    }


    // Update is called once per frame
    void Update()
    {
        if (dmScript.currentState != lastFrameState)
        {
            switch (dmScript.currentState)
            {

                case (int)DockManagementScript.states.backwards:
                    StopAllCoroutines();
                    speed = playSpeed;
                    
                    timer = 1 - timer;
                   
                    StartCoroutine(ElevateCoroutine());
                    break;
                case (int)DockManagementScript.states.pause:
                    speed = 0;
                    if (lastFrameState == (int)DockManagementScript.states.backwards)
                        timer = 1 - timer;
                    StopAllCoroutines();
                    break;
                case (int)DockManagementScript.states.play:
                    StopAllCoroutines();
                    speed = playSpeed;
                    if (lastFrameState == (int)DockManagementScript.states.backwards)
                        timer = 1 - timer;
                    StartCoroutine(FallingCoroutine());
                    break;
                case (int)DockManagementScript.states.forward:
                    StopAllCoroutines();
                    speed = forwardSpeed;
                    if (lastFrameState == (int)DockManagementScript.states.backwards)
                        timer = 1 - timer;
                    StartCoroutine(SpeedFallingCoroutine());
                    break;

            }
        }
        lastFrameState = dmScript.currentState;

    }


    IEnumerator FallingCoroutine()
    {
        while (true)
        {
            //Debug.Log(timer);
            timer += Time.deltaTime * speed;

            transform.localPosition = Vector3.Lerp(StartPosition, EndPosition, timer);
            if (timer / speed >= depopTimer)
                Destroy(gameObject);
            yield return 0;
        }

    }


    IEnumerator SpeedFallingCoroutine()
    {
        while (true)
        {
            //Debug.Log(timer);
            timer += Time.deltaTime * speed;

            transform.localPosition = Vector3.Lerp(StartPosition, EndPosition, timer);
            if (timer / speed >= depopTimer / 2.0f)
                Destroy(gameObject);
            yield return 0;
        }

    }

    IEnumerator ElevateCoroutine()
    {
        while (true)
        {
            timer += Time.deltaTime * speed;
            transform.localPosition = Vector3.Lerp(EndPosition, StartPosition, timer);
           
            if (timer / speed >= depopTimer)
                Destroy(gameObject);
            yield return 0;
        }
    }

}
