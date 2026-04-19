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
        SceneManager.LoadSceneAsync(1);
    }
}
