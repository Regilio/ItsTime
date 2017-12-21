using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMoving : MonoBehaviour {

	public Transform StartPos;
	public Transform EndPos;
    public float speed = 0.5f;
	public Coroutine moveCoroutine;
	float timer = 0;


	public void StartMoving (){

		moveCoroutine = StartCoroutine(ClosingA());
	}

	public void StopMoving(){
		if(moveCoroutine!=null)
			StopCoroutine (moveCoroutine);
		timer = 0;
		transform.position = Vector3.Lerp(StartPos.position, EndPos.position, timer);
	}

    IEnumerator ClosingA()
    {

        while(timer < 1)
        {
            timer += Time.deltaTime * speed;
			transform.position = Vector3.Lerp(StartPos.position, EndPos.position, timer);
            yield return 0;
        }
    }



}
