using System;
using System.IO;
using TMPro;
using UnityEngine;
using Dan.Main;
public class LeaderboardManager : MonoBehaviour
{
    #region Statication

    public static LeaderboardManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    #endregion
    [Header("Leaderboard UI")]
    [SerializeField] private TextMeshProUGUI[] nameLabels;
    [SerializeField] private TextMeshProUGUI[] scoreLabels;
    public string chosenUsername;
    
    [Header("HighScore UI")]
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI roundText;
    [SerializeField] private TextMeshProUGUI timeText;
    private string savePath => Path.Combine(Application.persistentDataPath, "HighScore.json");
    
    public class ScoreSaveData
    {
        public int score;
        public int round;
        public float time;
    }
    private void Start()
    {
        GetLeaderboard();
        if (File.Exists(savePath))
        {
            ScoreSaveData foundData = GetHighScores();
            highScoreText.text = $"{foundData.score}";
            roundText.text = $"{foundData.round}";
            timeText.text = TimeSpan.FromSeconds(foundData.time).ToString(@"mm\:ss\.ff");
        }
    }

    public void GetLeaderboard()
    {
        Leaderboards.YoumuLeaderboard.GetEntries(entries =>
        {
            int loopLength = (entries.Length < nameLabels.Length) ? entries.Length : nameLabels.Length;
            for (int i = 0; i < loopLength; i++)
            {
                nameLabels[i].text = entries[i].Username;
                scoreLabels[i].text = entries[i].Score.ToString();
            }
        });
    }

    public void SetLeaderboardEntry(string username, int score)
    {
        Leaderboards.YoumuLeaderboard.UploadNewEntry(username, score, ((_) =>
        {
            GetLeaderboard();
        }));
        Leaderboards.YoumuLeaderboard.ResetPlayer();
    }

    public ScoreSaveData GetHighScores()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            ScoreSaveData saveData = JsonUtility.FromJson<ScoreSaveData>(json);
            return saveData;
        }
        return null;
    }
    
    public void UpdateHighScores(int newScore, int round, float time)
    {
        ScoreSaveData saveData;
        if (File.Exists(savePath))
        {
            saveData = GetHighScores();
            if (newScore > saveData.score)
            {
                saveData.score = newScore;
            }
            if (round > saveData.round)
            {
                saveData.round = round;
            }
            if (time > saveData.time)
            {
                saveData.time = time;
            }
        }
        else
        {
            saveData = new ScoreSaveData();
            saveData.score = newScore;
            saveData.round = round;
            saveData.time = time;
        }
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(savePath, json);
    }
}
