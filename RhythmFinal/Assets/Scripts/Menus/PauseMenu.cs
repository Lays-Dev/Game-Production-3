using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject pauseMenuPanel;

    [Header("Gameplay Input")]
    // PlayerInput is the input system the player will use to interact with the pause menu
    [SerializeField] private PlayerInput playerInput;  
    

    private bool isPaused = false;

    private void Awake()
    {
        // This if statment checks if the pause menu panel is unselected. It should be automatically unselected in the inspector.
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false);
        }

        isPaused = false;
        // Game starts unpaused
        Time.timeScale = 1f; 
        // Unpause audio when the game starts
        AudioListener.pause = false; 
        // Hide the cursor when the game starts
        Cursor.visible = false; 
        // Lock the cursor to the center of the screen when the game starts
        Cursor.lockState = CursorLockMode.Locked; 
    }

    private void Update()
    {
        // Check if the pause button is pressed
        if (playerInput.actions["Pause"].triggered)
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        Debug.Log("Pause button pressed");
        if (isPaused)
        {
            Debug.Log("Unpausing the game");
            ResumeGame();
        }
        else
        {
            Debug.Log("Pausing the game");
            PauseGame();
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // Freeze the game
        AudioListener.pause = true; // Pause audio
        Cursor.visible = true; // Show the cursor
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor

        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(true); // Show the pause menu UI
        }

        playerInput.SwitchCurrentActionMap("UI"); // Switch to UI input map

    }

    public void ResumeGame()
    {
        isPaused = false;

        Time.timeScale = 1f; // Unfreeze the game
        AudioListener.pause = false; // Unpause audio
        Cursor.visible = false; // Hide the cursor
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false); // Hide the pause menu UI
        }
        playerInput.SwitchCurrentActionMap("Player"); // Switch back to player input map
        Debug.Log("Resuming the game");
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Ensure the game is unpaused before quitting
        AudioListener.pause = false; // Unpause audio before quitting
        Cursor.visible = true; // Show the cursor before quitting
        Cursor.lockState = CursorLockMode.None; // Unlock the cursor before quitting
        SceneManager.LoadScene(0); // Load the main menu scene 
    }

}