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

    public AudioClip walkingSound;

    public AudioClip backgroundMusic;
    private bool _startedMusic;

    private Animator _animator;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();

        additionalJumps = defaultAdditionalJumps;
    }

    void Update()
    {
        if (!_startedMusic)
        {
            SoundManager.Instance.PlayMusic(backgroundMusic);
            _startedMusic = true;
        }

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

        _animator.SetBool("Walking", Mathf.Abs(moveBy) > 0);

        var flipX = transform.localScale.x;
        if (x != 0)
        {
            flipX = x < 0 ? Mathf.Abs(transform.localScale.x) * -1 : Mathf.Abs(transform.localScale.x);
        }

        transform.localScale = new Vector3(flipX, transform.localScale.y, transform.localScale.z);
        flyingMovement.SetFlipped(flipX / Mathf.Abs(flipX));
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor || additionalJumps > 0))
        {
            SoundManager.Instance.Play(walkingSound);
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
}
