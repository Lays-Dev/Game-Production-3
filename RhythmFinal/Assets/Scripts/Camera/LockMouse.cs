using UnityEngine;

public class LockMouse : MonoBehaviour
{
    
    public bool cursorLocked = true;
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Update()
    {
        if (cursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked; // locks cursor
            Cursor.visible = false; // additionally makes it invisible on game window
        }
        else if (!cursorLocked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true; // make cursor visible again
        }
        

    }

    
}
