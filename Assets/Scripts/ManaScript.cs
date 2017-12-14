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
    private bool consumption;
    public GameObject PauseCanvas;
    public bool Paused = false;

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
     //   Pause();
    }



    private void Update()
    {
        content.fillAmount = currentFill;

        if (Input.GetKeyDown(KeyCode.I))        //Quand on appuie sur I, on consomme/deconsomme
        {
            consumption = !consumption;
          //  Paused = !Paused;
        //    Pause();
        }
        if (consumption == true)
        {
            Debug.Log("on consomme");
            mana.MyCurrentValue -= Time.deltaTime * 8;

            //Mettre ici le changement de couleur de l'ecran pour montrer qu'on a active la pause
        }

        if (mana.MyCurrentValue <= 0)
        {
            Debug.Log("Plus de Mana");
            consumption = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PuitMana")
        {
            Debug.Log("Puit de Mana");
            mana.MyCurrentValue += Time.deltaTime * 15;
        }

        if (other.gameObject.tag == "Potion")
        {
            Debug.Log("Potion ramassee");
            mana.MyCurrentValue += 25;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PuitMana")
        {
            Debug.Log("Puit de Mana");
            mana.MyCurrentValue += Time.deltaTime * 15;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PuitMana")
        {
            Debug.Log("Sortie Puit de Mana");
            mana.MyCurrentValue = mana.MyCurrentValue;
        }
    }

    public void Initialize(float currentValue, float maxValue)
    {
        MyMaxValue = maxValue;
        MyCurrentValue = currentValue;
    }

  //  private void Pause()
   // {
    //    PauseCanvas.SetActive(Paused);
    //}

}
