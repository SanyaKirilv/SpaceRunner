using System;
using System.Collections.Generic;
using UnityEngine;

public class PipeCreator : MonoBehaviour
{
    public List<string> variants = new() {
        "0000",
        "0001",
        "0010",
        "0011",
        "0100",
        "0101",
        "0110",
        "0111",
        "1000",
        "1001",
        "1010",
        "1011",
        "1100",
        "1101",
        "1110",
        "1111"
    };

    public GameObject prefab;
    void Start()
    {
        for(int i = 0; i < variants.Count; i++)
        {
            var _pipe = Instantiate(prefab, new Vector3(20*i, 0, 0), Quaternion.identity);
            for(int q = 0; q < 4; q++)
            {
                _pipe.GetComponent<Pipe>().down[q].gameObject.SetActive(variants[i][q] == '1');
                _pipe.GetComponent<Pipe>().up[q].gameObject.SetActive(false);
                _pipe.GetComponent<Pipe>().obstacles[q].gameObject.SetActive(false);
                _pipe.name = "Section";
            }
        }
        for(int i = 0; i < variants.Count; i++)
        {
            var _pipe = Instantiate(prefab, new Vector3(20*i, 20, 0), Quaternion.identity);
            for(int q = 0; q < 4; q++)
            {
                _pipe.GetComponent<Pipe>().down[q].gameObject.SetActive(variants[i][q] == '1');
                _pipe.GetComponent<Pipe>().up[q].gameObject.SetActive(variants[i][q] != '1');
                _pipe.GetComponent<Pipe>().obstacles[q].gameObject.SetActive(false);
                _pipe.name = "Section";
            }
        }
        for(int i = 0; i < variants.Count; i++)
        {
            var _pipe = Instantiate(prefab, new Vector3(20*i, 40, 0), Quaternion.identity);
            for(int q = 0; q < 4; q++)
            {
                _pipe.GetComponent<Pipe>().down[q].gameObject.SetActive(variants[i][q] == '1');
                _pipe.GetComponent<Pipe>().up[q].gameObject.SetActive(variants[i][q] != '1');
                _pipe.GetComponent<Pipe>().obstacles[q].gameObject.SetActive(variants[i][q] != '1');
                _pipe.name = "Section";
            }
        }
        for(int i = 0; i < variants.Count; i++)
        {
            var _pipe = Instantiate(prefab, new Vector3(20*i, 60, 0), Quaternion.identity);
            for(int q = 0; q < 4; q++)
            {
                _pipe.GetComponent<Pipe>().down[q].gameObject.SetActive(variants[i][q] == '1');
                _pipe.GetComponent<Pipe>().up[q].gameObject.SetActive(false);
                _pipe.GetComponent<Pipe>().obstacles[q].gameObject.SetActive(variants[i][q] != '1');
                _pipe.name = "Section";
            }
        }
        for(int i = 0; i < variants.Count; i++)
        {
            var _pipe = Instantiate(prefab, new Vector3(20*i, 80, 0), Quaternion.identity);
            for(int q = 0; q < 4; q++)
            {
                _pipe.GetComponent<Pipe>().down[q].gameObject.SetActive(false);
                _pipe.GetComponent<Pipe>().up[q].gameObject.SetActive(variants[i][q] != '1');
                _pipe.GetComponent<Pipe>().obstacles[q].gameObject.SetActive(false);
                _pipe.name = "Section";
            }
        }   
    }
}