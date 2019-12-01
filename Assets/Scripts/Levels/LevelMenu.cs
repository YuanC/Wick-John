using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    private int moveCount;
    public Text MoveCountText;
    public Text FailMessage;
    public GameObject CompleteMenu;
    public GameObject FailMenu;
    public Fade SceneTransition;
    public MusicSource musicSource;

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

    public void SetCompleteMenu(bool showMenu)
    {
        CompleteMenu.SetActive(showMenu);
    }

    public void SetFailMenu(bool showMenu)
    {
        FailMenu.SetActive(showMenu);
    }

    public void SetFailMessage(string message)
    {
        FailMessage.text = message;
    }

    public void OnReturnToLevelSelectClick()
    {
        SaveLoad.UpdateLevelMoveCount(moveCount);
        StartCoroutine(musicSource.FadeOut());
        StartCoroutine(SceneTransition.TransitionToScene("Level Select"));
    }

    public void OnRetryClick()
    {
        SaveLoad.UpdateLevelMoveCount(moveCount);
        StartCoroutine(musicSource.FadeOut());
        StartCoroutine(SceneTransition.TransitionToScene(SceneManager.GetActiveScene().name));
    }

    public void ShowCompleteMenuPanel()
    {
        CompleteMenu.SetActive(true);
    }
}
