using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Mover))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _lifeTime = 4f;
    private Mover _mover;
    private Vector3 _pointToMove;

    public UnityAction<Enemy> LifeEnded;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
    }

    private void Update()
    {
        _mover.Move(_speed, _pointToMove);
    }

    private void OnEnable()
    {
        _mover.MovementEnded += () => StartCoroutine(WaitForDeactivate());
    }

    private void OnDisable()
    {
        _mover.MovementEnded -= () => StartCoroutine(WaitForDeactivate());
    }

    public void SetPoint(Vector3 point)
    {
        _pointToMove = point;
    }

    private IEnumerator WaitForDeactivate()
    {
        yield return new WaitForSeconds(_lifeTime);
        LifeEnded?.Invoke(this);
    }
}
