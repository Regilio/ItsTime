using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaScript : MonoBehaviour
{
    private Image content;
    private float currentFill;
    public float MyMaxValue { get; set; }
    private float currentValue;
    private float initMana = 100;
    public ManaScript mana;
    public bool consumption;
    public float consumptionSpeed = 8;
    public float vitessePuits = 15;
    public float valeurPotion = 25;

    public float MyCurrentValue
    {
        get
        {
            return currentValue;
        }

        set
        {
            if (value > MyMaxValue)
            {
                currentValue = MyMaxValue;
            }
            else if (value < 0)
            {
                currentValue = 0;

            }
            else
            {
                currentValue = value;
            }

            currentFill = currentValue / MyMaxValue;
        }
    }

    private void Start()
    {
        content = GetComponent<Image>();
        mana.Initialize(initMana, initMana);
        consumption = false;

    }



    private void Update()
    {
        content.fillAmount = currentFill;

        if (consumption == true)
        {
            //Debug.Log("on consomme");
            mana.MyCurrentValue -= Time.deltaTime * consumptionSpeed;
        }

        if (mana.MyCurrentValue <= 0)
        {
            //Debug.Log("Plus de Mana");
            consumption = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PuitsMana")
        {
            Debug.Log("Puits de Mana");
            mana.MyCurrentValue += Time.deltaTime * vitessePuits;
        }

        if (other.gameObject.tag == "Potion")
        {
            Debug.Log("Potion ramassée");
            mana.MyCurrentValue += valeurPotion;
			Destroy (other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PuitMana")
        {
            Debug.Log("Puits de Mana");
            mana.MyCurrentValue += Time.deltaTime * vitessePuits;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PuitsMana")
        {
            Debug.Log("Sortie Puits de Mana");
            mana.MyCurrentValue = mana.MyCurrentValue;
        }
    }

    public void Initialize(float currentValue, float maxValue)
    {
        MyMaxValue = maxValue;
        MyCurrentValue = currentValue;
    }



}
