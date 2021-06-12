using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAttack : MonoBehaviour
{
    public GameObject magicMissile;

    private bool _disable;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_disable)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            var missile = Instantiate(magicMissile, transform.position, Quaternion.identity).GetComponent<HeatSeekingMissile>();

            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    missile.EngageMovingTarget(hit.collider.transform);
                } else
                {
                    missile.SetStaticPosition(hit.point);
                }
            } else
            {
                missile.SetStaticPosition(mousePos);
            }
        }
    }

    public void SetDisable(bool disable)
    {
        _disable = disable;
    }
}
