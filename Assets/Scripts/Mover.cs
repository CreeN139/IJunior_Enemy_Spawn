using UnityEngine;

public class Mover : MonoBehaviour
{
    public void Move(float speed, Vector3 direction)
    {
        transform.position = transform.position + direction * speed * Time.deltaTime;
    }
}
