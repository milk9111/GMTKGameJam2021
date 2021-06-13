using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortOrderAdjuster : MonoBehaviour
{
    private List<SpriteRenderer> _renderers;

    // Start is called before the first frame update
    void Start()
    {
        _renderers = new List<SpriteRenderer>(GetComponents<SpriteRenderer>());
        _renderers.AddRange(GetComponentsInChildren<SpriteRenderer>());
    }

    public void SetSortOrder(int sortOrder)
    {
        if (_renderers == null)
        {
            return;
        }

        foreach(var render in _renderers)
        {
            render.sortingOrder = sortOrder;
        }
    }
}
