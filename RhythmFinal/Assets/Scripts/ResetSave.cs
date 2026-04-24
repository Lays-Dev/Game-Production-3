using UnityEngine;

public class ResetSave : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.ResetSave();
            Debug.Log("Save data reset");
        }
    }
}