using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixPositionScript : MonoBehaviour
{
    

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            other.transform.SetParent(transform);
           

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
          other.transform.SetParent(null);
       

        }
    }


}
