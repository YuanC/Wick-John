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
    public GameObject AttributionPanel;
    public Button BackButton;
    public Button ForwardButton;
    public Color UnselectedLevelColor;
    public Color SelectedLevelColor;
    public int UnselectedLevelTextSize;
    public int SelectedLevelTextSize;

    public Fade SceneTransition;
    public MusicSource musicSource;

    private List<Dictionary<string, string>> levelData = new List<Dictionary<string, string>>()
    {
        new Dictionary<string, string>()
        {
            { "title", "Child is the Father of Man" },
            { "sceneName", "Level0" },
            { "optMoveCount", "23" }
        },
        new Dictionary<string, string>()
        {
            { "title", "Corrupted to the bone with the beauty of this forsaken world" },
            { "sceneName", "Level1" },
            { "optMoveCount", "44" }
        },
        new Dictionary<string, string>()
        {
            { "title", "I will show you fear in a handful of dust" },
            { "sceneName", "Level2" },
            { "optMoveCount", "94" }
        },
        new Dictionary<string, string>()
        {
            { "title", "And Jesus wept,\nfor there were no more worlds to conquer" },
            { "sceneName", "Level3" },
            { "optMoveCount", "N/A" }
        },

        new Dictionary<string, string>()
        {
            { "title", "Like when god throws a star\nAnd everyone looks up\nTo see that whip of sparks\nAnd then it's gone" },
            { "sceneName", "Level3" },
            { "optMoveCount", "N/A" }
        },
        new Dictionary<string, string>()
        {
            { "title", "The Answer" },
            { "sceneName", "Level5" },
            { "optMoveCount", "N/A" }
        }
    };

    void Start()
    {
        InstructionsPanel.SetActive(false);
        AttributionPanel.SetActive(false);
        SaveLoad.LoadSave();

        List<int> saveData = SaveLoad.SaveData;

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
        SelectLevel(SaveLoad.CurrentLevel);
        StartCoroutine(musicSource.FadeIn());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(SceneTransition.TransitionToScene("Epigraph"));
            StartCoroutine(musicSource.FadeOut());
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && selectedLevel < unlockedLevelCount - 1)
        {
            SelectLevel(selectedLevel + 1);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && selectedLevel > 0)
        {
            SelectLevel(selectedLevel - 1);
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            OpenLevel();
        }
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
        MoveCountText.text = $"Current Best Movecount: {currentBestString}, Optimal Movecount: {selectedLevelData["optMoveCount"]}";
        ChapterNumberText.text = $"~ Chapter {selectedLevel + 1} ~";

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

    public void SetAttributionPanel(bool isActive)
    {
        AttributionPanel.SetActive(isActive);
    }

    public void OpenLevel()
    {
        SaveLoad.CurrentLevel = selectedLevel;
        StartCoroutine(musicSource.FadeOut());
        StartCoroutine(SceneTransition.TransitionToScene(levelData[selectedLevel]["sceneName"]));
    }

    public void Exit()
    {
        Application.Quit();
    }
}
