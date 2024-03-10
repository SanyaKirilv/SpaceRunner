using System;
using UnityEngine;

public class Obstacle : MonoBehaviour {
    public ObstacleType type;
    public static Action<ObstacleType> onTouched;
    private void OnTriggerEnter(Collider other) 
    {
        switch (type)
        {
            case ObstacleType.Star:
                onTouched?.Invoke(ObstacleType.Star);
                Destroy(this);
                break;
            case ObstacleType.Obstacle:
                if(other.gameObject.tag == "Player")
                    onTouched?.Invoke(ObstacleType.Obstacle);
                break;
            case ObstacleType.Spawner:
                if(other.gameObject.tag == "Player")
                    onTouched?.Invoke(ObstacleType.Spawner);
                break;
        }
    }     
}
