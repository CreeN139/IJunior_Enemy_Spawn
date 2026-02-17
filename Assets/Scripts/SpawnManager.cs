using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SpawnManager : MonoBehaviour
{
    private List<Spawner> _spawners;
    private float _timeBetweenSpawn = 2f;

    private void Awake()
    {
        _spawners = Object.FindObjectsByType<Spawner>(FindObjectsSortMode.None).ToList();
    }

    private void Start()
    {
        InvokeRepeating(nameof(Work), 0f, _timeBetweenSpawn);
    }

    private Spawner SelectSpawner()
    {
        return _spawners[Random.Range(0, _spawners.Count)];
    }

    private void Work()
    {
        SelectSpawner().PrepareEnemy();
    }
}
