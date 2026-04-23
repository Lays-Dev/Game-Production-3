using UnityEngine;
using UnityEngine.SceneManagement;

public class SkyBoxChanger : MonoBehaviour
{
    public Material[] skyboxes; // Array to hold different skybox materials
    public int sceneIndex; // Index of the scene selected to go through
    

    
    public void ChangeSkybox() // changes skybox
    {
        if (skyboxes.Length == 0 || skyboxes == null) return; // Check if there are any skyboxes in the array

        sceneIndex++;
        if (sceneIndex >= skyboxes.Length) // Loop back to the first skybox if we exceed the array length
        {
            sceneIndex = 0;
        }

        LevelState.Instance.selectedLevel = sceneIndex; // Set the selected level in the LevelState singleton to the scene index of this skybox changer
        LevelState.Instance.levelSelected = true; // Set levelSelected to true to allow player to go through door

        Debug.Log("Skybox changed to: " + skyboxes[sceneIndex].name); // Log the name of the new skybox
        RenderSettings.skybox = skyboxes[sceneIndex]; // Set the new skybox based on the skybox of the chosen level

        DynamicGI.UpdateEnvironment(); // Update the environment to apply the new skybox
    }
    
}
