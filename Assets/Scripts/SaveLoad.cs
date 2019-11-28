using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad
{
    // Class for saving and loading a persistent leaderboard

    public static List<int> SaveData = new List<int>() {-1, -1, -1, -1};

    // Adding and new save entry
    public static void UpdateLevelMoveCount(int index, int moveCount)
    {
        if (index >= 0 && index < SaveData.Count)
        {
            SaveData[index] = Mathf.Min(SaveData[index], moveCount);

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
    }
}