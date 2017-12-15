using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlatformScript : MonoBehaviour
{

    public Transform Prefab;
    float timer = 0;

    void Start()
    {
        StartCoroutine(Spawn());
        timer = 4.9f;
    }

    IEnumerator Spawn()
    {
        while(true)
        {
            timer += Time.deltaTime;

            if (timer > 5)
            {
                Instantiate(Prefab,this.gameObject.transform.position,Quaternion.identity);
                timer = 0;
            }
            yield return 0;
        }
    }

}