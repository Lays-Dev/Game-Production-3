using UnityEngine;
using UnityEngine.SceneManagement;

public class SkyBoxChanger : MonoBehaviour
{
    public Material[] skyboxes; // Array to hold different skybox materials
    public int sceneIndex; // Index of the scene selected to go through
    public GameObject DialCamera; // Reference to the camera that will be enabled when player is in range of the skybox changer
    public GameObject DialUI;
    public GameObject Player;
    public Animator resetLever;// Reference to the player game object to disable it when the camera is enabled

    public void enableCamera() // enables the camera when player is in range of the skybox changer
    {
        DialCamera.SetActive(true);
        resetLever.SetTrigger("Reset");
        Player.SetActive(false); // Disable the player game object to prevent movement and interactions while the camera is active
        DialUI.SetActive(true); // Enable the UI to show the skybox selection options

    }
    public void disableCamera() // disables the camera when player is out of range of the skybox changer
    {
        DialCamera.SetActive(false); // Disable the camera when player is out of range of the skybox changer
        
        Debug.Log("Playerback");
        Player.SetActive(true); // Enable the player game object to allow movement and interactions again
        DialUI.SetActive(false); // Disable the UI to hide the skybox selection options
    }   

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
    public void ChangeSkyboxBackwards() // changes skybox
    {
        if (skyboxes.Length == 0 || skyboxes == null) return; // Check if there are any skyboxes in the array

        sceneIndex--;
        if (sceneIndex < 0) // Loop back to the last skybox if we go below the array length
        {
            sceneIndex = skyboxes.Length - 1;
        }

        LevelState.Instance.selectedLevel = sceneIndex; // Set the selected level in the LevelState singleton to the scene index of this skybox changer
        LevelState.Instance.levelSelected = true; // Set levelSelected to true to allow player to go through door

        Debug.Log("Skybox changed to: " + skyboxes[sceneIndex].name); // Log the name of the new skybox
        RenderSettings.skybox = skyboxes[sceneIndex]; // Set the new skybox based on the skybox of the chosen level

        DynamicGI.UpdateEnvironment(); // Update the environment to apply the new skybox
    }

}
