using UnityEngine;

public class ColliderSpawn : MonoBehaviour
{
    public GameObject MusicGamePrefab;
 
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Instantiate(MusicGamePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
 