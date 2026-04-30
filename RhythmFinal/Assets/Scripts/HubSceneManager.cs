using UnityEngine;

public class HubSceneManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (LevelState.Instance != null)
        {
            LevelState.Instance.selectedLevel = 0;
            LevelState.Instance.levelSelected = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
