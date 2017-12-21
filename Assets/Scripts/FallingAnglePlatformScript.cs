using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingAnglePlatformScript : MonoBehaviour
{

    public Rigidbody Ball;
    public Rigidbody FallingAnglePlatform;



    public DockManagementScript dmScript;
    public int lastFrameState;

    void Start()
    {

        Ball.useGravity = false;
        Ball.isKinematic = true;

        FallingAnglePlatform.useGravity = false;
        FallingAnglePlatform.isKinematic = true;



        lastFrameState = dmScript.currentState;
    }

    // Update is called once per frame
    void Update()
    {
        switch (dmScript.currentState)
        {
            case (int)DockManagementScript.states.backwards:

                break;
            case (int)DockManagementScript.states.pause:

                FallingAnglePlatform.useGravity = false;
                FallingAnglePlatform.isKinematic = true;
                Ball.useGravity = false;
                Ball.isKinematic = true;
                // Utilisation du pistolet pour enlever l'état pause à la balle
                // - ajout des conditions ici
                break;
            case (int)DockManagementScript.states.play:

                break;
            case (int)DockManagementScript.states.forward:


                break;

        }
    }



    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Ball.useGravity = true;
            Ball.isKinematic = false;
            FallingAnglePlatform.useGravity = true;
            FallingAnglePlatform.isKinematic = false;
        }
    }
}
