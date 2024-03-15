using System;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public ObstacleType type;
    public static Action<ObstacleType> onTouched;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
            onTouched?.Invoke(type);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) {
            onTouched?.Invoke(type);
            if (type == ObstacleType.Star)
                Destroy(gameObject);
        }
    }
}
