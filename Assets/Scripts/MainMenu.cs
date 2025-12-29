using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuUI;
    public TMP_InputField nameEntryUI;
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeSpeed;
    public void OpenMenu(GameObject menuToOpen)
    {
        menuToOpen.SetActive(true);
        mainMenuUI.SetActive(false);
    }

    public void CloseMenu(GameObject menuToClose)
    {
        menuToClose.SetActive(false);
        mainMenuUI.SetActive(true);
    }

    public void StartGame()
    {
        LeaderboardManager.instance.chosenUsername = nameEntryUI.text;
        Time.timeScale = 1;
        StartCoroutine(FadeToBlack());
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        {
            EditorApplication.ExitPlaymode();
        }
#else
        {
        Application.Quit();
        }
#endif
    }

    private IEnumerator FadeToBlack()
    {
        float currentState = 0;
        while (currentState < 1)
        {
            currentState += Time.deltaTime * fadeSpeed;
            fadeImage.color = Color.Lerp(Color.clear, Color.black, currentState);
            yield return null;
        }
        SceneManager.LoadScene(1);
    }
}
