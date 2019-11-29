using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    private int selectedLevel;

    private string[] levelData = new string[]
    {
        "Child is the Father of Man",
        "Corrupted to the Bone with the Beauty of this Forsaken World",
        "And Jesus Wept, for there were no more worlds to conquer",
        "The Answer"
    };

    private int[] optimalMoveCounts = new int[]
    {
        1,
        2,
        3,
        4
    };

    public Text TitleText;
    public Text MoveCountText;
    public Text ChapterNumberText;
    public Text InstructionsPanel;

    void Start()
    {
        SaveLoad.LoadSave();

        // Add level list buttons depending if the previous level was played;
        // Get the furthest unplayed level, else the last level
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectLevel(int index)
    {
        selectedLevel = index;

        // Replace level title, movecounts
    }

    public void SetInstructionsPanel(bool isActive)
    {

    }

    public void OpenLevel(int level)
    {
        SceneManager.LoadScene("Level_" + level);
    }
}
