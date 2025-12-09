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
    [SerializeField] private TextMeshProUGUI scoreUI;
    [SerializeField] private TextMeshProUGUI hitUI;
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
            if (currentTime > hitTimeout)
            {
                hitsAmount = 0;
                currentTime = 0;
                hitUI.text = $"Hits: {hitsAmount}";
            }
        }
    }

    public void UpdateHits()
    {
        hitsAmount++;
        currentTime = 0;
        hitUI.text = $"Hits: {hitsAmount}";
    }

    public void UpdateScore()
    {
        score += 1 * (1+(hitsAmount / 10f));
        scoreUI.text = $"Score: {Math.Floor(score)}";
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverUI.SetActive(true);
        gameOverScoreText.text = $"GAME OVER!\nScore: {Math.Floor(score)}";
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        
    }
}
