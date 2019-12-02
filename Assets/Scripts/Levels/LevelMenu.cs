using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Behaviour for the level UI
public class LevelMenu : MonoBehaviour
{
    private int moveCount;
    public Text MoveCountText;
    public Text FailMessage;
    public GameObject CompleteMenu;
    public GameObject FailMenu;
    public Fade SceneTransition;
    public MusicSource musicSource;

    // Set menus inactive
    void Start()
    {
        moveCount = 0;
        CompleteMenu.SetActive(false);
        FailMenu.SetActive(false);
    }

    void Update()
    {
        MoveCountText.text = "Moves: " + moveCount;

        if (Input.GetKeyDown(KeyCode.Return) && CompleteMenu.activeSelf)
        {
            OnReturnToLevelSelectClick();
        }
    }

    // Message handler for increasing movecount 
    public void ModifyMoveCount(int diff)
    {
        moveCount += diff;
    }

    // Show/hide completion menu
    public void SetCompleteMenu(bool showMenu)
    {
        CompleteMenu.SetActive(showMenu);
    }

    // Show/hide failure menu
    public void SetFailMenu(bool showMenu)
    {
        FailMenu.SetActive(showMenu);
    }

    // Edit failure menu message
    public void SetFailMessage(string message)
    {
        FailMessage.text = message;
    }

    // Returns to level select, saves result on button click
    public void OnReturnToLevelSelectClick()
    {
        SaveLoad.UpdateLevelMoveCount(moveCount);
        StartCoroutine(musicSource.FadeOut());
        StartCoroutine(SceneTransition.TransitionToScene("Level Select"));
    }

    // Restarts the level
    public void OnRetryClick()
    {
        SaveLoad.UpdateLevelMoveCount(moveCount);
        StartCoroutine(musicSource.FadeOut());
        StartCoroutine(SceneTransition.TransitionToScene(SceneManager.GetActiveScene().name));
    }
}
