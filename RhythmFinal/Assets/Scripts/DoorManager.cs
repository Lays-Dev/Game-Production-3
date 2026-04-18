using UnityEngine;
using UnityEngine.SceneManagement;


public class DoorManager : MonoBehaviour
{
    public string[] scenes;

    public void EnterLevel()
    {
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
