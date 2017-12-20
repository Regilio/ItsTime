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

    public float timer = 0;
    public float speed;
    public float playSpeed = 0.5f;
    public float forwardSpeed = 1f;

    int lastFrameState;

    void Start()
    {
        speed = playSpeed;


    }

    void Update()
    {


    }

    IEnumerator Falling1()
    {
        while (true)
        {   
            timer += Time.deltaTime * speed;
			Plateforme1.transform.position = Vector3.Lerp(StartPos1.position, EndPos1.position, timer);
            if (timer > 1)
                timer = 0;
            yield return 0;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(Falling1());
        }
    }



}