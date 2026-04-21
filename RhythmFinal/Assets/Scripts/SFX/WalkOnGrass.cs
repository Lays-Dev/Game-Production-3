using UnityEngine;
using System.Collections;

public class WalkOnGrass : MonoBehaviour
{
    public AudioClip walkOnGrassSFX;
    private Player playerMovement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovement = GetComponent<Player>();
        StartCoroutine(walkOnGrass());
    }

    IEnumerator walkOnGrass()
    {
        while(true)
        {
            if(playerMovement.movementInput.magnitude > 0.1f) // checks if player is moving
            {
                AudioManager.instance.PlaySFX(walkOnGrassSFX); // plays the sound effect at half volume
            }

            yield return new WaitForSeconds(0.5f); // waits for 0.5 seconds before checking again, adjust as needed for desired frequency of sound effect
        }
    }

   
}
