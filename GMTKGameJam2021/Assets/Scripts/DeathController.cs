using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathController : MonoBehaviour
{
    public FlyingSpotlight flyingSpotlight;
    public HumanSpotlight humanSpotlight;
    public CharacterSwitch characterSwitch;

    public GameObject gameOverMenu;

    public AudioClip deathSound;
    public AudioClip restartSound;

    public bool dead;

    private Guid _soundId;

    void Start()
    {
        gameOverMenu.SetActive(false);
        //_soundId = SoundManager.Instance.PrepareSound("player_death");
    }

    public void Restart()
    {
        SoundManager.Instance.Play(restartSound);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Dead()
    {
        if (!dead)
        {
            dead = true;
            characterSwitch.disabled = true;
            flyingSpotlight.gameObject.SetActive(false);
            humanSpotlight.gameObject.SetActive(false);
            SoundManager.Instance.Play(deathSound);
            gameOverMenu.SetActive(true);
        }
    }
}
