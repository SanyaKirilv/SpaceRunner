using System;
using UnityEngine;

public class Obstacle : MonoBehaviour 
{
    public ObstacleType type;
    public static Action<ObstacleType> onTouched;
    
    private void OnCollisionEnter(Collision other)
    {
        if(type == ObstacleType.Obstacle)
            if(other.gameObject.tag == "Player")
                    onTouched?.Invoke(ObstacleType.Obstacle);
    } 

    private void OnTriggerEnter(Collider other)
    {
        switch (type) 
        {
            case ObstacleType.Star:
                onTouched?.Invoke(ObstacleType.Star);
                Destroy(gameObject);
                break;
            case ObstacleType.Spawner:
                if(other.gameObject.tag == "Player")
                    onTouched?.Invoke(ObstacleType.Spawner);
                break;
        }
    }    
}
