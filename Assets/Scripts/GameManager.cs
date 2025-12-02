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
    public TextMeshProUGUI scoreUI;
    [Header("Game Over UI")]
    public GameObject gameOverUI;
    public TextMeshProUGUI gameOverScoreText;
    public void UpdateScore()
    {
        score += 1;
        scoreUI.text = $"Score: {score}";
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverUI.SetActive(true);
        gameOverScoreText.text = $"GAME OVER!\nScore: {score}";
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        
    }
}
