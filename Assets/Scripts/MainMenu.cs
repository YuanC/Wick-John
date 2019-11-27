using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    void Start()
    {
        // Load the leaderboard list upon scene load, and append the entries to the UI

        //SaveLoad.LoadLeaderboard();

        //if (SaveLoad.Leaderboard.Count > 0)
        //{
        //    LeaderboardEntries.text = "";

        //    for (int i = 0; i < SaveLoad.Leaderboard.Count; i++)
        //    {
        //        LeaderboardEntries.text += SaveLoad.Leaderboard[i].name + " " + SaveLoad.Leaderboard[i].time + "\n";
        //    }
        //}
    }

    public void ClickQuitButton()
    {
        Application.Quit();
    }

    public void ClickBeginButton()
    {
        SceneManager.LoadScene("Level Select");
    }
}
