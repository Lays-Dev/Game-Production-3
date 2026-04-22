using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Note : MonoBehaviour
{
    double timeInstantiated;
    public float assignedTime;
    public RectTransform rt; 
    public Image NotePicture;

    void Start()
    {
        //
        timeInstantiated = SongManager.GetAudioSourceTime();
        rt = GetComponent<RectTransform>();
        NotePicture= GetComponent<Image>();
        NotePicture.enabled = false;
        StartCoroutine(NoteVisibility());
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
    public IEnumerator NoteVisibility()
    {
        yield return new WaitForSeconds(.1f);
        Debug.Log("Note should be visible now");
        NotePicture.enabled = true;
        yield return null;



    }

}
