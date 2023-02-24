using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    [SerializeField] private int score;
    [SerializeField] private string username;

    public PlayerData() { }
    public PlayerData(string username)
    {
        Username = username;
        score = 0;
    }
    public PlayerData(string username, int score)
    {
        Username = username;
        Score = score;
    }
    public int Score { get => score; set => score = value; }
    public string Username { get => username; set => username = value; }
}

[System.Serializable]
public class Scoreboard
{
    [SerializeField] private List<PlayerData> scoreboard;

    public Scoreboard()
    {
        if (scoreboard == null)
        {
            scoreboard = new List<PlayerData>();
        }
    }
    public void addPlayerData(PlayerData playerdata)
    {
        scoreboard.Add(playerdata);
    }

    public PlayerData GetTopPlayer()
    {
        if(scoreboard.Count > 0)
        {
            PlayerData topPlayer = new PlayerData("", 0);
            foreach(PlayerData player in scoreboard)
            {
                if(player.Score > topPlayer.Score)
                {
                    topPlayer = player;
                }
            }
            return topPlayer;
        }
        return null;
    }

    public bool isScoreboardEmpty()
    {
        if(scoreboard.Count == 0)
        {
            return true;
        }
        return false;
    }
}

[System.Serializable]
public class GameInformation
{
    [SerializeField] public PlayerData currentPlayerData;
    [SerializeField] public Scoreboard scoreboard;
}

public class GlobalGameManager : MonoBehaviour
{
    public static GlobalGameManager Instance;
    public GameInformation gameInformation;

    private void Awake()
    {
        if (gameInformation == null)
        {
            gameInformation = new GameInformation();
        }
            LoadGame();
        
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SaveGame()
    {
        gameInformation.scoreboard.addPlayerData(gameInformation.currentPlayerData);

        string jsonObject = JsonUtility.ToJson(gameInformation.scoreboard);
        File.WriteAllText(Application.persistentDataPath + "/saveFile.json", jsonObject);
    }

    public void LoadGame()
    {
        string dataPath = Application.persistentDataPath + "/saveFile.json";
        if (File.Exists(dataPath))
        {
            string jsonObject = File.ReadAllText(dataPath);
            gameInformation.scoreboard = JsonUtility.FromJson<Scoreboard>(jsonObject);
        }
    }
}
