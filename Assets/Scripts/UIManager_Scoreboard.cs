using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager_Scoreboard : MonoBehaviour
{
    [SerializeField] private GameObject scoreboardItemTemplate;
    [SerializeField] private GameObject scoreboardListViewContent;
    private Scoreboard scoreboard;
    
    void Awake()
    {
        scoreboard = GlobalGameManager.Instance.gameInformation.scoreboard;
        PlayerData[] players = scoreboard.GetTopTenPlayers().ToArray();

        for (int i = 0; i < players.Length; i++)
        {
            GameObject tmpItemTemplate = Instantiate(scoreboardItemTemplate);
            tmpItemTemplate.transform.Find("Number").GetComponent<TextMeshProUGUI>().SetText((i + 1).ToString());
            tmpItemTemplate.transform.Find("Username").GetComponent<TextMeshProUGUI>().SetText(players[i].Username);
            tmpItemTemplate.transform.Find("Score").GetComponent<TextMeshProUGUI>().SetText(players[i].Score.ToString());
            tmpItemTemplate.transform.SetParent(scoreboardListViewContent.transform);
        }
    }

    public void backToMenu()
    {
        SceneManager.LoadScene("GUI_Main_Menu");
    }
}
