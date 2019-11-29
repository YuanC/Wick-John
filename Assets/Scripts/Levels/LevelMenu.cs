using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    private int moveCount;
    public Text MoveCountText;
    public GameObject CompleteMenu;
    public GameObject FailMenu;

    void Start()
    {
        moveCount = 0;
        CompleteMenu.SetActive(false);
        FailMenu.SetActive(false);
    }

    void Update()
    {
        MoveCountText.text = "Moves: " + moveCount;
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

    public void OnReturnToLevelSelectClick()
    {
        SaveLoad.UpdateLevelMoveCount(moveCount);
        SceneManager.LoadScene("Level Select");
    }

    public void ShowCompleteMenuPanel()
    {
        CompleteMenu.SetActive(true);
    }
}
