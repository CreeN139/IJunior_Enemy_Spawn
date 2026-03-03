using UnityEngine;
using UnityEngine.Events;

public class Mover : MonoBehaviour
{
    private float _sqrAcceptableDifference = 0.01f * 0.01f;
    public bool IsMoving;
    public UnityAction MovementEnded;

    public void Move(float speed, Vector3 target)
    {
        if (IsMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }

        if ((target - transform.position).sqrMagnitude < _sqrAcceptableDifference && IsMoving)
        {
            IsMoving = false;
            MovementEnded?.Invoke();
        }
    }
}
