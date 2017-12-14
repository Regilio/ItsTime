using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private ManaScript mana;

    private float initMana = 100;


    // Use this for initialization
    void Start()
    {
        mana.Initialize(initMana, initMana);
    }

    // Update is called once per frame
    void Update()
    {
        //La partie suivante est pour tester
        if (Input.GetKey(KeyCode.I))
        {
            Debug.Log("Touche I");
            mana.MyCurrentValue -= Time.deltaTime*5;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log("Touche O");
            mana.MyCurrentValue = 100;

            // Fin de la partie test
        }
    }



}
