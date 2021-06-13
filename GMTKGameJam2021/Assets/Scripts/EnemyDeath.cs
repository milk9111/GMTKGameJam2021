using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public bool invincible;

    public AudioClip deathSound;

    private Guid _soundId;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void Kill()
    {
        if (invincible)
        {
            return;
        }

        SoundManager.Instance.Play(deathSound);

        Destroy(gameObject);
    }
}
