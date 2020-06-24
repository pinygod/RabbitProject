using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBunny : MonoBehaviour
{

    public GameObject[] hats;
    public GameObject bunny;
    private GameObject hat;

    void Start()
    {
        hat = hats[Random.Range(0, hats.Length)];
        Instantiate(bunny, hat.transform.position, Quaternion.identity);
    }

    void Update()
    {
        
    }
}
