using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WalkOnGrass : MonoBehaviour
{
    private AudioClip currentSFX;
    public Player playerMovement;


    public List<AudioClip> footstepSounds; // List of footstep sounds to choose from

    public void Footstep()
    {
        if(playerMovement.movementInput.magnitude > 0.1f) // checks if player is moving
        {
            currentSFX = footstepSounds[Random.Range(0, footstepSounds.Count)]; // randomly selects a footstep sound from the list
            AudioManager.instance.PlaySFX(currentSFX); // plays the sound effect at half volume
        }

    }
   
}
