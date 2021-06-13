using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnerTypes
{
    Infinite,
    Finite
}

public class Spawner : MonoBehaviour
{
    public SpawnerTypes spawnerType;
    public GameObject enemy;
    public float timeBtwSpawns;
    public int staticSpawnCount;

    public bool finished;
    private bool _infinite;

    private IList<GameObject> _instantiatedEnemies;
    private bool _disabled;

    // Start is called before the first frame update
    void Start()
    {
        _instantiatedEnemies = new List<GameObject>();
        finished = false;
        _infinite = spawnerType == SpawnerTypes.Infinite;
        _disabled = false;
    }

    public void BeginSpawning()
    {
        //_infinite = isInfinite;
        InvokeRepeating("Spawn", timeBtwSpawns, timeBtwSpawns);
    }

    private void Spawn()
    {
        if (_disabled)
        {
            return;
        }

        if (!_infinite && staticSpawnCount <= 0)
        {
            Finish();
            return;
        }

        var obj = Instantiate(enemy, transform.position, Quaternion.identity);
        _instantiatedEnemies.Add(obj);

        if (!_infinite)
        {
            staticSpawnCount--;
        }
    }

    public void Disable()
    {
        _disabled = true;
        foreach(var enemy in _instantiatedEnemies)
        {
            enemy.GetComponent<EnemyAI>().Disable();
            enemy.GetComponent<EnemyAttack>().Disable();
        }
    }

    public void Enable()
    {
        _disabled = false;
        foreach (var enemy in _instantiatedEnemies)
        {
            enemy.GetComponent<EnemyAI>().Enable();
            enemy.GetComponent<EnemyAttack>().Enable();
        }
    }

    public void Finish()
    {
        finished = true;
        CancelInvoke("Spawn");
    }

     
}
