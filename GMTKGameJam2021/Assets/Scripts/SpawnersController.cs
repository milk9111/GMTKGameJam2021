using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnersController : MonoBehaviour
{
    private IList<Spawner> _spawners;

    // Start is called before the first frame update
    void Start()
    {
        _spawners = new List<Spawner>(FindObjectsOfType<Spawner>());
    }

    public void Disable()
    {
        foreach (var spawner in _spawners)
        {
            spawner.Disable();
        }
    }

    public void Enable()
    {
        foreach (var spawner in _spawners)
        {
            spawner.Enable();
        }
    }
}
