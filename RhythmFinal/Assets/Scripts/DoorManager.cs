using UnityEngine;
using UnityEngine.SceneManagement;


public class DoorManager : MonoBehaviour
{
    public string[] scenes;

    public string TutorialLevel = "TutorialLevel";
    public bool isReturnDoor = false;

    public void EnterLevel()
    {
        
        if(isReturnDoor)
        {
            SceneManager.LoadScene(TutorialLevel);
        }

        if (LevelState.Instance == null)
        {
            Debug.LogError("LevelState is NULL!");
            return;
        }

        if (!LevelState.Instance.levelSelected) // extra safety check
        {
            Debug.Log("No level selected yet!");
            return;
        }
        
        int level = LevelState.Instance.selectedLevel;

        if (level >= 0 && level < scenes.Length)
        {
            SceneManager.LoadScene(scenes[level]);
        }
    }
    
}
