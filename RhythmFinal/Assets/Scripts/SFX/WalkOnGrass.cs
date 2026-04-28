using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WalkOnGrass : MonoBehaviour
{
    private FootstepSound currentSFX;
    public Player playerMovement;
    [System.Serializable]
    public struct FootstepSound
    {
        public AudioClip sound;
        public float volume;
    }

    public List<FootstepSound> footstepSounds; // List of footstep sounds to choose from

    public void Footstep()
    {
        if (playerMovement.movementInput.magnitude > 0.1f)
        {
            currentSFX = RandomSFX(footstepSounds);
            AudioManager.instance.PlaySFX(currentSFX.sound, currentSFX.volume);
        }
    }

    private FootstepSound RandomSFX(List<FootstepSound> sounds)
    {
        return sounds[Random.Range(0, sounds.Count)];
    }
}
