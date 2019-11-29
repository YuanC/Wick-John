using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class LevelSelect : MonoBehaviour
{
    private int selectedLevel;
    private int unlockedLevelCount;

    public Text TitleText;
    public Text MoveCountText;
    public Text ChapterNumberText;
    public GameObject LevelList;
    public GameObject LevelListItem;
    public GameObject InstructionsPanel;
    public Button BackButton;
    public Button ForwardButton;
    public Color UnselectedLevelColor;
    public Color SelectedLevelColor;
    public int UnselectedLevelTextSize;
    public int SelectedLevelTextSize;


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
        InstructionsPanel.SetActive(false);
        SaveLoad.LoadSave();

        List<int> saveData = SaveLoad.SaveData;
        //for(int i = 0; i< saveData.Count; i++)
        //{
        //    Debug.Log(saveData[i]);
        //}

        int index = 0;
        while (index < saveData.Count)
        {
            AddLevelToUI(index);
            if (saveData[index] != -1)
            {
                index++;
            }
            else
            {
                break;
            }
        }
        unlockedLevelCount = index + 1;
        SelectLevel(index);
    }

    // Makes the level accessible in the menu
    private void AddLevelToUI(int index)
    {
        GameObject levelButton = Instantiate(LevelListItem, LevelList.transform);
        levelButton.GetComponentInChildren<Text>().text = $"{index + 1}";
        levelButton.transform.SetSiblingIndex(index + 1);
        levelButton.GetComponent<Button>().onClick.AddListener(() => 
            {
                int levelIndex = Int32.Parse(levelButton.GetComponentInChildren<Text>().text) - 1;
                SelectLevel(levelIndex);
            }
        );
    }

    // Replace level title, chapter number, movecounts
    public void SelectLevel(int index)
    {
        selectedLevel = index;
        Dictionary<string, string> selectedLevelData = levelData[selectedLevel];

        TitleText.text = selectedLevelData["title"];

        string currentBestString = SaveLoad.SaveData[selectedLevel] != -1 ? $"{SaveLoad.SaveData[selectedLevel]}" : "N/A";
        MoveCountText.text = $"Current Best: {currentBestString} moves, Optimal: {selectedLevelData["optMoveCount"]} moves";
        ChapterNumberText.text = $"~ {selectedLevel + 1} ~";

        BackButton.interactable = (selectedLevel > 0);
        ForwardButton.interactable = (selectedLevel < unlockedLevelCount - 1);

        foreach (Text buttonText in LevelList.GetComponentsInChildren<Text>())
        {
            if (buttonText.text == $"{selectedLevel + 1}")
            {
                buttonText.color = SelectedLevelColor;
                buttonText.fontSize = SelectedLevelTextSize;
            }
            else
            {
                buttonText.color = UnselectedLevelColor;
                buttonText.fontSize = UnselectedLevelTextSize;
            }
        }
    }

    public void OnBackButtonClick()
    {
        SelectLevel(selectedLevel - 1);
    }

    public void OnForwardButtonClick()
    {
        SelectLevel(selectedLevel + 1);
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
