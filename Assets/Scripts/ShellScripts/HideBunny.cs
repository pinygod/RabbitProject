using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HideBunny : MonoBehaviour
{
    public GameObject[] hats;
    public GameObject bunny;
    public GameObject hat;
    public GameObject newHat;

    [SerializeField] private float huy;

    private Vector2 hatStartPosition;
    private Vector2 hatTargetPosition;
    private float time = 0;
    
    [SerializeField] private int ctr = 0;

    float Easing(float x)
    {
        ctr++;
        huy = x < 0.5 ? x * x * 2 : (1 - (1 - x) * (1 - x) * 2);
        return x < 0.5 ? x * x * 2 : (1 - (1 - x) * (1 - x) * 2);
    }

    [SerializeField] private Vector3 hat1pos, hat2pos, hat3pos;

    void Start()
    {
        hat1pos = hats[0].transform.position;
        hat2pos = hats[1].transform.position;
        hat3pos = hats[2].transform.position;
    }

    void Update()
    {
        if (glagster == true) {

        if ((ctr > 2) && (huy <= 0))
        {
            hats[0].GetComponent<HatsMixer>().kek();
            hats[1].GetComponent<HatsMixer>().kek();
            hats[2].GetComponent<HatsMixer>().kek();

            GameObject zemletrus = GameObject.Find("Bunny(Clone)");
            Destroy(zemletrus);
            //enabled = false;
            glagster = false;
        }
        else
        {
            hat.transform.position = Vector2.Lerp(hatStartPosition, hatTargetPosition, Easing(time));
            time += Time.deltaTime;
        }

        }
    }

    public void newBunny()
    {
        newHat = hats[Random.Range(0, hats.Length)];
        Instantiate(bunny, newHat.transform.position, Quaternion.identity);
    }

    //public GameObject Hat;

    public HatsMixer HatsMixer;

    bool glagster = false;

    public void startButton () {
        //hats[0].transform.position = hat1pos;
        //hats[1].transform.position = hat2pos;
        //hats[2].transform.position = hat3pos;

        hats[0].GetComponent<HatsMixer>().clear();
        hats[1].GetComponent<HatsMixer>().clear();
        hats[2].GetComponent<HatsMixer>().clear();

        ctr = 0;

        HatsMixer.flagster = false;
        glagster = true;

        hat = hats[Random.Range(0, hats.Length)];

        hatStartPosition = hat.transform.position;
        hatTargetPosition = hatStartPosition;
        hatTargetPosition.y += 1.5f;

        Instantiate(bunny, hatStartPosition, Quaternion.identity);
    }
  
}
