using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject optionsScreen;

    public void Play()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Options()
    {
        optionsScreen.SetActive(true);
    }

    public void Back()
    {
        optionsScreen.SetActive(false);
    }

    public void Menu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
