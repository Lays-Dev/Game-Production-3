using System.IO;
using UnityEngine;

public static class SaveSystem
{   
    //Save file path
    private static string savePath =
        Application.persistentDataPath + "/saveData.json";

    // Save game data to file as JSON
    public static void SaveGame(GameManager manager)
    {
        SaveData data = new SaveData();

        // Quest data
        data.questItemsCollected   = manager.questItemsCollected;
        data.questCompleted        = manager.questCompleted;
        data.tutorialCompleted     = manager.tutorialCompleted;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);
        Debug.Log("Game saved to: " + savePath);
    }

    // Load game data from file and return as SaveData object
    public static SaveData LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            return JsonUtility.FromJson<SaveData>(json);
        }

        Debug.Log("No save file found");
        return null;
    }
}