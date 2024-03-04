using System;
using UnityEngine;

public class Obstacle : MonoBehaviour {
    public ObstacleType type;
    public static Action<ObstacleType> onTouched;
    private void OnTriggerEnter(Collider other) => onTouched?.Invoke(type);
}
