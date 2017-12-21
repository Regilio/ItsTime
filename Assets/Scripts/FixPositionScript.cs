using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixPositionScript : MonoBehaviour
{
	GameObject currentSon = null;


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            other.transform.SetParent(transform);
			currentSon = other.gameObject;

        }
    }




    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
			removeSon();
		}
    }

	public void removeSon(){
		if (currentSon != null) {
			currentSon.transform.SetParent (null);
			currentSon.transform.localScale = new Vector3 (1, 1, 1);
			currentSon = null;
		}
	}

}
