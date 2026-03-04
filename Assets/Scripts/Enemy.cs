using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Mover))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _lifeTime = 4f;
    private Mover _mover;
    private Vector3 _directionToMove;

    public event Action<Enemy> LifeEnded;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
    }

    private void Update()
    {
        _mover.Move(_speed, _directionToMove);
    }

    private void OnEnable()
    {
        _mover.MovementEnded += StartRoutine;
    }

    private void OnDisable()
    {
        _mover.MovementEnded -= StartRoutine;
    }

    public void Initialize(Vector3 direction)
    {
        _directionToMove = transform.position + direction;
        transform.rotation = Quaternion.LookRotation(direction);
        _mover.StartMovement();
    }

    private IEnumerator WaitForDeactivate()
    {
        yield return new WaitForSeconds(_lifeTime);
        LifeEnded?.Invoke(this);
    }

    private void StartRoutine()
    {
        StartCoroutine(WaitForDeactivate());
    }
}
