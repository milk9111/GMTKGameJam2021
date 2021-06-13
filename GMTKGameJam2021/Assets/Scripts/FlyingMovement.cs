using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMovement : MonoBehaviour
{
    public bool active;
    public float speed;

    private Rigidbody2D _rb;
    private FlyFollow _follow;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
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

        var flipX = transform.localScale.x;
        if (x != 0 )
        {
            flipX = x < 0 ? Mathf.Abs(transform.localScale.x) * -1 : Mathf.Abs(transform.localScale.x);
        }

        float y = Input.GetAxisRaw("Vertical");
        float moveByY = y * speed;
        _rb.velocity = new Vector2(moveByX, moveByY);
        transform.localScale = new Vector3(flipX, transform.localScale.y, transform.localScale.z);

        if (x != 0 || y != 0)
        {
            _follow.SetFollow(false);
        }
    }

    public void SetFlipped(float flipX)
    {
        if (_follow.follow)
        {
            var flip = Mathf.Abs(transform.localScale.x);
            if (flipX < 0)
            {
                flip = -flip;
            }

            transform.localScale = new Vector3(flip, transform.localScale.y, transform.localScale.z);
        }
    }
}
