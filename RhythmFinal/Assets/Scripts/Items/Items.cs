using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Items : MonoBehaviour
{
    // Items is gonna be a class

    // Functions
    public string itemName;
    public string itemDescription;
    public int amount;
    public GameObject MusicGamePrefab;
    public GameObject worldUI;
    public GameObject Camera;
    public Image Image;
    public bool startsRhythmGame;
    public Animator animator;

    public void PickUp(Inventory inventory)
    {
        Player player = GameObject.FindFirstObjectByType<Player>();

        if (player != null)
        {
            player.inRhythmGame = true;
            player.controlLock = true;
        }
        animator.SetTrigger("FadeIn");
        StartCoroutine(RhythmGame());
        



    }
    public IEnumerator RhythmGame()
    {
        yield return new WaitForSeconds(1.75f);
        Instantiate(MusicGamePrefab, transform.position, Quaternion.identity);
        Camera.SetActive(true);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            worldUI.SetActive(true);
        }
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

