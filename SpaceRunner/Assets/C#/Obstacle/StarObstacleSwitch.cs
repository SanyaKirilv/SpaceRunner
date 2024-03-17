using UnityEngine;

public class StarObstacleSwitch : MonoBehaviour
{
    private void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(Random.Range(0, 10) < 9 ? 0 : 1).gameObject.SetActive(true);
    }
}