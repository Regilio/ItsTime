using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingAsyncScript : MonoBehaviour
{

    public Transform Plateforme1;
    public Transform Plateforme2;
    public Transform Plateforme3;

    public Transform StartPos1;
    public Transform StartPos2;
    public Transform StartPos3;

    public Transform EndPos1;
    public Transform EndPos2;
    public Transform EndPos3;

    public DockManagementScript dmScript;

    float timer1 = 0;
    float timer2 = 0;
    float timer3 = 0;

    public float speed;
    float playSpeed = 0.15f;
    float forwardSpeed = 0.5f;

    int lastFrameState;

    bool spawning = false;



    void Start()
    {
        speed = playSpeed;
      


        switch (dmScript.currentState)
        {

            case (int)DockManagementScript.states.backwards:
                StopAllCoroutines();
                speed = playSpeed;

                break;
            case (int)DockManagementScript.states.pause:
                speed = 0;
                StopAllCoroutines();
                break;
            case (int)DockManagementScript.states.play:
                StopAllCoroutines();
                speed = playSpeed;

                break;
            case (int)DockManagementScript.states.forward:
                StopAllCoroutines();
                speed = forwardSpeed;

                break;

        }
        lastFrameState = dmScript.currentState;

        StopAllCoroutines();

    }

    void Update()
    {
        if (dmScript.currentState != lastFrameState && spawning)
        {
            switch (dmScript.currentState)
            {

                case (int)DockManagementScript.states.backwards:
                    StopAllCoroutines();
                    speed = playSpeed;
                    StartCoroutine(Elevate1());
                    StartCoroutine(Elevate2());
                    StartCoroutine(Elevate3());

                    break;
                case (int)DockManagementScript.states.pause:
                    speed = 0;
                    StopAllCoroutines();
                    break;
                case (int)DockManagementScript.states.play:
                    StopAllCoroutines();
                    speed = playSpeed;
                    StartCoroutine(Falling1());
                    StartCoroutine(Falling2());
                    StartCoroutine(Falling3());
                    break;
                case (int)DockManagementScript.states.forward:
                    StopAllCoroutines();
                    speed = forwardSpeed;

                    StartCoroutine(Falling1());
                    StartCoroutine(Falling2());
                    StartCoroutine(Falling3());
                    break;

            }
        }
        lastFrameState = dmScript.currentState;
    }



    //boucle pour la plateforme 1 de la plateforme 5
    IEnumerator Falling1()
    {
        while (true)
        {
            timer1 += Time.deltaTime * speed;
            Plateforme1.transform.position = Vector3.Lerp(StartPos1.position, EndPos1.position, timer1);
            if (timer1 > 1)
                timer1 = 0;

            yield return 0;
        }
    }

    //boucle pour la plateforme 2 de la plateforme 5
    IEnumerator Falling2()
    {
        while (true)
        {

            timer2 += Time.deltaTime * speed;
            Plateforme2.transform.position = Vector3.Lerp(StartPos2.position, EndPos2.position, timer2);
            if (timer2 > 1)
                timer2 = 0;
            yield return 0;
        }
    }

    //boucle pour la plateforme 3 de la plateforme 5
    IEnumerator Falling3()
    {
        while (true)
        {

            timer3 += Time.deltaTime * speed;
            Plateforme3.transform.position = Vector3.Lerp(StartPos3.position, EndPos3.position, timer3);
            if (timer3 > 1)
                timer3 = 0;
            yield return 0;
        }
    }


    //boucle pour la plateforme 1 de la plateforme 5
    IEnumerator Elevate1()
    {
        while (true)
        {
            timer1 -= Time.deltaTime * speed;
            Plateforme1.transform.position = Vector3.Lerp(StartPos1.position, EndPos1.position, timer1);
            if (timer1 < 0)
                timer1 = 1;

            yield return 0;
        }
    }

    //boucle pour la plateforme 2 de la plateforme 5
    IEnumerator Elevate2()
    {
        while (true)
        {

            timer2 -= Time.deltaTime * speed;
            Plateforme2.transform.position = Vector3.Lerp(StartPos2.position, EndPos2.position, timer2);
            if (timer2 < 0)
                timer2 = 1;
            yield return 0;
        }
    }

    //boucle pour la plateforme 3 de la plateforme 5
    IEnumerator Elevate3()
    {
        while (true)
        {

            timer3 -= Time.deltaTime * speed;
            Plateforme3.transform.position = Vector3.Lerp(StartPos3.position, EndPos3.position, timer3);
            if (timer3 < 0)
                timer3 = 1;
            yield return 0;
        }
    }

    IEnumerator Async()
    {
        StartCoroutine(Falling1());
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Falling2());
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Falling3());
        yield return 0;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !spawning)
        {
            spawning = true;
           
            StartCoroutine(Async());
          
        }

    }
}
