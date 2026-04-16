using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject optionsMenuUI; //Copy and paste as needed for more menus
    public int sceneIndex;

    private void Start()
    {
        ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        mainMenuUI.SetActive(true);
        optionsMenuUI.SetActive(false);
    }

    public void ShowOptionsMenu()
    {
        mainMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayGame(int newSceneIndex)
    {
        newSceneIndex = sceneIndex;
        SceneManager.LoadScene(sceneIndex);
    }
}