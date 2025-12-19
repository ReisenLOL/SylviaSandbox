using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuUI;
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
        SceneManager.LoadScene(1);
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
}
