using UnityEngine;

public class ColliderSpawn : MonoBehaviour
{
    public GameObject MusicGamePrefab;

    public void PickUp(Inventory inventory)
    {
        Instantiate(MusicGamePrefab, transform.position, Quaternion.identity);
    }
    
}
 