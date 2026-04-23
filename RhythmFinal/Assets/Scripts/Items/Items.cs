using System.Collections;
using UnityEngine;

public class Items : MonoBehaviour
{
    // Items is gonna be a class

    // Functions
    public string itemName;
    public string itemDescription;
    public int amount;
    public GameObject MusicGamePrefab;
    public GameObject worldUI;
    public int ID;
    bool canBeInteractedWith = true;

    public bool startsRhythmGame;
    
    public void PickUp(Inventory inventory)
    {
        Player player = GameObject.FindFirstObjectByType<Player>();

        if (player != null)
        {
            player.inRhythmGame = true;
            player.controlLock = true;
        }
        
        GameObject spawned = Instantiate(MusicGamePrefab, transform.position, Quaternion.identity);
        Lane lane = spawned.GetComponentInChildren<Lane>();
        if (lane != null)
        {
            lane.LaneID = ID;
        }

   
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canBeInteractedWith)
        {

            worldUI.SetActive(true);
        }
    }

    public void gameComplete()
    {
        worldUI.SetActive(false);
        this.gameObject .SetActive(false);
        canBeInteractedWith = false;
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            worldUI.SetActive(false);
        }
    }
    public IEnumerator CollectItem()
    {
        Destroy(gameObject);
        yield return null;
    }
}

