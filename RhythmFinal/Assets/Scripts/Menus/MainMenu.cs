using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
// SceneM

public class MainMenu : MonoBehaviour
{
    public GameObject firstButton;

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(firstButton);
    }

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
        SceneManager.LoadScene(0); // Load the main menu scene 
    }

    public void CreditsMenu()
    {
        SceneManager.LoadSceneAsync(4);
    }
    
}
