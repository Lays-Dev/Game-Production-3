using UnityEngine;

public class AnvilPrefabChange : MonoBehaviour
{
    public GameObject anvilPrefab;
    public GameObject tutorialRhythmGame;
    public GameObject violinRhythmGame;
    public GameObject harpRhythmGame;
    public GameObject hornRhythmGame;

    public GameObject gameManager;
    void Start()
    {
        gameObject.GetComponent<Items>().MusicGamePrefab = anvilPrefab;
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject gameManager = GameObject.FindWithTag("GameManager");
        if (gameManager.GetComponent<GameManager>().ViolinCraft)
        {
            gameObject.GetComponent<Items>().MusicGamePrefab = violinRhythmGame;
        }
        else if (gameManager.GetComponent<GameManager>().HarpCraft)
        {
            gameObject.GetComponent<Items>().MusicGamePrefab = harpRhythmGame;
        }
        else if (gameManager.GetComponent<GameManager>().HornCraft)
        {
            gameObject.GetComponent<Items>().MusicGamePrefab = hornRhythmGame;
        }
        else
        {
            gameObject.GetComponent<Items>().MusicGamePrefab = tutorialRhythmGame;
        }
    }
}