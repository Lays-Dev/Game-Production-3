using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction pauseAction;

    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private bool isPaused;

    private void Awake()
    {
        playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        pauseAction = playerInput.UI.Pause;
        pauseAction.Enable();
        pauseAction.performed += TogglePause;
    }

    private void OnDisable()
    {
        pauseAction.performed -= TogglePause;
        pauseAction.Disable();
    }

// context can be named anything
    public void TogglePause(InputAction.CallbackContext context)
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            ActivatePauseMenu();
    
        }
        else
        {
            DeactivatePauseMenu();
        }
    }

    void ActivatePauseMenu()
    {
        Time.timeScale = 0f;
        AudioListener.pause = true;
        pauseMenuUI.SetActive(true);
    }

    void DeactivatePauseMenu()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        pauseMenuUI.SetActive(false);
    }

}