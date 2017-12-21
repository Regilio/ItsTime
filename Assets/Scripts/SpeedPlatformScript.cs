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

	public ObstacleMoving fallingObstacleScript;

    public DockManagementScript dmScript;
	bool isMoving = false;
	Coroutine moveCoroutine;
	Coroutine leaveCoroutine;

    void Start()
    {

        speed = playSpeed;
        timer = 0;
    }



	IEnumerator Move(){
		while (true) {
			switch (dmScript.currentState) {
			case (int)DockManagementScript.states.backwards:
				speed = -playSpeed;
				break;
			case (int)DockManagementScript.states.pause:
				speed = 0;
				break;
			case (int)DockManagementScript.states.play:
				speed = playSpeed;
				break;
			case (int)DockManagementScript.states.forward:
				speed = forwardSpeed;
				break;
			}

			timer += Time.deltaTime * speed;
			if (timer < 0)
				timer = 0;
			if (timer > 1)
				timer = 1;
			transform.position = Vector3.Lerp (StartPosition.position, EndPosition.position, timer);
			yield return 0;
		}
	}

	IEnumerator LeavePlatform(){
		float secondsLeft = 0;
		while (secondsLeft < 2.0f) {
			secondsLeft += Time.deltaTime;
			yield return 0;
		}
		timer = 0;
		transform.position = Vector3.Lerp (StartPosition.position, EndPosition.position, timer);
		StopCoroutine (moveCoroutine);
		fallingObstacleScript.StopMoving ();
		yield return 0;
	}

    void OnTriggerEnter(Collider other)
    {
       if (other.tag == "Player")
        {
			isMoving = true;
			other.transform.parent = transform;
			moveCoroutine = StartCoroutine(Move());
			fallingObstacleScript.StartMoving ();
			if (leaveCoroutine != null) {
				StopCoroutine (leaveCoroutine);
			}
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = null;
			leaveCoroutine = StartCoroutine (LeavePlatform ());
        }
    }
}





