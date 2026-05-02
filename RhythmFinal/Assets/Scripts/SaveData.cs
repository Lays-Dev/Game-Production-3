using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public int currentQuestIndex;
    public List<QuestInfo> questList = new List<QuestInfo>();
    public bool firstTimePlaying = true;
}

[Serializable]
public class QuestInfo
{
    [Header("Level Int")]
    public int levelInt;         // 1 = Ice, 2 = Desert, 3 = Forest, 4 = Tutorial

    [Header("Quest General Info")]
    public int questID;
    public string questName;

    [Header("Quest Item Info")]
    public int totalItemsNeeded;
    public int itemsCollected;

    [Header("Quest Aftermath")]
    public bool isCompleted;
    public bool nextQuest;       // True if this quest chains to another on completion
    public int nextQuestID;      // List index of the next quest
    public bool returnToTutorial;// True if completion should load the TutorialLevel scene

    [Header("Quest Display")]
    public string questDisplayText;
    public bool bossBattle;      // True for boss/single-trigger quests

    [Header("The Wall")]
    public bool removeWall;      // True if the gate should be open
}