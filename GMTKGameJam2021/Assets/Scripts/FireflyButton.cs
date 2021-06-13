using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyButton : MonoBehaviour
{
    public Sprite normal;
    public Sprite pressed;

    public GameObject[] tileGroups;

    private SpriteRenderer _renderer;

    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Bug")
        {
            foreach(var tileGroup in tileGroups)
            {
                tileGroup.SetActive(!tileGroup.activeSelf);
            }
            
            _renderer.sprite = pressed;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Bug")
        {
            foreach (var tileGroup in tileGroups)
            {
                tileGroup.SetActive(!tileGroup.activeSelf);
            }

            _renderer.sprite = normal;
        }
    }
}
