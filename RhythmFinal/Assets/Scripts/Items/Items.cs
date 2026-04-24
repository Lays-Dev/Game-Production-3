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
    public RandomGen spawner;

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

    public void Start()
    {
        spawner = FindAnyObjectByType<RandomGen>();
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
        canBeInteractedWith = false;

        // Remove the spawn point that matches this item's position
        GameObject pointToRemove = null;
        foreach (GameObject point in spawner.spawned)
        {
            if (Vector3.Distance(point.transform.position, transform.position) < 1f)
            {
                pointToRemove = point;
                break;
            }
        }
        if (pointToRemove != null)
            spawner.spawned.Remove(pointToRemove);

        this.gameObject.SetActive(false);
    }

    public void gameFailed()
    {
        spawner.MoveToNewSpawnPoint(this.gameObject);
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

