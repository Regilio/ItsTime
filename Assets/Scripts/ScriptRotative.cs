using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptRotative : MonoBehaviour {

    Vector3 RotationA;
    Vector3 RotationB;
   public float timer = 0;
    public float speed;
    public float playSpeed = 0.05f;
    public float forwardSpeed = 0.1f;

    public DockManagementScript dmScript;

    int lastFrameState;


    void Start()
    {
        speed = playSpeed;
        RotationA = new Vector3(0, 0, 0);
        RotationB = new Vector3(0, 360, 0);
        lastFrameState = dmScript.currentState;
        StartCoroutine(NormalRotation());

    }

    void Update()
    {
        if(dmScript.currentState != lastFrameState)
        {
            switch (dmScript.currentState)
            {
                case (int)DockManagementScript.states.backwards:
                    StopAllCoroutines();
                    StartCoroutine(BackwardRotation());
                    speed = playSpeed;
                    break;
                case (int)DockManagementScript.states.pause:
                    speed = 0;
                    StopAllCoroutines();
                    break;
                case (int)DockManagementScript.states.play:
                    StopAllCoroutines();
                    StartCoroutine(NormalRotation());
                    speed = playSpeed;
                    break;
                case (int)DockManagementScript.states.forward:
                    StopAllCoroutines();
                    StartCoroutine(NormalRotation());
                    speed = forwardSpeed;
                    break;

            }
            lastFrameState = dmScript.currentState;
        }
    }


    IEnumerator NormalRotation()
    {
        while (true)
        {
            timer += Time.deltaTime * speed;
            transform.localEulerAngles = Vector3.Lerp(RotationA, RotationB, timer);
            if (timer > 1)
                timer = 0;
            yield return 0;
        }

    }

    IEnumerator BackwardRotation()
    {
        while (true)
        {
            timer -= Time.deltaTime * speed;
            //Debug.Log(timer);
            transform.localEulerAngles = Vector3.Lerp(RotationA, RotationB, timer);
            if (timer < 0)
                timer = 1;
            yield return 0;
        }

    }


}
