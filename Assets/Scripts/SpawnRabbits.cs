using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRabbits : MonoBehaviour
{
    private float[] StartCoords = new float [] {1.6f, 0.5f, -1.2f, 4f, 0.5f, 2.5f};
    public GameObject rabbit;
    private int erand;
    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn() {
        while (true) {
            erand = Random.Range(0, 3);
            Instantiate(rabbit, new Vector3(StartCoords[erand], StartCoords[erand+3], 0), Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
