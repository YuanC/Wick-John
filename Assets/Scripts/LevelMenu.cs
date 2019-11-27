using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    private int moveCount;
    public Text TimerText;
    public GameObject CompleteMenu;

    void Start()
    {
        moveCount = 0;
        CompleteMenu.SetActive(false);
    }

    void Update()
    {
    }

    //TODO: Implement message handler for increasing movecount 
    //TODO: Implement message handler for showing completemenu panel movecount 

    public void OnReturnToLevelSelectClick()
    {
        //SaveLoad.UpdateLeaderboard(LeaderboardPanel.GetComponentInChildren<InputField>().text, (int)timer);
        SceneManager.LoadScene("Level Select");
    }

    public void ShowCompleteMenuPanel()
    {
        CompleteMenu.SetActive(true);
    }
}
