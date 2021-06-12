using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderIgnore : MonoBehaviour
{
    private Collider2D _collider;

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(collision.collider, _collider);
            return;
        }
    }
}
