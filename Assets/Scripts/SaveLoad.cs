using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad
{
    // Class for saving and loading a persistent leaderboard
    //public static List<int> SaveData = new List<int>() {1, 1, 1, 1, 1,-1};
    public static List<int> SaveData;

    public static int CurrentLevel = 0;

    // Adding and new save entry
    public static void UpdateLevelMoveCount(int moveCount)
    {
        if (CurrentLevel >= 0 && CurrentLevel < SaveData.Count)
        {
            if (SaveData[CurrentLevel] == -1)
            {
                SaveData[CurrentLevel] = moveCount;
            }
            else
            {
                SaveData[CurrentLevel] = Mathf.Min(SaveData[CurrentLevel], moveCount);
            }

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/SaveData.gd");
            bf.Serialize(file, SaveLoad.SaveData);
            file.Close();
        }
    }

    // Loading the SaveData
    public static void LoadSave()
    {
        if (File.Exists(Application.persistentDataPath + "/SaveData.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/SaveData.gd", FileMode.Open);
            SaveLoad.SaveData = (List<int>)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            SaveData = new List<int>() { -1, -1, -1, -1, -1, -1 };
        }
    }

    // Save Debugging Purposes
    public static void DeleteSave()
    {
        SaveData = new List<int>() { -1, -1, -1, -1, -1, -1 };

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SaveData.gd");
        bf.Serialize(file, SaveLoad.SaveData);
        file.Close();
    }

    // Save Debugging Purposes
    public static void UnlockAllLevels()
    {
        SaveData = new List<int>() { 500, 500, 500, 500, 500, 500 };

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SaveData.gd");
        bf.Serialize(file, SaveLoad.SaveData);
        file.Close();
    }
}