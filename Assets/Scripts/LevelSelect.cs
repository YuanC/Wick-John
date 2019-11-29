using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    private int selectedLevel;

    public Text TitleText;
    public Text MoveCountText;
    public Text ChapterNumberText;
    public GameObject LevelList;
    public GameObject LevelListItem;
    public GameObject InstructionsPanel;

    private List<Dictionary<string, string>> levelData = new List<Dictionary<string, string>>()
    {
        new Dictionary<string, string>()
        {
            { "title", "Child is the Father of Man" },
            { "sceneName", "Debug" },
            { "optMoveCount", "1" }
        },
        new Dictionary<string, string>()
        {
            { "title", "Corrupted to the Bone with the Beauty of this Forsaken World" },
            { "sceneName", "Debug" },
            { "optMoveCount", "1" }
        },
        new Dictionary<string, string>()
        {
            { "title", "And Jesus Wept, for there were no more worlds to conquer" },
            { "sceneName", "Debug" },
            { "optMoveCount", "1" }
        },
        new Dictionary<string, string>()
        {
            { "title", "The Answer" },
            { "sceneName", "Debug" },
            { "optMoveCount", "1" }
        }
    };

    void Start()
    {
        SaveLoad.LoadSave();

        List<int> saveData = SaveLoad.SaveData;
        //Debug.Log(saveData.ToString());
        for(int i = 0; i< saveData.Count; i++)
        {
            Debug.Log(saveData[i]);
        }

        int index = 0;
        while (index < saveData.Count)
        {
            if (index == 0 || saveData[index - 1] != -1)
            {
                AddLevelToUI(index);
                index++;
            }
            else
            {
                break;
            }
        }
        SelectLevel(index);
    }

    // Makes the level accessible in the menu
    private void AddLevelToUI(int index)
    {
        // LevelList add the prefab
    }

    // Replace level title, chapter number, movecounts
    public void SelectLevel(int index)
    {
        selectedLevel = index;
        Dictionary<string, string> selectedLevelData = levelData[selectedLevel];

        TitleText.text = selectedLevelData["title"];
        MoveCountText.text = $"Current Best: {SaveLoad.SaveData[selectedLevel]} / {selectedLevelData["optMoveCount"]} moves";
        ChapterNumberText.text = $"~ {selectedLevel + 1} ~";
    }

    public void SetInstructionsPanel(bool isActive)
    {
        InstructionsPanel.SetActive(isActive);
    }

    public void OpenLevel()
    {
        SaveLoad.CurrentLevel = selectedLevel;
        SceneManager.LoadScene(levelData[selectedLevel]["sceneName"]);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
