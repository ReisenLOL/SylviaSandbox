using System;
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
    [SerializeField] private TextMeshProUGUI[] nameLabels;
    [SerializeField] private TextMeshProUGUI[] scoreLabels;
    public string chosenUsername;

    private readonly string publicLeaderboardKey = "2b836767763cfa11c33d22e98dfbda369b0cb1eb2fc3f5418640676a518433d5";

    private void Start()
    {
        GetLeaderboard();
    }

    public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, ((msg) =>
        {
            int loopLength = (msg.Length < nameLabels.Length) ? msg.Length : nameLabels.Length;
            for (int i = 0; i < loopLength; i++)
            {
                nameLabels[i].text = msg[i].Username;
                scoreLabels[i].text = msg[i].Score.ToString();
            }
        }));
    }

    public void SetLeaderboardEntry(string username, int score)
    {
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, score, ((_) =>
        {
            GetLeaderboard();
        }));
    }
}
