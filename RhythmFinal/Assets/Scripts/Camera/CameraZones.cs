using UnityEngine;
using Unity.Cinemachine;

public class CameraZones : MonoBehaviour
{
    public CinemachineCamera virtualCamera; // Reference to the virtual camera for this zone

    private static CameraZones activeZone;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        // turn off previous zone
        if (activeZone != null && activeZone != this)
        {
            activeZone.virtualCamera.Priority = 0;
        }

        // activate this one
        activeZone = this; 
        virtualCamera.Priority = 20;

        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            player.currentCameraTransform = virtualCamera.transform;
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        // ONLY deactivate if this is current zone
        if (activeZone == this)
        {
            virtualCamera.Priority = 0;
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
