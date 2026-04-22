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
    
    public void PickUp(Inventory inventory)
    {
        Debug.Log("hello");
        Instantiate(MusicGamePrefab, transform.position, Quaternion.identity);

   
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

