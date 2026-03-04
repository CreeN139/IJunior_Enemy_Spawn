using UnityEngine;
using System.Collections;

public class SpawnSelector : MonoBehaviour
{
    [SerializeField] private float _timeBetweenSpawn;
    [SerializeField] private Spawner[] _spawners;

    private void Start()
    {
        StartCoroutine(SelectSpawnRoutine());
    }

    private IEnumerator SelectSpawnRoutine()
    {
        WaitForSeconds Delay = new WaitForSeconds(_timeBetweenSpawn);

        while (enabled)
        {
            yield return Delay;

            int randomIndex = Random.Range(0, _spawners.Length);
            _spawners[randomIndex].SpawnEnemy();
        }
    }
}
