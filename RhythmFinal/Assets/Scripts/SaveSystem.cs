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
        data.HarpCraft            = manager.HarpCraft;
        data.ViolinCraft          = manager.ViolinCraft;
        data.HornCraft            = manager.HornCraft;  
        data.crafting             = manager.crafting;
        data.hornCraftReady       = manager.hornCraftReady;
        data.harpCraftReady       = manager.harpCraftReady;
        data.violinCraftReady     = manager.violinCraftReady;
        data.activeQuest          = manager.activeQuest;
        data.ViolinCraftComplete   = manager.ViolinCraftComplete;
        data.HarpCraftComplete     = manager.HarpCraftComplete;
        data.HornCraftComplete     = manager.HornCraftComplete;
        data.tutorialCompleted       = manager.tutorialCompleted;

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