using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int currentQuestIndex;
    public bool firstTimePlaying = true;
    public List<QuestInfo> questList = new List<QuestInfo>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadGame();

            // Seed immediately in Awake so questList is populated
            // before any other script's Start() runs
            if (firstTimePlaying)
                SeedFirstTime();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ── First-time setup ──────────────────────────────────────────────────────

    private void SeedFirstTime()
    {
        questList         = BuildQuestList();
        currentQuestIndex = 4; // Start on Quest 4 (first normal quest)
        firstTimePlaying  = false;
        SaveGame();
        Debug.Log("[GameManager] First-time seed complete — " + questList.Count + " quests saved.");
    }

    private List<QuestInfo> BuildQuestList()
    {
        // INDEX = position in this list. nextQuestID must match the target's index.
        //
        // 0 = New Beginnings      (tutorial)
        // 1 = The Elder's Trial   (boss, forest)
        // 2 = The Sphynx's Trial  (boss, desert)
        // 3 = The Mammoth's Trial (boss, ice)
        // 4 = Violins is the Key  (normal, forest  → boss index 1)
        // 5 = Tomorrow's Dust     (normal, desert  → boss index 2)
        // 6 = Pink Phantom        (normal, ice     → boss index 3)

        return new List<QuestInfo>
        {
            // ── Index 0 ───────────────────────────────────────────────────────
            new QuestInfo
            {
                levelInt         = 4,
                questID          = 0,
                questName        = "New Beginnings",
                totalItemsNeeded = 1,
                itemsCollected   = 0,
                isCompleted      = false,
                nextQuest        = false,
                nextQuestID      = 0,
                returnToTutorial = false,
                questDisplayText = "Start your journey by choosing a quest",
                bossBattle       = false,
                removeWall       = false,
            },
            // ── Index 1 (boss) ────────────────────────────────────────────────
            new QuestInfo
            {
                levelInt         = 3,
                questID          = 1,
                questName        = "The Elder's Trial",
                totalItemsNeeded = 1,
                itemsCollected   = 0,
                isCompleted      = false,
                nextQuest        = false,
                nextQuestID      = 0,
                returnToTutorial = true,
                questDisplayText = "Return to the Elder",
                bossBattle       = true,
                removeWall       = true,
            },
            // ── Index 2 (boss) ────────────────────────────────────────────────
            new QuestInfo
            {
                levelInt         = 2,
                questID          = 2,
                questName        = "The Sphynx's Trial",
                totalItemsNeeded = 1,
                itemsCollected   = 0,
                isCompleted      = false,
                nextQuest        = false,
                nextQuestID      = 0,
                returnToTutorial = true,
                questDisplayText = "Return to the Sphynx",
                bossBattle       = true,
                removeWall       = true,
            },
            // ── Index 3 (boss) ────────────────────────────────────────────────
            new QuestInfo
            {
                levelInt         = 1,
                questID          = 3,
                questName        = "The Mammoth's Trial",
                totalItemsNeeded = 1,
                itemsCollected   = 0,
                isCompleted      = false,
                nextQuest        = false,
                nextQuestID      = 0,
                returnToTutorial = true,
                questDisplayText = "Return to the Mammoth",
                bossBattle       = true,
                removeWall       = true,
            },
            // ── Index 4 (normal) ──────────────────────────────────────────────
            new QuestInfo
            {
                levelInt         = 3,
                questID          = 4,
                questName        = "Violins is the Key",
                totalItemsNeeded = 3,
                itemsCollected   = 0,
                isCompleted      = false,
                nextQuest        = true,
                nextQuestID      = 1,
                returnToTutorial = false,
                questDisplayText = "enchanted logs collected",
                bossBattle       = false,
                removeWall       = false,
            },
            // ── Index 5 (normal) ──────────────────────────────────────────────
            new QuestInfo
            {
                levelInt         = 2,
                questID          = 5,
                questName        = "Tomorrow's Dust",
                totalItemsNeeded = 3,
                itemsCollected   = 0,
                isCompleted      = false,
                nextQuest        = true,
                nextQuestID      = 2,
                returnToTutorial = false,
                questDisplayText = "piles of sand collected",
                bossBattle       = false,
                removeWall       = false,
            },
            // ── Index 6 (normal) ──────────────────────────────────────────────
            new QuestInfo
            {
                levelInt         = 1,
                questID          = 6,
                questName        = "Pink Phantom",
                totalItemsNeeded = 3,
                itemsCollected   = 0,
                isCompleted      = false,
                nextQuest        = true,
                nextQuestID      = 3,
                returnToTutorial = false,
                questDisplayText = "crystals collected",
                bossBattle       = false,
                removeWall       = false,
            },
        };
    }

    // ── Save / Load ───────────────────────────────────────────────────────────

    public void SaveGame()
    {
        firstTimePlaying = false;
        SaveSystem.SaveGame(this);
    }

    public void LoadGame()
    {
        SaveData data = SaveSystem.LoadGame();
        if (data == null)
        {
            Debug.Log("[GameManager] No save file — will seed on first run.");
            return;
        }

        firstTimePlaying  = data.firstTimePlaying;
        currentQuestIndex = data.currentQuestIndex;
        questList         = data.questList;
        Debug.Log("[GameManager] Loaded. Quest index: " + currentQuestIndex);
    }

    public void ResetSave()
    {
        firstTimePlaying  = true;
        currentQuestIndex = 0;
        questList.Clear();
        SaveSystem.DeleteSave();
        SeedFirstTime(); // re-seed so the list is never empty after a reset
        Debug.Log("[GameManager] Save reset and re-seeded.");
    }

    // ── Helpers ───────────────────────────────────────────────────────────────

    public QuestInfo GetQuest(int questID)
        => questList.Find(q => q.questID == questID);

    public QuestInfo GetCurrentQuest()
    {
        if (currentQuestIndex < 0 || currentQuestIndex >= questList.Count)
            return null;
        return questList[currentQuestIndex];
    }

    public void SetCurrentQuest(int index)
    {
        if (index < 0 || index >= questList.Count)
        {
            Debug.LogError("[GameManager] SetCurrentQuest: index " + index + " out of range.");
            return;
        }
        currentQuestIndex = index;
        SaveGame();
    }
}