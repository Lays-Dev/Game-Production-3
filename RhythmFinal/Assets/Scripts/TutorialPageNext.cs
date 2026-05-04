using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TutorialPageNext : MonoBehaviour
{
    public GameObject currentPage;
    public GameObject nextPage;

    public GameObject firstSelectedButton;
    void Start()
    {
        UnlockMouse();

        EventSystem.current.SetSelectedGameObject(firstSelectedButton);
        
    }
    public void ClosePage()
    {
        currentPage.SetActive(false);
        LockMouse();
        
    }
    public void NextPage()
    {
        currentPage.SetActive(false);
        nextPage.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
