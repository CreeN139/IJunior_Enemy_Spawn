using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Mover))]
public class Enemy : MonoBehaviour
{
    [Range(1f, 2f)]
    [SerializeField] private float _speed = 1.5f;
    [SerializeField] private float _lifeTime = 5f;
    private Mover _mover;
    private Vector3 _directionToMove;
    private WaitForSeconds _lifeDelay;
    private Coroutine _coroutine;

    public event Action<Enemy> LifeEnded;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _lifeDelay = new WaitForSeconds(_lifeTime);
    }

    private void Update()
    {
        _mover.Move(_speed, _directionToMove);

        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(WaitForDeactivate());
        }
    }

    public void Initialize(Vector3 direction)
    {
        _directionToMove = direction;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    private IEnumerator WaitForDeactivate()
    {
        yield return _lifeDelay;
        LifeEnded?.Invoke(this);
        _coroutine = null;
    }
}
