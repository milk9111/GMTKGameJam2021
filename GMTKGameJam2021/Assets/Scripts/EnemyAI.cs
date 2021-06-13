using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform target;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    private Path _path;
    private int _currentWaypoint = 0;
    private bool _reachedEndOfPath = false;
    private Seeker _seeker;
    private Rigidbody2D _rb;
    private bool _searching = true;
    private bool _disabled = false;

    // Start is called before the first frame update
    void Start()
    {
        _seeker = GetComponent<Seeker>();
        _rb = GetComponent<Rigidbody2D>();

        if (target == null)
        {
            target = FindObjectOfType<FlyingSpotlight>().transform;
        }

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
        if (_disabled || !_searching || _path == null)
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

        var p = (Vector2)_path.vectorPath[_currentWaypoint];
        var rbPos = _rb.position;
        var unnormalizedDirection = p - rbPos;

        var direction = unnormalizedDirection.normalized;
        var force = direction * speed * Time.deltaTime;

        _rb.AddForce(force);

        var distance = Vector2.Distance(_rb.position, _path.vectorPath[_currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            _currentWaypoint++;
        }

        if (unnormalizedDirection.x > 0.3f)
        {
            //Debug.Log("Facing Right");
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (unnormalizedDirection.x < -0.3f)
        {
            //Debug.Log("Facing Left: " + direction);
            //Debug.Log($"p: {p}");
            //Debug.Log($"rbPos: {rbPos}");
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    public void Disable()
    {
        _disabled = true;
    }

    public void Enable()
    {
        _disabled = false;
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
