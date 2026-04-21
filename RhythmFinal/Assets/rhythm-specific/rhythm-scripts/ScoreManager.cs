using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public AudioSource hitSFX;
    public float hitAmount;
    public float hitTotal;
    public Lane noteAmount;
    public TextMeshProUGUI HitAmounts;
    public TextMeshProUGUI ComboCounter;
    public float comboAmount;
    public GameObject QuestUI;

    void Start()
    {
        instance = this;
        StartCoroutine(FindUI());



    }
    public IEnumerator FindUI()
    {

        yield return new WaitForSeconds(.1f);
        QuestUI = GameObject.FindWithTag("UI");
        QuestUI.SetActive(false);
    }
    public void Awake()
    {

    }
    // This is the function that is called when the player hits a note. It plays a sound effect, increases the hit amount and combo amount by 1, and updates the UI.
    public static void hit()
    {
        instance.hitSFX.Play();
        instance.hitAmount += 1;
        instance.comboAmount += 1;
    }
    public static void miss()
    {
        instance.comboAmount = 0;
    }
    // This is the function that updates the UI every frame. It updates the combo counter and the hit amounts.
    void Update()
    {
        ComboCounter.text = instance.comboAmount + "x";
        HitAmounts.text = instance.hitAmount + "/" + instance.noteAmount.timeStamps.Count;
    }
}