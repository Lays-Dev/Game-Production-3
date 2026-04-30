using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Quest - Song of the Caged Bird")]
    public float questItemsCollected;
    public bool questCompleted;
    public bool ViolinCraft;
    public bool HarpCraft;
    public bool HornCraft;

    public int activeQuest = -1;

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
        if(ViolinCraft == true)
        {
            Debug.Log("Violin Crafted");    
        }
        if(HarpCraft == true)
            {
                Debug.Log("Harp Crafted");    
            }
            if(HornCraft ==  true)
            {
                Debug.Log("Horn Crafted");
        }
    }

    public void LoadGame()
    {
        SaveData data = SaveSystem.LoadGame();
        if (data == null) return;

        // Quest data
        questItemsCollected = data.questItemsCollected;
        questCompleted = data.questCompleted;
        ViolinCraft = data.ViolinCraft;
        HarpCraft = data.HarpCraft;
        HornCraft = data.HornCraft;

        Debug.Log("Game Loaded Successfully");
    }
    public void ResetSave()
    {
        questItemsCollected = 0;
        questCompleted = false;
        ViolinCraft = false;
        HarpCraft = false;
        HornCraft = false;

        SaveGame();
        Debug.Log("Quest progress reset");
    }
}