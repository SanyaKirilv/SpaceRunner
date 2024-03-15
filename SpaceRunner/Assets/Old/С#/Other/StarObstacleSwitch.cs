using System.Collections.Generic;
using UnityEngine;

public class StarObstacleSwitch : MonoBehaviour
{
    public GameObject[] obj;

    void Start() 
    {
        obj[Random.Range(0,2)].SetActive(true);
    }

}