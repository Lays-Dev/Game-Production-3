using UnityEngine;

public class Items : MonoBehaviour
{
    // Items is gonna be a class

    // Functions
    public string itemName;
    public string itemDescription;
    public int amount;
    
    
    public void PickUp(Inventory inventory)
    {
        inventory.AddItem(itemName, amount); // adds to inventory
        Destroy(gameObject); // get rid of the model, smithing girl is strong but we don't want the game to look like Death Stranding.
    }
    
}
