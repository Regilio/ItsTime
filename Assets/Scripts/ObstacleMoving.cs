using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMoving : MonoBehaviour {

    public Vector3 StartPos;
    public Vector3 EndPos;
    public float speed = 0.5f;

    void Start () {

        StartPos = transform.localPosition;

        StartCoroutine(ClosingA());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator ClosingA()
    {
        float timer = 0;

        while(timer < 1)
        {
            timer += Time.deltaTime * speed;
            transform.localPosition = Vector3.Lerp(StartPos, EndPos, timer);
            yield return 0;
        }
    }



}
