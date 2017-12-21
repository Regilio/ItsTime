using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentPickupScript : MonoBehaviour {

    public int fragmentID = 0;
    // Use this for initialization
    void Start () {
        if (PlayerPrefs.GetInt("Fragment " + fragmentID, 0) == 1)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<FragmentTextScript>().addFragment();
            PlayerPrefs.SetInt("Fragment " + fragmentID, 1);
            Destroy(gameObject);      //On detruit le game object qui est dans le trigger "other"
        }
    }
}
