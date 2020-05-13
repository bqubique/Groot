using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    [Header("Menu Screens")]
    public GameObject mainScreen;
    public GameObject startScreen;
    public GameObject newLevelScreen;
    public GameObject loadLevelScreen;
    

	public void StartScreen()
    {
        mainScreen.SetActive(false);
        startScreen.SetActive(true);
    }

    public void NewLevel()
    {
        startScreen.SetActive(false);
        newLevelScreen.SetActive(true);
        //Application.LoadLevel("FirstLevel");
    }

    public void LoadLevel()
    {
        startScreen.SetActive(false);
        loadLevelScreen.SetActive(true);
    }

    public void back2Menu()
    {
        startScreen.SetActive(false);
        loadLevelScreen.SetActive(false);
        newLevelScreen.SetActive(false);
        mainScreen.SetActive(true);
    }

    public void Exit()
    {
        Debug.Log("DENEME : EXIT SUCCESFULL");
        Application.Quit();
    }

}