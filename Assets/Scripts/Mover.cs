using UnityEngine;
using UnityEngine.Events;

public class Mover : MonoBehaviour
{
    public bool IsMoving;
    public UnityAction MovementEnded;

    public void Move(float speed, Vector3 point)
    {
        if (IsMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, point, speed * Time.deltaTime);
        }

        if (transform.position == point && IsMoving)
        {
            IsMoving = false;
            MovementEnded?.Invoke();
        }
    }
}
