using System.Collections;
using TMPro;
using UnityEngine;

public class QuestTest : MonoBehaviour
{
    public TextMeshProUGUI QuestText;
    public int itemsCollected;
    public GameObject Spawner;
    public TextMeshProUGUI QuestTitle;
    public bool hasBeenCollected;
    public bool rockWallGone;
    public bool gemWallGone;
    public bool treeWallGone;


    public int currentQuest = -1; // -1 means no quest, 0 means first quest, 1 means second quest, etc.
    public GameObject questUI;

    public void StartQuest(int questID)
    {
        currentQuest = questID;
        GameManager.instance.activeQuest = questID;

        itemsCollected = 0;

        questUI.SetActive(true); // Show the quest UI

        switch (currentQuest)
        {
            case 0:
                QuestTitle.text = "Song of the Caged Bird";
                break;
            case 1:
                QuestTitle.text = "Tomorrow's Dust";
                break;
            case 2:
                QuestTitle.text = "The Pink Phantom";
                break;
            
            default:
                QuestTitle.text = "";
                break;
        }

    }

    void Start()
    {
        currentQuest = GameManager.instance.activeQuest;
        switch (currentQuest)
        {
            case 0:
                QuestTitle.text = "Song of the Caged Bird";
                break;

            case 1:
                QuestTitle.text = "Tomorrow's Dust";
                break;

            case 2:
                QuestTitle.text = "The Pink Phantom";
                break;

            default:
                QuestTitle.text = "";
                break;
        }
        
        //Get quest title from GameManager
        RandomGen Spawner = this.GetComponent<RandomGen>();

        // Load quest progress
        itemsCollected = (int)GameManager.instance.questItemsCollected;
        if (GameManager.instance.questCompleted)
        {
            if(treeWallGone == true)
            {
                GameObject TreeWall = GameObject.FindWithTag("TreeWall");
                TreeWall.SetActive(false);
            }
        }

        // Respawn minigames based on remaining items
        if (Spawner != null)
            StartCoroutine(Spawner.SpawnRandomSong());
    }

    public IEnumerator collectItem()
    {
        //Check if the item has already been collectedm
        if (hasBeenCollected == false)
        {
            itemsCollected++;
            hasBeenCollected = true;

            //Save progress after collecting an item
            GameManager.instance.questItemsCollected = itemsCollected;
            if (itemsCollected >= 3)
                GameManager.instance.questCompleted = true;
            GameManager.instance.SaveGame();
        }

        yield return new WaitForSeconds(0.4f);
        hasBeenCollected = false;
                    
    }

    void Update()
    {
        switch (GameManager.instance.activeQuest)
        {
            case 0:
                if (GameManager.instance.violinCraftReady == true)
                {
                    QuestText.text = "Craft the violin";
                }
                else if (itemsCollected == 3)
                {
                    QuestText.text = "Go Speak To The Elder";
                    if (treeWallGone == false)
                    {
                        GameObject TreeWall = GameObject.FindWithTag("TreeWall");
                        if (TreeWall != null)
                            TreeWall.SetActive(false);
                        GameManager.instance.ViolinCraft = true;
                        GameManager.instance.HarpCraft = false;
                        GameManager.instance.HornCraft = false;
                        GameManager.instance.SaveGame();
                        treeWallGone = true;
                    }
                    
                }
                else
                    QuestText.text = "Collect " + itemsCollected + "/3 Enchanted Planks";

                break;
            case 1:
                if (GameManager.instance.harpCraftReady == true)
                {
                    QuestText.text = "Craft the harp";
                }
                else if (itemsCollected == 3)
                {
                    QuestText.text = "Find the Sphinx";
                    if (rockWallGone == false)
                    {
                        GameObject Rockwall = GameObject.FindWithTag("Rockwall");
                        Rockwall.SetActive(false);
                        GameManager.instance.ViolinCraft = false;
                        GameManager.instance.HarpCraft = true;
                        GameManager.instance.HornCraft = false;
                        GameManager.instance.SaveGame();
                        rockWallGone = true;
                    }
                }

                else
                    QuestText.text = "Collect " + itemsCollected + "/3 Mystic Sands";
                break;
            case 2:
                if (GameManager.instance.hornCraftReady == true)
                {
                    QuestText.text = "Craft the horn";
                }
                else if (itemsCollected == 3)
                {
                    QuestText.text = "Approach the Phantom Mammoth";
                    if (gemWallGone == false)
                    {
                        GameObject GemWall = GameObject.FindWithTag("GemWall");
                        GemWall.SetActive(false);
                        GameManager.instance.ViolinCraft = false;
                        GameManager.instance.HarpCraft = false;
                        GameManager.instance.HornCraft = true;
                        GameManager.instance.SaveGame();
                        gemWallGone = true;
                    }   
                }
                else
                    QuestText.text = "Collect " + itemsCollected + "/3 Magical Rocks";
                break;
        }
        
        
    }
}