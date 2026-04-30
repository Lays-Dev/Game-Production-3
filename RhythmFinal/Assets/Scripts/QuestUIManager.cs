using UnityEngine;

public class QuestUIManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public QuestTest questTest;

    void Update()
    {
        if (LevelState.Instance == null) return;

        int level = LevelState.Instance.selectedLevel;

        if (questTest.currentQuest != level)
        {
            questTest.StartQuest(level);
        }
    }
}
