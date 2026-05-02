using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string savePath =>
        Application.persistentDataPath + "/saveData.json";

    public static void SaveGame(GameManager manager)
    {
        SaveData data = new SaveData
        {
            currentQuestIndex = manager.currentQuestIndex,
            questList         = manager.questList,
            firstTimePlaying  = manager.firstTimePlaying
        };

        File.WriteAllText(savePath, JsonUtility.ToJson(data, true));
        Debug.Log("[SaveSystem] Saved to: " + savePath);
    }

    public static SaveData LoadGame()
    {
        if (!File.Exists(savePath))
            return null;

        return JsonUtility.FromJson<SaveData>(File.ReadAllText(savePath));
    }

    // Wipes the file so the next LoadGame() returns null and firstTimePlaying stays true
    public static void DeleteSave()
    {
        if (File.Exists(savePath))
            File.Delete(savePath);
        Debug.Log("[SaveSystem] Save file deleted.");
    }
}