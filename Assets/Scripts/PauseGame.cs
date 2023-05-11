using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
public class PauseGame : MonoBehaviour
{
    // Reference to the game object that contains the pause menu
    public GameObject pauseMenu;
    public Button StopGame;
    public Button ContinueGame;
    public Button BacktoMenu;
    public Button ReStart;
    public RewardedAdsButton rewardedAdsButton;
    public static bool gameIsPaused = false;

    private void Awake()
    {
        StopGame.onClick.AddListener(Pause);
        ContinueGame.onClick.AddListener(ResumeGame);
        BacktoMenu.onClick.AddListener(BackToMenu);
        ReStart.onClick.AddListener(RestartGame);
    }
    public void Pause()
    {
        // Stop the game time and set the gameIsPaused variable to true
        Time.timeScale = 0;     
        Debug.Log("Stop");
        
        // Show the pause menu and disable the stop game button
        pauseMenu.SetActive(true); 
        gameIsPaused = true;    
    }

    public void ResumeGame()
    {
        // Resume the game time and set the gameIsPaused variable to false
        Time.timeScale = 1;
       

        // Hide the pause menu and enable the stop game button
        pauseMenu.SetActive(false);
        gameIsPaused = false;
    }
    public void RestartGame()
    { 
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameIsPaused = false;
        
        Debug.Log("Restart Scean");     
    }
   

    public void BackToMenu()
    {
        try {
        
        // Load the main menu scene
        SceneManager.LoadScene("MainMenu"); 
        }
        catch (Exception e)
        {
            Debug.Log("Object Catch = " + e);
        }
    }
}