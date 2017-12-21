using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FragmentTextScript : MonoBehaviour
{
    public Text TxtFragment;
    public int nbFragments;

    // Use this for initialization
    void Start()
    {
        if (TxtFragment == null)            //Si on a oublie de mettre le text dans le public sur unity, alors il va le mettre ici:
            TxtFragment = GameObject.Find("fragmentText").GetComponent<Text>();
        nbFragments = PlayerPrefs.GetInt("Fragments");
        TxtFragment.text = "Fragments : " + nbFragments;
    }

    public void addFragment()
    {
        nbFragments = nbFragments + 1;
        TxtFragment.text = "Fragments : " + nbFragments;
        PlayerPrefs.SetInt("Fragments", nbFragments);
    }

}
