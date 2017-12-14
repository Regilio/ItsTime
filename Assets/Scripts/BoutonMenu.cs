using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoutonMenu : MonoBehaviour
{

    public void Load()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("Scene"));
        Debug.Log("Loaded");
    }

    public void Save()
    {
        PlayerPrefs.SetInt("Scene", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.Save();
        Debug.Log("Saved");
    }

    public void Quitter()
    {
        Application.Quit();
    }

  
    public void LoadScene(int SceneNumber)
     {
        SceneManager.LoadScene(SceneNumber);
     }
}
