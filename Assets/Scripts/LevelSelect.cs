using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    private int selectedLevel;

    private List<Dictionary<string, string>> levelData = new List<Dictionary<string, string>>()
    {
        new Dictionary<string, string>() 
        {
            { "title", "Child is the Father of Man" },
            { "sceneName", "Debug" },
            { "optMoveCounts", "1" }
        },
        new Dictionary<string, string>()
        {
            { "title", "Corrupted to the Bone with the Beauty of this Forsaken World" },
            { "sceneName", "Debug" },
            { "optMoveCounts", "1" }
        },
        new Dictionary<string, string>()
        {
            { "title", "And Jesus Wept, for there were no more worlds to conquer" },
            { "sceneName", "Debug" },
            { "optMoveCounts", "1" }
        },
        new Dictionary<string, string>()
        {
            { "title", "The Answer" },
            { "sceneName", "Debug" },
            { "optMoveCounts", "1" }
        }
    };

    public Text TitleText;
    public Text MoveCountText;
    public Text ChapterNumberText;
    public GameObject LevelList;
    public GameObject LevelListItem;
    public GameObject InstructionsPanel;

    void Start()
    {
        SaveLoad.LoadSave();

        // Add level list buttons depending if the previous level was played;
        // LevelList add whatever
        // Get the furthest unplayed level, else the last level
        // SelectLevel(latest)
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectLevel(int index)
    {
        selectedLevel = index;

        // Replace level title, chapter number, movecounts
    }

    public void SetInstructionsPanel(bool isActive)
    {
        InstructionsPanel.SetActive(isActive);
    }

    public void OpenLevel(int level)
    {
        SceneManager.LoadScene("Level_" + level);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
