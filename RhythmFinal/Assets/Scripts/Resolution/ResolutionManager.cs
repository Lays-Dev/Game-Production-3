using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    // for now only sets resolution to 1080p, more settings might be added in the future
    
    // Other notes:
    /*
        Might be better to make this script run at the very beginning when opening the game, but we don't have anything like that yet
        so I'm placing this as a reminder for when we do.
    */
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Screen.SetResolution(1920, 1080, true); //sets resolution to 1080p and fullscreen mode
        // true indicates the game is in fullscreen mode, you can set it to false for windowed mode
    }



}
