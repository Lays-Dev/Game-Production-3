using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class RandomGen : MonoBehaviour
{
    
    public GameObject[] spawnSongs;
    public GameObject[] spawnPoints;
    public float spawnCount = 0f;
    public float spawnAmount = 4f;
    public List<GameObject> spawned = new List<GameObject>();
    public InputAction resetGenerationAction;

    void Start()
    {
        //On start this finds all gameobjects tagged with spawnpoint and adds them to the spawnpoints array, then starts the coroutine that spawns the songs
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        StartCoroutine(SpawnRandomSong());
    }

    public IEnumerator SpawnRandomSong()
    {
        
        while (spawnCount < spawnAmount) //By changing spawn amount we can alter how many objects the player must interact with.
        {
            //This picks a random index from the spawnpoints array, checks if it has already been picked, if not it spawns the song and adds it to the spawned list, if it has already been picked it logs that and tries again.
            int randomIndex = Random.Range(0, spawnPoints.Length);
            GameObject selectedItem = spawnPoints[randomIndex];
            int pickSong = Random.Range(0, spawnSongs.Length);
            GameObject pickedSong = spawnSongs[pickSong];
            if (spawned.Contains(selectedItem) == false)
            {
                Debug.Log("Picked: " + selectedItem.name);
                spawned.Add(selectedItem);
                spawnCount++;
                Instantiate(pickedSong, selectedItem.transform.position, Quaternion.identity);
                yield return new WaitForSeconds(.2f);
            }
            else 
                Debug.Log("Already picked: " + selectedItem.name);
            yield return new WaitForSeconds(.2f);


           
        }
    }

    public void Update()
    {
        //HARDCODED KEYBOARD INPUT TO RESET THE GENERATION, THIS DESTROYS ALL OBJECTS WITH THE SONG TAG, RESETS THE SPAWN COUNT AND SPAWNED LIST, THEN STARTS THE COROUTINE AGAIN.
        if (Keyboard.current.rKey.isPressed)
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Song");
            foreach (GameObject go in gameObjects)
            {
                Destroy(go);
            }
            spawnCount = 0f;
            spawned.Clear();
            StartCoroutine(SpawnRandomSong());
        }
    }
}
