using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [Header("Game Over UI")]
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private TextMeshProUGUI gameOverScoreText;
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
        gameOverScoreText.text = $"GAME OVER!\nScore: {Math.Floor(score)}";
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
}
