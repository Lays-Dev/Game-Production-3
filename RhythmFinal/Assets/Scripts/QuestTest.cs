using JetBrains.Annotations;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestTest : MonoBehaviour
{
    public TextMeshProUGUI QuestText;
    public float itemsCollected;
    public GameObject Spawner;
    public TextMeshProUGUI QuestTitle;
    public bool hasBeenCollected;
    public GameObject TreeWall;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RandomGen Spawner = this.GetComponent<RandomGen>();
        QuestTitle.text = "Song of the Caged Bird";
        
    }

    public IEnumerator collectItem()
    {
        if (hasBeenCollected == false)
        {
            itemsCollected++;
            hasBeenCollected = true;
        }
        yield return new WaitForSeconds(0.4f);
        hasBeenCollected = false;
        if (itemsCollected == 3)
        {
            TreeWall.SetActive(false);
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (itemsCollected == 3)
        {
            QuestText.text = "Go Speak To The Elder";
        }
        else
        {
            QuestText.text = "Collect " + itemsCollected + "/" + "3" + " Enchanted Planks";
        }
    }
}
