using UnityEngine;
using UnityEngine.InputSystem;

public class ExitToMainMenu : MonoBehaviour
{
    
    public PauseMenu pauseMenuScript;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Gamepad.all.Count > 0 && Gamepad.current.selectButton.wasPressedThisFrame || Gamepad.all.Count > 0 && Gamepad.current.buttonEast.wasPressedThisFrame)
        {
            GameObject gameManager = GameObject.FindWithTag("GameManager");
            gameManager.GetComponent<GameManager>().ResetSave();
            pauseMenuScript.LoadMainMenu();
            
        }
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            GameObject gameManager = GameObject.FindWithTag("GameManager");
            gameManager.GetComponent<GameManager>().ResetSave();
            pauseMenuScript.LoadMainMenu();
            
        }
        
    }
}
