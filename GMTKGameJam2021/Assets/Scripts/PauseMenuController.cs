using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public GameObject panel;
    public CharacterSwitch characterSwitch;
    public SpawnersController spawnersController;

    private bool _paused;

    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
        _paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_paused)
            {
                UnPause();
            } else
            {
                _paused = true;
                panel.SetActive(true);
                characterSwitch.Disable();
                spawnersController.Disable();
            }
        }   
    }

    public void UnPause()
    {
        _paused = false;
        panel.SetActive(false);
        characterSwitch.Enable();
        spawnersController.Enable();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void TitleScreen()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
