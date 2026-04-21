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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RandomGen Spawner = this.GetComponent<RandomGen>();
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
        
    }
    // Update is called once per frame
    void Update()
    {
        QuestTitle.text = "Quest Tracker";
        QuestText.text = "Collect " + itemsCollected + "/" + "4" + " Enchanted Planks";
    }
}
