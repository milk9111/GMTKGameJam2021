using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyFlipper : MonoBehaviour
{
    public SpriteRenderer _renderer;

    private AIPath aiPath;

    // Start is called before the first frame update
    void Start()
    {
        aiPath = GetComponent<AIPath>();
    }

    // Update is called once per frame
    void Update()
    {
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            _renderer.flipX = false;
        } else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            _renderer.flipX = true;
        }
    }
}
