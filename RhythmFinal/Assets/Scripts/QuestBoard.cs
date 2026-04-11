using UnityEngine;

public class QuestBoard : MonoBehaviour
{
    public GameObject questBoardUI; // Reference to the quest board UI
    public Camera QuestBoardCamera; // Reference to the camera
    
    
    public void openBoard()
    {
        // Code to open quest board UI
        Debug.Log("Quest Board Opened");

        questBoardUI.SetActive(true); // Show the quest board UI
        
    }

    public void closeBoard()
    {
        // Code to close quest board UI
        Debug.Log("Quest Board Closed");

        questBoardUI.SetActive(false); // Hide the quest board UI
    }




    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
