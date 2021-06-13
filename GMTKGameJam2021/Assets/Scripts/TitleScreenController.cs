using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenController : MonoBehaviour
{
    public Image titlescreenPanel;
    public Sprite normalTitlescreen;
    public Sprite alternateTitlescreen;

    public Button alternateButton;
    public Button normalButton;

    public AudioClip alternateStartMusic;

    void Start()
    {
        alternateButton.gameObject.SetActive(true);
        normalButton.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        SoundManager.Instance.StopMusic();
        SceneManager.LoadScene("Level1");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void AlternateTitleScreen()
    {
        titlescreenPanel.sprite = alternateTitlescreen;
        alternateButton.gameObject.SetActive(false);
        normalButton.gameObject.SetActive(true);
        SoundManager.Instance.PlayMusic(alternateStartMusic);
    }

    public void NormalTitleScreen()
    {
        titlescreenPanel.sprite = normalTitlescreen;
        alternateButton.gameObject.SetActive(true);
        normalButton.gameObject.SetActive(false);
        SoundManager.Instance.StopMusic();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
