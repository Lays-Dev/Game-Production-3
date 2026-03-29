using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public AudioSource hitSFX;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }
    public static void hit()
    {
        instance.hitSFX.Play();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
