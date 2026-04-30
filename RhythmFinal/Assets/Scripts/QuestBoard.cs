using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class QuestBoard : MonoBehaviour
{
    public GameObject questBoardUI; // Reference to the quest board UI
    public CinemachineCamera QuestBoardCamera; // Reference to the virtual camera
    public GameObject firstSelectedButton;

    public bool isOpen = false; // To track if the quest board is open or not


    
    
    public void openBoard(Player player)
    {
        isOpen = true;
        
        // Code to open quest board UI
        Debug.Log("Quest Board Opened");

        player.controlLock = true; // Lock player controls
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        QuestBoardCamera.gameObject.SetActive(true); // Activate the quest board camera
        player.playerCamera.gameObject.SetActive(false); // Deactivate the player's main camera
        

        questBoardUI.SetActive(true); // Show the quest board UI
        EventSystem.current.SetSelectedGameObject(firstSelectedButton);
        
    }

    public void closeBoard(Player player)
    {
        isOpen = false;
        
        // Code to close quest board UI
        Debug.Log("Quest Board Closed");

        player.controlLock = false; // Unlock player controls
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        QuestBoardCamera.gameObject.SetActive(false); // Deactivate the quest board camera
        player.playerCamera.gameObject.SetActive(true); // Reactivate the player's main camera

        questBoardUI.SetActive(false); // Hide the quest board UI
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!questBoardUI.activeSelf) return;
        
        if(questBoardUI.activeSelf)
        {
            if (EventSystem.current.currentSelectedGameObject == null && (Keyboard.current.anyKey.wasPressedThisFrame || Gamepad.current != null && Gamepad.current.wasUpdatedThisFrame))
            {
                // basically if no selected object in UI and any key/ gamepad button is pressed, set it to the first button.
                EventSystem.current.SetSelectedGameObject(firstSelectedButton);
            }
        }
    }
}
