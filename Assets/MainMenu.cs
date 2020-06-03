using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    [Header("Menu Screens")]
    public GameObject mainScreen;
    

	public void Play()
    {
        mainScreen.SetActive(false);
        Application.LoadLevel("LeventLevel1");
    }

    public void Exit()
    {
        Debug.Log("DENEME : EXIT SUCCESFULL");
        Application.Quit();
    }

}