using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    private ObjectPool<Enemy> _pool;
    private int _poolDefaultCapacity = 10;
    private int _poolMaxSize = 20;
    private float _radiusOfSpawn = 30f;

    private void Awake()
    {
        _pool = new ObjectPool<Enemy>(
            createFunc: () => Create(),
            actionOnGet: (obj) => obj.gameObject.SetActive(true),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolDefaultCapacity,
            maxSize: _poolMaxSize);
    }

    public void PrepareEnemy()
    {
        Enemy enemy = GetSpawnedEnemy();
        Vector3 point = GetRandomPoint();
        enemy.SetPoint(point);
        enemy.transform.LookAt(point);
        enemy.GetComponent<Mover>().IsMoving = true;
    }

    private Enemy Create()
    {
        GameObject cube = Instantiate(_prefab);
        cube.transform.SetParent(transform);
        Enemy enemy = cube.GetComponent<Enemy>();
        
        return enemy;
    }

    private Enemy GetSpawnedEnemy()
    {
        Enemy enemy = _pool.Get();
        enemy.transform.position = transform.position;
        enemy.LifeEnded += ReleaseToPool;

        return enemy;
    }

    private Vector3 GetRandomPoint()
    {
        Vector2 randomCircle = Random.insideUnitCircle * _radiusOfSpawn;
        Vector3 randomPoint = transform.position + new Vector3(randomCircle.x, 0f, randomCircle.y);


        return randomPoint;
    }

    private void ReleaseToPool(Enemy enemy)
    {
        enemy.LifeEnded -= ReleaseToPool;
        _pool.Release(enemy);
    }
}
