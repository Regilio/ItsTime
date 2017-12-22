using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoutonMenu : MonoBehaviour
{
    GameObject Player;
    private PauseScript pauseScript;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        if(Player!=null)
            pauseScript = Player.GetComponent<PauseScript>();
    }

    public void Load()
    {
        if (pauseScript != null)
        {
            pauseScript.Paused = false;
            pauseScript.Pause();
        }
        SceneManager.LoadScene(PlayerPrefs.GetInt("Scene"));
        Debug.Log("Loaded");
    }

    public void Save()
    {
        PlayerPrefs.SetInt("Scene", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.SetInt("Fragments", Player.GetComponent<FragmentTextScript>().nbFragments);
        PlayerPrefs.Save();
        Debug.Log("Saved");
    }

    public void Quitter()
    {
        if (pauseScript != null)
        {
            pauseScript.Paused = false;
            pauseScript.Pause();
        }
        Application.Quit();
    }

    public void LoadScene(int SceneNumber)
    {
        if(pauseScript != null)
        {
            pauseScript.Paused = false;
            pauseScript.Pause();
        }
        SceneManager.LoadScene(SceneNumber);
    }

    public void NewGame() //new game
    {
        PlayerPrefs.DeleteAll();
        Debug.Log(PlayerPrefs.GetInt("Fragments"));
        if (pauseScript != null)
        {
            pauseScript.Paused = false;
            pauseScript.Pause();
        }
        SceneManager.LoadScene(1);
     }
}
