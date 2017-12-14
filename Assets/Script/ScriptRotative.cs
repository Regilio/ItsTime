using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptRotative : MonoBehaviour {

    Vector3 RotationA;
    Vector3 RotationB;
    float timer = 0;
    float speed = 0.05f;


    void Start()
    {
        RotationA = new Vector3(0, 0, 0);
        RotationB = new Vector3(0, 360, 0);

        StartCoroutine(NormalRotation());
    }

    void Update()
    {
  
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
}
