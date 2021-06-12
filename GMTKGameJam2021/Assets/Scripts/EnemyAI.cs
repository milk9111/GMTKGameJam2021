using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform target;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    public SpriteRenderer spriteRenderer;

    private Path _path;
    private int _currentWaypoint = 0;
    private bool _reachedEndOfPath = false;
    private Seeker _seeker;
    private Rigidbody2D _rb;
    private bool _searching = true;

    // Start is called before the first frame update
    void Start()
    {
        _seeker = GetComponent<Seeker>();
        _rb = GetComponent<Rigidbody2D>();

        _searching = true;
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    private void UpdatePath()
    {
        if (_seeker.IsDone())
        {
            _seeker.StartPath(_rb.position, target.position, OnPathComplete);
        }
    }

    private void OnPathComplete(Path path)
    {
        if (!path.error)
        {
            _path = path;
            _currentWaypoint = 0;
        }
    }

    void FixedUpdate()
    {
        if (!_searching || _path == null)
        {
            return;
        }

        if (_currentWaypoint >= _path.vectorPath.Count)
        {
            _reachedEndOfPath = true;
            return;
        } else
        {
            _reachedEndOfPath = false;
        }

        var direction = ((Vector2)_path.vectorPath[_currentWaypoint] - _rb.position).normalized;
        var force = direction * speed * Time.deltaTime;

        _rb.AddForce(force);

        var distance = Vector2.Distance(_rb.position, _path.vectorPath[_currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            _currentWaypoint++;
        }

        if (force.x >= 0.01f)
        {
            spriteRenderer.flipX = false;
        }
        else if (force.x <= -0.01f)
        {
            spriteRenderer.flipX = true;
        }
    }

    public void StopSearching()
    {
        _searching = false;
        _rb.velocity = Vector2.zero;
        _rb.angularVelocity = 0;
    }

    public void StartSearching()
    {
        _searching = true;
    }
}
