using UnityEngine;
using System.Collections;

public class SpawnSelector : MonoBehaviour
{
    [SerializeField] private float _timeBetweenSpawn;

    private Spawner[] _spawners;

    private void Awake()
    {
        _spawners = GetComponentsInChildren<Spawner>();
    }

    private void Start()
    {
        StartCoroutine(SelectSpawnRoutine());
    }

    private IEnumerator SelectSpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeBetweenSpawn);

            int randomIndex = Random.Range(0, _spawners.Length);
            _spawners[randomIndex].PrepareEnemy();
        }
    }
}
