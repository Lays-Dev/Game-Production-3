using UnityEngine;

public class Note : MonoBehaviour
{
    double timeInstantiated;
    public float assignedTime;

    
    void Start()
    {
        //
        timeInstantiated = SongManager.GetAudioSourceTime();
    }

    
    void Update()
    {
        // All of this code is figuring out how long the note needs to take to get to it's position
        double timeSinceInstantiated = SongManager.GetAudioSourceTime() - timeInstantiated;
        float t = (float)(timeSinceInstantiated / (SongManager.instance.noteTime * 2));

        GetComponent<SpriteRenderer>().enabled = true;
        //This is what moves the note from the right side of the screen to the left. It is gathering the spawn position and bringing it to the despawn position and then deleting it after T reaches 1
        if (t > 1)
            {
            Destroy(gameObject);
        }
        else 
        {
            transform.position = Vector3.Lerp(Vector3.right * SongManager.instance.noteSpawnX, Vector3.right * SongManager.instance.noteDespawnX, t);
        }

    }
}
