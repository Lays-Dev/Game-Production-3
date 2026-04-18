using UnityEngine;

public class LevelState : MonoBehaviour
{
    // script must be placed on an object in the scene, preferably empty, and it will hold the state of the level, such as which level is currently selected. This is used to pass information between scenes, such as which skybox to use in the rhythm game scene based on the player's choice in the hub world.
    
    public static LevelState Instance;

    public int selectedLevel = 0; // This will hold the index of the selected level, set by the SkyBoxChanger
    public bool levelSelected = false; // prevents player from going through door without first selecting a level
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;   
        }
        else
        {
            Destroy(gameObject);
        }

    }

}
