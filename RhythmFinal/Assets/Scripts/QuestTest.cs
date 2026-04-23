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

    void Start()
    {
        // Get quest title from GameManager
        RandomGen Spawner = this.GetComponent<RandomGen>();
        QuestTitle.text = "Song of the Caged Bird";

        //Load quest progress
        itemsCollected = (int)GameManager.instance.questItemsCollected;
        if (GameManager.instance.questCompleted)
            TreeWall.SetActive(false);
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
        if (itemsCollected == 3)
            QuestText.text = "Go Speak To The Elder";
        else
            QuestText.text = "Collect " + itemsCollected + "/3 Enchanted Planks";
    }
}