using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyFollow : MonoBehaviour
{
    public bool follow;

    public Transform flyFollowTarget1;
    public Transform flyFollowTarget2;

    public float smoothTime = 0.03f;

    public float yVelocity;
    public float xVelocity;

    public Transform humanScale;

    public AudioClip followSound;

    private Transform _target;
    private Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _target = CalculateFlyFollowTarget();
    }

    void FixedUpdate()
    {
        if (!follow)
        {
            return;
        }

        _target = CalculateFlyFollowTarget();

        var newY = Mathf.SmoothDamp(transform.position.y, _target.position.y, ref yVelocity, smoothTime * Time.smoothDeltaTime);
        var newX = Mathf.SmoothDamp(transform.position.x, _target.position.x, ref xVelocity, smoothTime * Time.smoothDeltaTime);

        _rb.MovePosition(new Vector2(newX, newY));
    }

    public void SetFollow(bool newFollow)
    {
        if (newFollow)
        {
            SoundManager.Instance.Play(followSound);
        }

        follow = newFollow;
    }

    public Transform CalculateFlyFollowTarget()
    {
        return humanScale.localScale.x < 0 ? flyFollowTarget1 : flyFollowTarget2;
    }
}
