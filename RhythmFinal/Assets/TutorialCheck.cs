using UnityEngine;

public class TutorialCheck : MonoBehaviour
{
    public GameObject page1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GameManager.instance.tutorialCompleted)
        {
            page1.SetActive(false);
        }
        else if (!GameManager.instance.tutorialCompleted)
        {
            page1.SetActive(true);
            GameManager.instance.tutorialCompleted = true;
            GameManager.instance.SaveGame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
