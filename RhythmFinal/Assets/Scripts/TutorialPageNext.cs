using UnityEngine;

public class TutorialPageNext : MonoBehaviour
{
    public GameObject currentPage;
    public GameObject nextPage;
    void Start()
    {
        Cursor.lockState=CursorLockMode.Locked;
        Cursor.visible=false;
    }
    public void ClosePage()
    {
        currentPage.SetActive(false);
        
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
}
