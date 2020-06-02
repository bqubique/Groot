using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PMenu : MonoBehaviour
{
	public GameObject pauseMenuUI;
    public static bool IsPaused = false;
    Player p;
    private void Start()
    {
        pauseMenuUI.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
        	if(IsPaused){
        		Continue();
        	}
        	else{
        		pauseMenuUI.SetActive(true);
    			Time.timeScale = 0f;
    			IsPaused = true;
        	}
        }
    }

    public void Continue(){
    	pauseMenuUI.SetActive(false);
    	Time.timeScale = 1f;
    	IsPaused = false;
    }

    public void Exit(){
    	//save
    	//SavernLoader.Save(p);
    	//Time.timescale = 1f;
    	//SceneManager.LoadScene(0);
    }
	
}
