using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeCreator : MonoBehaviour
{
    public List<string> variants = new() {
        "0001",
        "0011",
        "0110",
        "0101",
        "0010",
        "0111",
        "1111"
    };

    public GameObject prefab;
    private void Start() => StartCoroutine(StartSpawn());

    private IEnumerator StartSpawn()
    {
        var i = 0;
        while (i < variants.Count)
        {
            StartCoroutine(SpawnPipe1(i));
            StartCoroutine(SpawnPipe2(i));
            StartCoroutine(SpawnPipe3(i));
            StartCoroutine(SpawnPipe4(i));
            StartCoroutine(SpawnPipe5(i));
            yield return new WaitForSeconds(.125f);
            i++;
            print(i);
        }
    }

    private IEnumerator SpawnPipe1(int i)
    {
        var _pipe = Instantiate(prefab, new Vector3(60 * i, 0, 0), Quaternion.identity);
        for (int q = 0; q < 4; q++)
        {
            _pipe.GetComponent<Pipe>().down[q].SetActive(variants[i][q] == '1');
            _pipe.GetComponent<Pipe>().up[q].SetActive(false);
            _pipe.GetComponent<Pipe>().obstacle[q].SetActive(false);

            _pipe.GetComponent<Pipe>().down[q + 4].SetActive(variants[i][q] == '1');
            _pipe.GetComponent<Pipe>().up[q + 4].SetActive(false);
            _pipe.GetComponent<Pipe>().obstacle[q + 4].SetActive(false);
            yield return new WaitForSeconds(.125f);
        }
        _pipe.name = $"Section_{variants[i]}";
        Destroy(_pipe.GetComponent<Pipe>());
    }

    private IEnumerator SpawnPipe2(int i)
    {
        var _pipe = Instantiate(prefab, new Vector3(60 * i, 60, 0), Quaternion.identity);
        for (int q = 0; q < 4; q++)
        {
            _pipe.GetComponent<Pipe>().down[q].SetActive(variants[i][q] == '1');
            _pipe.GetComponent<Pipe>().up[q].SetActive(variants[i][q] != '1');
            _pipe.GetComponent<Pipe>().obstacle[q].SetActive(false);

            _pipe.GetComponent<Pipe>().down[q + 4].SetActive(variants[i][q] == '1');
            _pipe.GetComponent<Pipe>().up[q + 4].SetActive(variants[i][q] != '1');
            _pipe.GetComponent<Pipe>().obstacle[q + 4].SetActive(false);
            yield return new WaitForSeconds(.125f);
        }
        _pipe.name = $"Section_{variants[i]}";
        Destroy(_pipe.GetComponent<Pipe>());
    }

    private IEnumerator SpawnPipe3(int i)
    {
        var _pipe = Instantiate(prefab, new Vector3(60 * i, 120, 0), Quaternion.identity);
        for (int q = 0; q < 4; q++)
        {
            _pipe.GetComponent<Pipe>().down[q].SetActive(variants[i][q] == '1');
            _pipe.GetComponent<Pipe>().up[q].SetActive(false);
            _pipe.GetComponent<Pipe>().obstacle[q].SetActive(variants[i][q] != '1');

            _pipe.GetComponent<Pipe>().down[q + 4].SetActive(variants[i][q] == '1');
            _pipe.GetComponent<Pipe>().up[q + 4].SetActive(false);
            _pipe.GetComponent<Pipe>().obstacle[q + 4].SetActive(variants[i][q] != '1');
            yield return new WaitForSeconds(.125f);
        }
        _pipe.name = $"Section_{variants[i]}";
        Destroy(_pipe.GetComponent<Pipe>());
    }

    private IEnumerator SpawnPipe4(int i)
    {
        var _pipe = Instantiate(prefab, new Vector3(60 * i, 180, 0), Quaternion.identity);
        for (int q = 0; q < 4; q++)
        {
            _pipe.GetComponent<Pipe>().down[q].SetActive(false);
            _pipe.GetComponent<Pipe>().up[q].SetActive(variants[i][q] == '1');
            _pipe.GetComponent<Pipe>().obstacle[q].SetActive(variants[i][q] != '1');

            _pipe.GetComponent<Pipe>().down[q + 4].SetActive(false);
            _pipe.GetComponent<Pipe>().up[q + 4].SetActive(variants[i][q] == '1');
            _pipe.GetComponent<Pipe>().obstacle[q + 4].SetActive(variants[i][q] != '1');
            yield return new WaitForSeconds(.125f);
        }
        _pipe.name = $"Section_{variants[i]}";
        Destroy(_pipe.GetComponent<Pipe>());
    }

    private IEnumerator SpawnPipe5(int i)
    {
        var _pipe = Instantiate(prefab, new Vector3(60 * i, 240, 0), Quaternion.identity);
        for (int q = 0; q < 4; q++)
        {
            _pipe.GetComponent<Pipe>().down[q].SetActive(false);
            _pipe.GetComponent<Pipe>().up[q].SetActive(variants[i][q] == '1');
            _pipe.GetComponent<Pipe>().obstacle[q].SetActive(false);

            _pipe.GetComponent<Pipe>().down[q + 4].SetActive(false);
            _pipe.GetComponent<Pipe>().up[q + 4].SetActive(variants[i][q] == '1');
            _pipe.GetComponent<Pipe>().obstacle[q + 4].SetActive(false);
            yield return new WaitForSeconds(.125f);
        }
        _pipe.name = $"Section_{variants[i]}";
        Destroy(_pipe.GetComponent<Pipe>());
    }

}