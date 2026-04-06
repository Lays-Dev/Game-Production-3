using UnityEngine;

public class Note : MonoBehaviour
{
    double timeInstantiated;
    public float assignedTime;
    public RectTransform rt;

    void Start()
    {
        //
        timeInstantiated = SongManager.GetAudioSourceTime();
        RectTransform rt = GetComponent<RectTransform>();
    }

    
    void Update()
    {
        // All of this code is figuring out how long the note needs to take to get to it's position
        double timeSinceInstantiated = SongManager.GetAudioSourceTime() - timeInstantiated;
        rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, -400f);
        float t = (float)(timeSinceInstantiated / (SongManager.instance.noteTime * 2));

        
        //This is what moves the note from the right side of the screen to the left. It is gathering the spawn position and bringing it to the despawn position and then deleting it after T reaches 1
        if (t > 1)
            {
            Destroy(gameObject);
        }
        else 
        {
            rt.anchoredPosition = Vector2.Lerp(Vector2.right * SongManager.instance.noteSpawnX, Vector2.right * SongManager.instance.noteDespawnX, t);
        }

    }
}
