using TMPro;
using UnityEngine;

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
    [Header("UI")] 
    public TextMeshProUGUI scoreUI;
}
