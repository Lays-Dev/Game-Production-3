using UnityEngine;
using Unity.Cinemachine;

public class CameraZones : MonoBehaviour
{
    public CinemachineCamera virtualCamera; // Reference to the virtual camera for this zone

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            virtualCamera.Priority = 20; // Set a higher priority to activate this camera
        }
        
        
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            player.currentCameraTransform = virtualCamera.transform;
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            virtualCamera.Priority = 0; // Reset the priority when the player exits the zone
        }
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
