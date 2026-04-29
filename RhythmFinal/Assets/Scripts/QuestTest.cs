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
    public GameObject TreeWall;

    public int currentQuest = -1; // -1 means no quest, 0 means first quest, 1 means second quest, etc.
    public GameObject questUI;

    public void StartQuest(int questID)
    {
        currentQuest = questID;
        itemsCollected = 0;

        questUI.SetActive(true); // Show the quest UI

        switch (questID)
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
        }

    }

    void Start()
    {
        //Get quest title from GameManager
        RandomGen Spawner = this.GetComponent<RandomGen>();
        QuestTitle.text = "";

        // Load quest progress
        itemsCollected = (int)GameManager.instance.questItemsCollected;
        if (GameManager.instance.questCompleted)
            TreeWall.SetActive(false);

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
        if (itemsCollected == 3)
            TreeWall.SetActive(false);
    }

    void Update()
    {
        switch (currentQuest)
        {
            case -1:
                QuestText.text = "";
                break;
            case 0:
                if (itemsCollected == 3)
                    QuestText.text = "Go Speak To The Elder";
                else
                    QuestText.text = "Collect " + itemsCollected + "/3 Enchanted Planks";
                break;
            case 1:
                if (itemsCollected == 3)
                    QuestText.text = "Find the Sphinx";
                else
                    QuestText.text = "Collect " + itemsCollected + "/3 Mystic Sands";
                break;
            case 2:
                if (itemsCollected == 3)
                    QuestText.text = "Approach the Phantom Mammoth";
                else
                    QuestText.text = "Collect " + itemsCollected + "/3 Magical Rocks";
                break;
        }
        
        
    }
}