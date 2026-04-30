using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialPageNext : MonoBehaviour
{
    public GameObject currentPage;
    public GameObject nextPage;
    public PlayerInput playerInput;
    void Start()
    {
        Cursor.lockState=CursorLockMode.None;
        Cursor.visible=true;
        playerInput.actions.FindActionMap("UI").Enable();
    }
    public void ClosePage()
    {
        currentPage.SetActive(false);
        
    }
    public void NextPage()
    {
        Debug.Log("Next Page");
        currentPage.SetActive(false);
        nextPage.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
