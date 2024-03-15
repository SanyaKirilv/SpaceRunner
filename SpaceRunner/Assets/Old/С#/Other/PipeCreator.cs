using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeCreator : MonoBehaviour
{
    public List<string> variants = new() {
        "0000",
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
            StartCoroutine(SpawnPipe6(i));
            StartCoroutine(SpawnPipe7(i));
            StartCoroutine(SpawnPipe8(i));
            yield return new WaitForSeconds(.125f);
            i++;
            print(i);
        }
    }

    private IEnumerator SpawnPipe1(int i)
    {
        var _pipe = Instantiate(prefab, new Vector3(55 * i, 0, 0), Quaternion.identity);
        for (int q = 0; q < 4; q++)
        {
            _pipe.GetComponent<Pipe>().down[q].SetActive(variants[i][q] == '1');
            _pipe.GetComponent<Pipe>().up[q].SetActive(false);

            _pipe.GetComponent<Pipe>().down[q+4].SetActive(false);
            _pipe.GetComponent<Pipe>().up[q+4].SetActive(false);
            yield return new WaitForSeconds(.125f);
        }
        _pipe.name = $"Section_{variants[i]}";
        Destroy(_pipe.GetComponent<Pipe>());
    }
    private IEnumerator SpawnPipe2(int i)
    {
        var _pipe = Instantiate(prefab, new Vector3(55 * i, 55, 0), Quaternion.identity);
        for (int q = 0; q < 4; q++)
        {
            _pipe.GetComponent<Pipe>().down[q].SetActive(variants[i][q] == '1');
            _pipe.GetComponent<Pipe>().up[q].SetActive(variants[i][q] != '1');

            _pipe.GetComponent<Pipe>().down[q+4].SetActive(false);
            _pipe.GetComponent<Pipe>().up[q+4].SetActive(false);
            yield return new WaitForSeconds(.125f);
        }
        _pipe.name = $"Section_{variants[i]}";
        Destroy(_pipe.GetComponent<Pipe>());
    }
    private IEnumerator SpawnPipe3(int i)
    {
        var _pipe = Instantiate(prefab, new Vector3(55 * i, 110, 0), Quaternion.identity);
        for (int q = 0; q < 4; q++)
        {
            _pipe.GetComponent<Pipe>().down[q].SetActive(variants[i][q] == '1');
            _pipe.GetComponent<Pipe>().up[q].SetActive(false);

            _pipe.GetComponent<Pipe>().down[q+4].SetActive(variants[i][q] == '1');
            _pipe.GetComponent<Pipe>().up[q+4].SetActive(false);
            yield return new WaitForSeconds(.125f);
        }
        _pipe.name = $"Section_{variants[i]}";
        Destroy(_pipe.GetComponent<Pipe>());
    }
    private IEnumerator SpawnPipe4(int i)
    {
        var _pipe = Instantiate(prefab, new Vector3(55 * i, 165, 0), Quaternion.identity);
        for (int q = 0; q < 4; q++)
        {
            _pipe.GetComponent<Pipe>().down[q].SetActive(variants[i][q] != '1');
            _pipe.GetComponent<Pipe>().up[q].SetActive(variants[i][q] == '1');

            _pipe.GetComponent<Pipe>().down[q+4].SetActive(variants[i][q] != '1');
            _pipe.GetComponent<Pipe>().up[q+4].SetActive(variants[i][q] == '1');
            yield return new WaitForSeconds(.125f);
        }
        _pipe.name = $"Section_{variants[i]}";
        Destroy(_pipe.GetComponent<Pipe>());
    }
    private IEnumerator SpawnPipe5(int i)
    {
        var _pipe = Instantiate(prefab, new Vector3(55 * i, 220, 0), Quaternion.identity);
        for (int q = 0; q < 4; q++)
        {
            _pipe.GetComponent<Pipe>().down[q].SetActive(variants[i][q] == '1');
            _pipe.GetComponent<Pipe>().up[q].SetActive(variants[i][q] != '1');

            _pipe.GetComponent<Pipe>().down[q+4].SetActive(variants[i][q] == '1');
            _pipe.GetComponent<Pipe>().up[q+4].SetActive(variants[i][q] != '1');
            yield return new WaitForSeconds(.125f);
        }
        _pipe.name = $"Section_{variants[i]}";
        Destroy(_pipe.GetComponent<Pipe>());
    }
    private IEnumerator SpawnPipe6(int i)
    {
        var _pipe = Instantiate(prefab, new Vector3(55 * i, 275, 0), Quaternion.identity);
        for (int q = 0; q < 4; q++)
        {
            _pipe.GetComponent<Pipe>().down[q].SetActive(false);
            _pipe.GetComponent<Pipe>().up[q].SetActive(variants[i][q] == '1');

            _pipe.GetComponent<Pipe>().down[q+4].SetActive(false);
            _pipe.GetComponent<Pipe>().up[q+4].SetActive(false);
            yield return new WaitForSeconds(.125f);
        }
        _pipe.name = $"Section_{variants[i]}";
        Destroy(_pipe.GetComponent<Pipe>());
    }
    private IEnumerator SpawnPipe7(int i)
    {
        var _pipe = Instantiate(prefab, new Vector3(55 * i, 330, 0), Quaternion.identity);
        for (int q = 0; q < 4; q++)
        {
            _pipe.GetComponent<Pipe>().down[q].SetActive(false);
            _pipe.GetComponent<Pipe>().up[q].SetActive(variants[i][q] == '1');

            _pipe.GetComponent<Pipe>().down[q+4].SetActive(false);
            _pipe.GetComponent<Pipe>().up[q+4].SetActive(variants[i][q] == '1');
            yield return new WaitForSeconds(.125f);
        }
        _pipe.name = $"Section_{variants[i]}";
        Destroy(_pipe.GetComponent<Pipe>());
    }
    private IEnumerator SpawnPipe8(int i)
    {
        var _pipe = Instantiate(prefab, new Vector3(55 * i, 385, 0), Quaternion.identity);
        for (int q = 0; q < 4; q++)
        {
            _pipe.GetComponent<Pipe>().down[q].SetActive(variants[i][q] != '1');
            _pipe.GetComponent<Pipe>().up[q].SetActive(variants[i][q] == '1');

            _pipe.GetComponent<Pipe>().down[q+4].SetActive(false);
            _pipe.GetComponent<Pipe>().up[q+4].SetActive(variants[i][q] == '1');
            yield return new WaitForSeconds(.125f);
        }
        _pipe.name = $"Section_{variants[i]}";
        Destroy(_pipe.GetComponent<Pipe>());
    }
}