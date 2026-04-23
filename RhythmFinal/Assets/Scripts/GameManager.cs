using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Quest - Song of the Caged Bird")]
    public float questItemsCollected;
    public bool questCompleted;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadGame();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveGame()
    {
        SaveSystem.SaveGame(this);
    }

    public void LoadGame()
    {
        SaveData data = SaveSystem.LoadGame();
        if (data == null) return;

        // Quest data
        questItemsCollected = data.questItemsCollected;
        questCompleted = data.questCompleted;

        Debug.Log("Game Loaded Successfully");
    }
}