using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestTest : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI QuestText;
    public TextMeshProUGUI QuestTitle;

    [Header("Scene Objects")]
    public GameObject Spawner;
    public GameObject TreeWall;

    private bool hasBeenCollected;
    private QuestInfo _quest;

    // ── Unity lifecycle ───────────────────────────────────────────────────────

    void Start()
    {
        RandomGen spawner = Spawner != null ? Spawner.GetComponent<RandomGen>() : null;

        _quest = GameManager.instance.GetCurrentQuest();

        if (_quest != null)
            UpdateQuestUI();
        else
            QuestTitle.text = "No Active Quest";

        if (spawner != null)
            StartCoroutine(spawner.SpawnRandomSong());
    }

    void Update()
    {
        // Update only drives display — no state changes here
        if (_quest == null) return;
        UpdateQuestUI();
    }

    // ── Collection ────────────────────────────────────────────────────────────

    // Called by Lane.cs after a rhythm minigame is won
    public IEnumerator CollectItem()
    {
        if (hasBeenCollected || _quest == null || _quest.isCompleted || _quest.bossBattle)
            yield break;

        hasBeenCollected = true;
        _quest.itemsCollected++;

        if (_quest.itemsCollected >= _quest.totalItemsNeeded)
        {
            _quest.isCompleted = true;

            if (_quest.nextQuest)
            {
                // Chain to the next quest and re-cache
                GameManager.instance.currentQuestIndex = _quest.nextQuestID;
                _quest = GameManager.instance.GetCurrentQuest();
                GameManager.instance.SaveGame();
            }
            else if (_quest.returnToTutorial)
            {
                GameManager.instance.SaveGame();
                SceneManager.LoadScene("TutorialLevel");
                yield break;
            }
        }

        GameManager.instance.SaveGame();
        UpdateQuestUI();

        yield return new WaitForSeconds(0.4f);
        hasBeenCollected = false;
    }

    // Called by boss-battle completion triggers
    public void CompleteBossBattle()
    {
        if (_quest == null || !_quest.bossBattle || _quest.isCompleted) return;

        _quest.isCompleted = true;

        if (_quest.returnToTutorial)
        {
            GameManager.instance.SaveGame();
            SceneManager.LoadScene("TutorialLevel");
            return;
        }

        if (_quest.nextQuest)
        {
            GameManager.instance.currentQuestIndex = _quest.nextQuestID;
            _quest = GameManager.instance.GetCurrentQuest();
        }

        GameManager.instance.SaveGame();
        UpdateQuestUI();
    }

    // ── UI ────────────────────────────────────────────────────────────────────

    public void UpdateQuestUI()
    {
        if (_quest == null) return;

        QuestTitle.text = _quest.questName;

        // Boss quests show a custom string; normal quests show count + label
        if (_quest.bossBattle)
            QuestText.text = _quest.questDisplayText;
        else
            QuestText.text = $"{_quest.itemsCollected}/{_quest.totalItemsNeeded} {_quest.questDisplayText}";

        if (TreeWall != null)
            TreeWall.SetActive(!_quest.removeWall);
    }
}