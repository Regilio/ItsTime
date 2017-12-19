using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlatformScript : MonoBehaviour
{

    public GameObject SpawnPrefab;

    public Transform pos1;
    public Transform pos2;
    bool enHaut = true;
    float timer = 0;
    public DockManagementScript dmScript;
    public float spawnRate = 5.0f;
    float currentRate = 1;

    int lastFrameState;

    void Start()
    {
        currentRate = spawnRate;
        timer = spawnRate - 0.1f;
    }



    
    void Update()
    {

        if (dmScript.currentState != lastFrameState)
        {
            switch (dmScript.currentState)
            {

                case (int)DockManagementScript.states.backwards:
                    enHaut = false;
                    DeplaceSpawn();
                    StopAllCoroutines();
                    currentRate = 1;
                    StartCoroutine(Spawn());
                    break;
                case (int)DockManagementScript.states.pause:
                    StopAllCoroutines();
                    break;
                case (int)DockManagementScript.states.play:
                    enHaut = true;
                    DeplaceSpawn();
                    StopAllCoroutines();
                    currentRate = 1;
                    StartCoroutine(Spawn());
                    break;
                case (int)DockManagementScript.states.forward:
                    enHaut = true;
                    DeplaceSpawn();
                    StopAllCoroutines();
                    currentRate = 2;
                    StartCoroutine(Spawn());
                    break;
            }
        }
        lastFrameState = dmScript.currentState;

    }

    void DeplaceSpawn()
    {
        if (enHaut)
        {
            gameObject.transform.position = pos1.position;
        }else
        {
            gameObject.transform.position = pos2.position;
        }
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            timer += Time.deltaTime * currentRate;
           
            if (timer > spawnRate)
            {
                //Debug.Log(transform.position);
                Instantiate(SpawnPrefab, transform.position, Quaternion.identity);
                GameObject newPlateform = Instantiate(SpawnPrefab, transform.position, Quaternion.identity);
                newPlateform.GetComponent<FallingScript>().lastFrameState = (int)dmScript.currentState;

                timer = 0;
            }
            yield return 0;
        }
    }

    

}