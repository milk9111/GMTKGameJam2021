using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public ParticleSystem particles;

    public FlyingSpotlight target;
    public float distanceBeforeAttack;
    public float repeatTimerSec = 1.5f;

    public AudioClip attackSound;

    private EnemyAI _enemyAI;

    private bool _damaging = false;
    private bool _disabled = false;

    // Start is called before the first frame update
    void Start()
    {
        _enemyAI = GetComponent<EnemyAI>();
        particles.gameObject.SetActive(false);

        if (target == null)
        {
            target = FindObjectOfType<FlyingSpotlight>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_disabled || target == null)
        {
            return;
        }

        if (Vector2.Distance(transform.position, target.transform.position) <= distanceBeforeAttack)
        {
            if (!_damaging)
            {
                InvokeRepeating("Damage", repeatTimerSec, repeatTimerSec);
                _damaging = true;
                particles.gameObject.SetActive(true);
                _enemyAI.StopSearching();
                particles.Play();
            }
        } else
        {
            CancelInvoke("Damage");
            _damaging = false;
            _enemyAI.StartSearching();
            particles.Stop();
        }
    }

    public void Disable()
    {
        _disabled = true;
        if (_damaging)
        {
            CancelInvoke("Damage");
        }
    }

    public void Enable()
    {
        _disabled = false;
        if (_damaging)
        {
            InvokeRepeating("Damage", repeatTimerSec, repeatTimerSec);
        }
    }

    private void Damage()
    {
        SoundManager.Instance.Play(attackSound);
        target.Hit();
    }
}
