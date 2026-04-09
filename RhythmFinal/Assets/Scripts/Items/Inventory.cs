using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public Dictionary<string, int> Items = new Dictionary<string, int>();

    public void AddItem(string itemName, int amount)
    {
        if (Items.ContainsKey(itemName))
        {
            Items[itemName] += amount; // If we already have one, add more items to the stack.
        }
        else
        {
            Items[itemName] = amount;
        }

        Debug.Log("Added item to inventory: " + itemName + " | Total: " + Items[itemName]);
    }
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
