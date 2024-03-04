using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int currentScore;
    [SerializeField] private int maxScore;


    private void OnEnable() => Obstacle.onTouched += ActionHandler;
    private void OnDisable() => Obstacle.onTouched -= ActionHandler;

    private void ActionHandler(ObstacleType type)
    {
        if(type == ObstacleType.Coin)
            UpdateScore();
        if(type == ObstacleType.Obstacle)
            Lose();
        if(type == ObstacleType.Spawner)
            SpawnNext();
    }

    private void UpdateScore()
    {
        currentScore++;
        maxScore = currentScore > maxScore ? currentScore : maxScore;
    }

    private void Lose()
    {

    }

    private void SpawnNext()
    {

    }
}
