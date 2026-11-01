using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LogicScript : MonoBehaviour
{
    public float slowDownSpeed;

    [SerializeField] private TextMeshProUGUI restartScreen;
    [SerializeField] private Volume volume;
    [SerializeField] private ChromaticAberration chrome;
    [SerializeField] private float transitionSpeed;

    void Start()
    {
        volume.profile.TryGet<ChromaticAberration>(out chrome);
    }

    public void SlowDown()
    {
        if(Time.timeScale > 0.1f)
        {
            Time.timeScale -= 0.05f;
        }
        
        Time.fixedDeltaTime = 0.02f * slowDownSpeed;
        chrome.intensity.value = Mathf.Lerp(chrome.intensity.value, 1f, Time.deltaTime * transitionSpeed);
    }

    public void SpeedUp()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.fixedUnscaledDeltaTime;

        chrome.intensity.value = Mathf.Lerp(chrome.intensity.value, 0f, Time.deltaTime * transitionSpeed); ;
    }

    public void GameOver()
    {
        restartScreen.gameObject.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
