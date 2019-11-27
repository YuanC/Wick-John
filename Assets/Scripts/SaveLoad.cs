using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class Record
{
    // Serializable class representing a leaderboard entry

    public string name;
    public int time;

    public Record(string name, int time)
    {
        this.name = name;
        this.time = time;
    }
}

public static class SaveLoad
{
    // Class for saving and loading a persistent leaderboard

    public static List<Record> Leaderboard = new List<Record>();
    private static int MaxEntries = 10;

    // Comparison method for sorting the leaderboard entries
    private static int CompareRecords(Record x, Record y)
    {
        if (x.time > y.time)
        {
            return 1;
        }
        else if (y.time > x.time)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }

    // Adding and saving a new entry in the leaderboard
    public static void UpdateLeaderboard(string name, int time)
    {
        Leaderboard.Add(new Record(name, time));

        Leaderboard.Sort(CompareRecords);
        
        if (Leaderboard.Count > MaxEntries)
        {
            Leaderboard.RemoveAt(MaxEntries);
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/leaderboard.gd");
        bf.Serialize(file, SaveLoad.Leaderboard);
        file.Close();
    }

    // Loading the leaderboard
    public static void LoadLeaderboard()
    {
        if (File.Exists(Application.persistentDataPath + "/leaderboard.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/leaderboard.gd", FileMode.Open);
            SaveLoad.Leaderboard = (List<Record>)bf.Deserialize(file);
            file.Close();
        }
    }
}