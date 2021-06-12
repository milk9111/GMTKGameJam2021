using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanMovement : MonoBehaviour
{
    Rigidbody2D _rb;
    SpriteRenderer _renderer;

    public float speed;
    public float jumpForce;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    public bool active;

    bool isGrounded = false;
    public Transform isGroundedChecker;
    public float checkGroundRadius;
    public LayerMask groundLayer;

    public float rememberGroundedFor;
    float lastTimeGrounded;

    public int defaultAdditionalJumps = 1;
    int additionalJumps;

    public FlyingMovement flyingMovement;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();

        additionalJumps = defaultAdditionalJumps;
    }

    void Update()
    {
        if (!active)
        {
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = 0;
            _rb.gravityScale = -(Physics2D.gravity * (fallMultiplier - 1)).y;
            return;
        } else
        {
            _rb.gravityScale = 1;
        }

        Move();
        Jump();
        BetterJump();
        CheckIfGrounded();
    }


    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float moveBy = x * speed;
        _rb.velocity = new Vector2(moveBy, _rb.velocity.y);

        var flipX = _renderer.flipX;
        if (x != 0)
        {
            flipX = x < 0;
        }

        _renderer.flipX = flipX;
        flyingMovement.SetFlipped(flipX);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor || additionalJumps > 0))
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
            additionalJumps--;
        }
    }

    void BetterJump()
    {
        if (_rb.velocity.y < 0)
        {
            _rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (_rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            _rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void CheckIfGrounded()
    {
        Collider2D colliders = Physics2D.OverlapCircle(isGroundedChecker.position, checkGroundRadius, groundLayer);

        if (colliders != null)
        {
            isGrounded = true;
            additionalJumps = defaultAdditionalJumps;
        }
        else
        {
            if (isGrounded)
            {
                lastTimeGrounded = Time.time;
            }
            isGrounded = false;
        }
    }

    //[Range(0.01f, 1)]
    /*public float speedMultiplier;
    public float maximumSpeed;

    public float jumpHeight = 10;
    public LayerMask groundLayer;

    private Rigidbody2D _rb;
    private float _horizontal;
    private bool _pressedJump;
    private bool _pressedHorizontal;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _pressedJump = false;
        _pressedHorizontal = false;
    }

    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");

        var jump = Input.GetKeyDown(KeyCode.Space);
        if (jump && IsGrounded())
        {
            _pressedJump = true;
        }
    }

    void FixedUpdate()
    {
        var changedPos = false;
        var x = 0f;
        var normalizedHorizontal = Mathf.Abs(_horizontal);
        if (normalizedHorizontal > 0.01f)
        {
            changedPos = true;
            _pressedHorizontal = true;
            x = _horizontal * speedMultiplier * Time.deltaTime;
        }

        

        var y = 0f;
        if (_pressedJump)
        {
            changedPos = true;
            _pressedJump = false;
            y = jumpHeight;
        }

        if (changedPos)
        {
            _rb.AddForce(new Vector2(x, 0));
            _rb.AddForce(new Vector2(0, y), ForceMode2D.Impulse);
        }

        var speed = Vector3.Magnitude(_rb.velocity);  // test current object speed

        // let go of the key so slow down
        if (normalizedHorizontal <= 0.01f && _pressedHorizontal)
        {
            _pressedHorizontal = false;
            ApplyBrake(speed * maximumSpeed);
        }

        if (speed > maximumSpeed)
        {
            Debug.Log("Reached max speed");
            ApplyBrake(speed - maximumSpeed);
        }
    }

    private void ApplyBrake(float brakeSpeed)
    {
        Vector3 normalisedVelocity = _rb.velocity.normalized;
        Vector3 brakeVelocity = normalisedVelocity * brakeSpeed;  // make the brake Vector3 value

        _rb.AddForce(-brakeVelocity);  // apply opposing brake force
    }

    private bool IsGrounded()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 1.0f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null)
        {
            return true;
        }

        return false;
    }*/
}
