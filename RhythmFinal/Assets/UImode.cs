using UnityEngine;
using UnityEngine.InputSystem;

public class UImode : MonoBehaviour
{
    public PlayerInput playerInput; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput.SwitchCurrentActionMap("UI");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
