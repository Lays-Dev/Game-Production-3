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
    public bool crafting;
    public int activeQuest = 0;
    public bool hornCraftReady;
    public bool harpCraftReady;
    public bool violinCraftReady;
    public bool ViolinCraftComplete;
    public bool HarpCraftComplete;
    public bool HornCraftComplete;
    public bool tutorialCompleted;

    public SkyBoxChanger skyBoxChanger;

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
        SaveSystem.SaveGame(this);
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
        activeQuest = data.activeQuest;
        crafting = data.crafting;
        hornCraftReady = data.hornCraftReady;
        harpCraftReady = data.harpCraftReady;
        violinCraftReady = data.violinCraftReady;
        ViolinCraftComplete = data.ViolinCraftComplete;
        HarpCraftComplete = data.HarpCraftComplete;
        HornCraftComplete = data.HornCraftComplete;
        tutorialCompleted = data.tutorialCompleted;
        Debug.Log("Game Loaded Successfully");
    }
    public void ResetSave()
    {
        questItemsCollected = 0;
        questCompleted = false;
        ViolinCraft = false;
        HarpCraft = false;
        HornCraft = false;
        crafting = false;
        harpCraftReady = false;
        hornCraftReady = false;
        violinCraftReady = false;
        ViolinCraftComplete = false;    
        HarpCraftComplete = false;
        HornCraftComplete = false;


        SaveGame();
        Debug.Log("Quest progress reset");
    }


}