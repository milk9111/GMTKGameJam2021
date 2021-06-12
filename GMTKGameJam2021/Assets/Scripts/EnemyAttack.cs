using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public ParticleSystem particles;

    public FlyingSpotlight target;
    public float distanceBeforeAttack;
    public float repeatTimerSec = 1.5f;

    private EnemyAI _enemyAI;

    private bool _damaging = false;

    // Start is called before the first frame update
    void Start()
    {
        _enemyAI = GetComponent<EnemyAI>();
        particles.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
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

    private void Damage()
    {
        target.Hit();
    }
}
