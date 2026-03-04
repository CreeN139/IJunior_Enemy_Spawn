using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _prefab;
    private ObjectPool<Enemy> _pool;
    private int _poolDefaultCapacity = 10;
    private int _poolMaxSize = 20;
    private float _minLengh = 10f;
    private float _maxLengh = 25f;
    private Vector3[] _directions = new Vector3[] {Vector3.forward, Vector3.back, Vector3.left, Vector3.right};

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

    public void SpawnEnemy()
    {
        Enemy enemy = GetSpawnedEnemy();

        if (enemy.TryGetComponent<Mover>(out _))
        {
            Vector3 direction = GetRandomDirection();
            float directionLengh = Random.Range(_minLengh, _maxLengh);
            Vector3 correctedDirection = direction * directionLengh;
            enemy.Initialize(correctedDirection);
        }
    }

    private Enemy Create()
    {
        Enemy enemy = Instantiate(_prefab);
        enemy.transform.SetParent(transform);

        return enemy;
    }

    private Enemy GetSpawnedEnemy()
    {
        Enemy enemy = _pool.Get();
        enemy.transform.position = transform.position;
        enemy.LifeEnded += ReleaseToPool;

        return enemy;
    }

    private Vector3 GetRandomDirection()
    {
        int randomIndex = Random.Range(0, _directions.Length);

        return _directions[randomIndex];
    }

    private void ReleaseToPool(Enemy enemy)
    {
        enemy.LifeEnded -= ReleaseToPool;
        _pool.Release(enemy);
    }
}
