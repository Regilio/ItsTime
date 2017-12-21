using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingPaltformScript : MonoBehaviour
{

    float speed;
    public float normalSpeed = 1;
    public float forwardSpeed = 2;

    public float offset = 1; //point de depart
    public float distanceCourse = 1;

    public DockManagementScript Dock;
    int lastFrameState;

    void Start()
    {
        speed = normalSpeed;
        lastFrameState = Dock.currentState;
        StartCoroutine(NormalTransition());
    }


    void Update()
    {
        if (Dock.currentState != lastFrameState)
        {
            switch (Dock.currentState)
            {
                case (int)DockManagementScript.states.backwards:
                    StopAllCoroutines();
                    StartCoroutine(BackwardTransition());
                    speed = normalSpeed;
                    break;
                case (int)DockManagementScript.states.pause:
                    speed = 0;
                    StopAllCoroutines();
                    break;
                case (int)DockManagementScript.states.play:
                    StopAllCoroutines();
                    StartCoroutine(NormalTransition());
                    speed = normalSpeed;
                    break;
                case (int)DockManagementScript.states.forward:
                    StopAllCoroutines();
                    StartCoroutine(NormalTransition());
                    speed = forwardSpeed;
                    break;

            }
            lastFrameState = Dock.currentState;
        }
    }



    IEnumerator NormalTransition()
    {

        while (true)
        { // Enlever le systeme de pingpong et utiliser Lerp

    //        transform.position = new Vector3(offset + Mathf.PingPong(Time.time * speed , distanceCourse), transform.position.y, transform.position.z); //effet va et vient
            yield return 0;
        }

    }

    IEnumerator BackwardTransition()
    {
        //Debug.Log(test);
        while (true)
        {
       //     transform.position = new Vector3(offset + Mathf.PingPong(Time.time * speed + test, distanceCourse), transform.position.y, transform.position.z); //effet va et vient
            yield return 0;
        }

    }






}

