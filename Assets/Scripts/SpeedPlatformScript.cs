using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPlatformScript : MonoBehaviour
{

    public Transform StartPosition;
    public Transform EndPosition; 

    public float timer = 0;
    public float speed;
    public float playSpeed = 0.2f;
    public float forwardSpeed = 0.4f;

    public DockManagementScript dmScript;
    public int lastFrameState;

    void Start()
    {

        speed = playSpeed;
        timer = 0;

        lastFrameState = dmScript.currentState;
        StartCoroutine(NormalMove());
    }

    void Update()
    {
        if (dmScript.currentState != lastFrameState)
        {
            switch (dmScript.currentState)
            {
                case (int)DockManagementScript.states.backwards:
                    StopAllCoroutines();
                    StartCoroutine(BackwardMove());
                    speed = playSpeed;
                    break;
                case (int)DockManagementScript.states.pause:
                    speed = 0;
                    StopAllCoroutines();
                    break;
                case (int)DockManagementScript.states.play:
                    StopAllCoroutines();
                    StartCoroutine(NormalMove());
                    speed = playSpeed;
                    break;
                case (int)DockManagementScript.states.forward:
                    StopAllCoroutines();
                    StartCoroutine(NormalMove());
                    speed = forwardSpeed;
                    break;

            }
            lastFrameState = dmScript.currentState;
        }


       
    }


    IEnumerator NormalMove()
    {
        while (true)
        {
            //Debug.Log("go");
            float pingPong = Mathf.PingPong(Time.time * speed, 1);
            transform.localPosition = Vector3.Lerp(StartPosition.position, EndPosition.position, pingPong);
            if (timer > 1)
                timer = 0;
            yield return 0;
        }

    }

    IEnumerator BackwardMove()
    {
        while(true)
        {
            //Debug.Log("Gogo");
            float pingPong = Mathf.PingPong(Time.time * speed, 1);
            transform.localPosition = Vector3.Lerp(EndPosition.position, StartPosition.position, pingPong);
            if (timer < 0)
                timer = 1;
            yield return 0;
        }
    }



    void OnTriggerEnter(Collider other)
    {
       /* if (other.gameObject.name == "Obstacle")
        {
            Debug.Log("Hit");
            StopAllCoroutines();
        }*/

       if (other.tag == "Player")
        {
           
            other.transform.parent = transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            
            other.transform.parent = null;
        }
    }
}





