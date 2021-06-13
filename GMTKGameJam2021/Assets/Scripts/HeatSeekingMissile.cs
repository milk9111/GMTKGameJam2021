using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatSeekingMissile : MonoBehaviour
{
    public float speed;
    public float lifetimeSec = 3f;
    public bool invincible;

    public Transform _movingTarget;
    public Vector2 _staticPosition;
    private bool _seekMovingTarget;

    private Vector2 _lookAtTarget;

    public AudioClip soundFx;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.Play(soundFx);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float newX = 0f;
        float newY = 0f;
        if (_seekMovingTarget)
        {
            if (_movingTarget == null || transform == null)
            {
                Destroy(gameObject);
            }

            try
            {
                newX = Mathf.Lerp(transform.position.x, _movingTarget.position.x, speed * Time.smoothDeltaTime);
                newY = Mathf.Lerp(transform.position.y, _movingTarget.position.y, speed * Time.smoothDeltaTime);
            } catch (Exception ex)
            {

            }
            
        } else
        {
            if (Mathf.Abs(transform.position.x - _staticPosition.x) <= 0.1f && Mathf.Abs(transform.position.y - _staticPosition.y) <= 0.1f && !invincible)
            {
                Destroy(gameObject);
            }

            newX = Mathf.Lerp(transform.position.x, _staticPosition.x, speed * Time.smoothDeltaTime);
            newY = Mathf.Lerp(transform.position.y, _staticPosition.y, speed * Time.smoothDeltaTime);

            transform.LookAt(_staticPosition);
        }

        transform.position = new Vector3(newX, newY, transform.position.z);
    }

    void LateUpdate()
    {
        Vector3 diff = _lookAtTarget - (Vector2)transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }

    public void EngageMovingTarget(Transform movingTarget)
    {
        _movingTarget = movingTarget;
        _seekMovingTarget = true;
        _lookAtTarget = movingTarget.position;
    }

    public void SetStaticPosition(Vector2 staticPosition)
    {
        _staticPosition = staticPosition;
        _seekMovingTarget = false;
        _lookAtTarget = staticPosition;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyDeath>().Kill();
        }

        if (!invincible)
        {
            Destroy(gameObject);
        }
    }
}
