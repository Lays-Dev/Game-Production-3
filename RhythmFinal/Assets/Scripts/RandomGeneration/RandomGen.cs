using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RandomGen : MonoBehaviour
{
    public GameObject[] spawnSongs;
    public GameObject[] spawnPoints;
    public List<GameObject> spawned = new List<GameObject>();

    private List<GameObject> songPool = new List<GameObject>(); // tracks remaining songs to pick from

    void Start()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        StartCoroutine(SpawnRandomSong());
    }

    // How many items are still needed
    private int ItemsRemaining()
    {
        if (GameManager.instance != null)
            return 3 - (int)GameManager.instance.questItemsCollected;
        return 3; // fallback if no GameManager
    }

    // Refills the song pool if empty (prevents repeating until all used)
    private GameObject PickSong()
    {
        if (songPool.Count == 0)
        {
            songPool = new List<GameObject>(spawnSongs);
        }

        int index = Random.Range(0, songPool.Count);
        GameObject picked = songPool[index];
        songPool.RemoveAt(index); // remove so it won't repeat until pool is exhausted
        return picked;
    }

    public IEnumerator SpawnRandomSong()
    {
        int spawnAmount = ItemsRemaining(); // spawn exactly as many as needed
        int spawnCount = 0;

        // Shuffle a copy of spawnPoints to avoid retry loops
        List<GameObject> availablePoints = new List<GameObject>(spawnPoints);

        while (spawnCount < spawnAmount && availablePoints.Count > 0)
        {
            int randomIndex = Random.Range(0, availablePoints.Count);
            GameObject selectedPoint = availablePoints[randomIndex];
            availablePoints.RemoveAt(randomIndex); // remove so it won't be picked again

            GameObject pickedSong = PickSong();

            Debug.Log("Spawning: " + pickedSong.name + " at " + selectedPoint.name);
            spawned.Add(selectedPoint);
            spawnCount++;

            Instantiate(pickedSong, selectedPoint.transform.position, Quaternion.Euler(-90, selectedPoint.transform.rotation.eulerAngles.y, selectedPoint.transform.rotation.eulerAngles.z));
            yield return new WaitForSeconds(.2f);
        }
    }

    public void ResetGeneration()
    {
        GameObject[] songs = GameObject.FindGameObjectsWithTag("Song");
        foreach (GameObject go in songs)
            Destroy(go);

        spawned.Clear();
        StartCoroutine(SpawnRandomSong());
    }
    void Update()
    {
        if (Keyboard.current.rKey.isPressed)
            ResetGeneration();
    }

    // Move a song item to a new unoccupied spawn point
    public void MoveToNewSpawnPoint(GameObject songObject)
    {
        // Build list of unoccupied points
        List<GameObject> availablePoints = new List<GameObject>();
        foreach (GameObject point in spawnPoints)
        {
            if (!spawned.Contains(point))
                availablePoints.Add(point);
        }

        if (availablePoints.Count == 0)
        {
            Debug.Log("No available spawn points to move to");
            return;
        }

        // Pick a random unoccupied point
        int randomIndex = Random.Range(0, availablePoints.Count);
        GameObject newPoint = availablePoints[randomIndex];

        // Remove old point from spawned, add new one
        // Find which old point this song was at
        foreach (GameObject point in spawned)
        {
            if (Vector3.Distance(point.transform.position, songObject.transform.position) < 1f)
            {
                spawned.Remove(point);
                break;
            }
        }

        spawned.Add(newPoint);
        songObject.transform.position = newPoint.transform.position;
        Debug.Log("Moved song to: " + newPoint.name);
    }
}