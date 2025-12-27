using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Statication

    public static GameManager instance;

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
    //this is where we handle the scoring...
    [Header("Score")]
    public float score;

    [Header("Main UI")] 
    [SerializeField] private GameObject gameUI;
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private TextMeshProUGUI hitUI;
    [SerializeField] private Transform hitTimerUI;
    public Image chargeUI; //adding this here because GameObject.find is stinky.
    [Header("Pause")]
    [SerializeField] private GameObject pauseUI;
    private bool pause = false;
    [Header("Game Over UI")]
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private TextMeshProUGUI gameOverScoreText;
    [SerializeField] private TextMeshProUGUI roundText;
    [SerializeField] private TextMeshProUGUI timeText;
    [Header("Hits")] 
    public int hitsAmount;
    [SerializeField] private float hitTimeout;
    private float currentTime;
    private void Update()
    {
        if (hitsAmount > 0)
        {
            currentTime += Time.deltaTime;
            hitTimerUI.localScale = new Vector3(1-(currentTime/hitTimeout), hitTimerUI.localScale.y, hitTimerUI.localScale.z);
            if (currentTime > hitTimeout)
            {
                hitsAmount = 0;
                currentTime = 0;
                hitUI.text = $"Hits: {hitsAmount}";
                hitUI.gameObject.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetPause();
        }
    }

    public void UpdateHits()
    {
        hitsAmount++;
        currentTime = 0;
        hitUI.text = $"Hits: {hitsAmount}";
        hitUI.gameObject.SetActive(true);
    }

    public void UpdateScore()
    {
        score += 1 * (1+(hitsAmount / 10f));
        scoreUI.text = $"{Math.Floor(score)}";
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverUI.SetActive(true);
        gameUI.SetActive(false);
        gameOverScoreText.text = $"{Math.Floor(score)}";
        roundText.text = $"{RoundManager.instance.roundNumber}";
        timeText.text = TimeSpan.FromSeconds(Time.timeSinceLevelLoad).ToString(@"mm\:ss\.ff");
        LeaderboardManager.instance.SetLeaderboardEntry(LeaderboardManager.instance.chosenUsername, (int)score);
        LeaderboardManager.instance.UpdateHighScores((int)score, RoundManager.instance.roundNumber, Time.timeSinceLevelLoad);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(0);
    }

    public void SetPause()
    {
        pause = !pause;
        if (pause)
        {
            Time.timeScale = 0;
            pauseUI.SetActive(true);
            gameUI.SetActive(false);
        }
        else
        {
            
            Time.timeScale = 1;
            pauseUI.SetActive(false);
            gameUI.SetActive(true);
        }
    }
}
