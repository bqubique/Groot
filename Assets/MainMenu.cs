using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    [Header("Menu Screens")]
    public GameObject mainScreen;
    public GameObject playScreen;
    

	public void Play()
    {
        mainScreen.SetActive(false);
        playScreen.SetActive(true);
    }

    public void NewGame()
    {
        playScreen.SetActive(false);
        Application.LoadLevel("HasanLevel1");
    }

    public void LoadGame()
    {
        playScreen.SetActive(false);
        Application.LoadLevel("HasanLevel1");
    }

    public void Back2Main()
    {
        playScreen.SetActive(false);
        mainScreen.SetActive(true);
    }

    public void Exit()
    {
        Debug.Log("DENEME : EXIT SUCCESFULL");
        Application.Quit();
    }

}