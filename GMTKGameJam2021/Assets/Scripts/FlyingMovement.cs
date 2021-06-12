using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMovement : MonoBehaviour
{
    public bool active;
    public float speed;

    private Rigidbody2D _rb;
    private SpriteRenderer _renderer;
    private FlyFollow _follow;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _follow = GetComponent<FlyFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!active)
        {
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = 0;
            return;
        }

        float x = Input.GetAxisRaw("Horizontal");
        float moveByX = x * speed;

        var flipX = _renderer.flipX;
        if (x != 0)
        {
            flipX = x < 0;
        }

        float y = Input.GetAxisRaw("Vertical");
        float moveByY = y * speed;
        _rb.velocity = new Vector2(moveByX, moveByY);
        _renderer.flipX = flipX;

        if (x != 0 || y != 0)
        {
            _follow.SetFollow(false);
        }
    }

    public void SetFlipped(bool flipX)
    {
        if (_follow.follow)
        {
            _renderer.flipX = flipX;
        }
    }
}
