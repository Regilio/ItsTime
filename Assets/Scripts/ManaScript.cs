using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaScript : MonoBehaviour
{
	private Image manaBar;
	private float currentFill;
	public float maxMana = 100;
	private float currentMana;
	public bool consumption;
	public float consumptionSpeed = 8;
	public float vitessePuits = 15;
	public float valeurPotion = 25;
	public DockManagementScript dockManagementScript;

	public Color manaColor;

	private void Awake()
	{
		manaBar = GameObject.FindGameObjectWithTag("ManaBar").GetComponent<Image>();
	}

	private void Start()
	{
		currentMana = maxMana;
		SetMana();
		consuming(false);

	}

	public void consuming(bool isConsuming)
	{
		consumption = isConsuming;
		if (consumption == true)
		{
			//Debug.Log("on consomme");
			manaBar.color = manaColor;
			StopAllCoroutines();
			StartCoroutine(Consume());
		}
		else
		{
			manaBar.color = Color.white;
			StopAllCoroutines();
		}
	}

	IEnumerator Consume()
	{
		while (true)
		{
			currentMana -= Time.deltaTime * consumptionSpeed;

			SetMana();

			if (currentMana <= 0)
			{
				//Debug.Log("Plus de Mana");
				consuming(false);
				dockManagementScript.Play();
				currentMana = 0;
			}
			yield return 0;

		}
	}

	private void SetMana() {

		currentFill = currentMana / maxMana;
		manaBar.fillAmount = currentFill;
	}


	public void ChangeMana(float mana){
		currentMana = mana;
		SetMana ();
	}

	private void OnTriggerEnter(Collider other)
	{

		if (other.gameObject.tag == "Potion")
		{

			//Debug.Log("Potion ramassée");
			currentMana += valeurPotion;
			if (currentMana > maxMana)
				currentMana = maxMana;
			other.gameObject.SetActive (false);


			SetMana();
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "PuitsMana")
		{
			dockManagementScript.Play();
			//Debug.Log("Puits de Mana");
			currentMana += Time.deltaTime * vitessePuits;
			manaBar.color = manaColor;
			if (currentMana > maxMana)
				currentMana = maxMana;

			SetMana();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "PuitsMana")
		{
			dockManagementScript.Play();
			//Debug.Log("Puits de Mana");
			manaBar.color = Color.white;
		}
	}



}