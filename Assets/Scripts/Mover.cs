using UnityEngine;
using System;

public class Mover : MonoBehaviour
{
    private float _sqrAcceptableDifference = 0.01f * 0.01f;
    private bool _isMoving;
    public event Action MovementEnded;

    public void Move(float speed, Vector3 target)
    {
        if (_isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }

        if ((target - transform.position).sqrMagnitude < _sqrAcceptableDifference && _isMoving)
        {
            _isMoving = false;
            MovementEnded?.Invoke();
        }
    }

    public void StartMovement()
    {
        _isMoving = true;
    }
}
