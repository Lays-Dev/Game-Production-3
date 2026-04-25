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
            pauseMenuScript.LoadMainMenu();
        }
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            pauseMenuScript.LoadMainMenu(); 
        }
        
    }
}
