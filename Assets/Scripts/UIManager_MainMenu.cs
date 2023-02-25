using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIManager_MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TMP_InputField usernameInput;
    // Start is called before the first frame update
    void Start()
    {
        UpdateScoreboardInformation();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        string username = usernameInput.text == "" ? "Anonymous" : usernameInput.text;
        GlobalGameManager.Instance.gameInformation.currentPlayerData = new PlayerData(username);
        SceneManager.LoadScene("main");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void ScoreboardButton()
    {
        SceneManager.LoadScene("GUI_Scoreboard");
    }

    public void UpdateScoreboardInformation()
    {
        Scoreboard scoreboard = GlobalGameManager.Instance.gameInformation.scoreboard;
        if (!scoreboard.isScoreboardEmpty())
        {
            PlayerData player = scoreboard.GetTopPlayer();
            scoreText.SetText("Best Score: " + player.Username + " : " + player.Score);
        }
    }

}
