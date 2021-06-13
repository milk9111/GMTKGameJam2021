using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantDeathTouch : MonoBehaviour
{
    private DeathController _deathController;

    void Start ()
    {
        _deathController = FindObjectOfType<DeathController>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _deathController.Dead();
        }
    }
}
