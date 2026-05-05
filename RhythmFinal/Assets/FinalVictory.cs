using UnityEngine;

public class FinalVictory : MonoBehaviour
{
    public GameObject violin;
    public GameObject harp;
    public GameObject horn;
    public GameObject finalWinScreen;    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      GameManager gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        if(gameManager.ViolinCraftComplete)
        {
            violin.SetActive(true);
        }
        if(gameManager.HarpCraftComplete)
        {
            harp.SetActive(true);
        }
        if(gameManager.HornCraftComplete)
        {
            horn.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameManager gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        InspectObject player = GameObject.FindWithTag("Player").GetComponent<InspectObject>();
        if (gameManager.ViolinCraftComplete && gameManager.HarpCraftComplete && gameManager.HornCraftComplete && player.inspecting == false)
        {
            Debug.Log("You win!");
            finalWinScreen.SetActive(true);

        }
    }
}
