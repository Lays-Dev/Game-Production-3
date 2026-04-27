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
    public Camera minigameCamera;
    public GameObject fadeIn;
    public GameObject TeleportSpot;
    public GameObject Item;

    public bool startsRhythmGame;
    
    public void PickUp(Inventory inventory)
    {
        Player player = GameObject.FindFirstObjectByType<Player>();

        if (player != null) //This locks player movement
        {
            player.inRhythmGame = true;
            player.controlLock = true;
        }
        Animator FadeIn = GameObject.FindWithTag("FadeInGuy").gameObject.GetComponent<Animator>();

        FadeIn.SetTrigger("FadeIn");
        StartCoroutine(RhythmStart());
       
    }
    public IEnumerator RhythmStart()
    {
        yield return new WaitForSeconds(.3f); // Wait for the fade-in animation to complete
        worldUI.SetActive(false);
        Player player = GameObject.FindFirstObjectByType<Player>();
        player.transform.position = TeleportSpot.transform.position; // Teleport the player to the rhythm game area
        player.transform.LookAt(Item.transform.position); // Make the player look at the rhythm game
        player.transform.rotation = Quaternion.Euler(0, player.transform.rotation.eulerAngles.y, 0); // 
        GameObject spawned = Instantiate(MusicGamePrefab, transform.position, Quaternion.identity); //This is what spawns our selected rhythm game
        Lane lane = spawned.GetComponentInChildren<Lane>();
        minigameCamera.gameObject.SetActive(true);
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
        minigameCamera.gameObject.SetActive(false);

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
        minigameCamera.gameObject.SetActive(false);
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

