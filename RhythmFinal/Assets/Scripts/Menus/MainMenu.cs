using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
// SceneM

public class MainMenu : MonoBehaviour
{
    // PlayGame is for the button to start the game
    public void PlayGame()
    {
        // Async loads the game per frame and doesn't freeze the UI while loading
        SceneManager.LoadSceneAsync(2);
    }

    // Quit function
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit button clicked");
    }

    public void TutorialMenu()
    {
        // Async loads the game per frame and doesn't freeze the UI while loading
        SceneManager.LoadSceneAsync(1);
    }

        public void LoadMainMenu()
    {
        Debug.Log("Loading the main menu");
        Time.timeScale = 1f; // Ensure the game is unpaused before quitting
        AudioListener.pause = false; // Unpause audio before quitting
        Cursor.visible = true; // Show the cursor before quitting
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor before quitting
        SceneManager.LoadScene(0); // Load the main menu scene 
    }
    
}
