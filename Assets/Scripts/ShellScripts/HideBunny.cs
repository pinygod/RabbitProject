using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HideBunny : MonoBehaviour
{
    public GameObject[] Hats;
    public GameObject Rabbit;
    private GameObject StartHat;

    private Vector3 hatStartPosition;
    private Vector3 hatTargetPosition;
    private Vector3 hat1Pos, hat2Pos, hat3Pos;
    private float time = 0;
    private bool IsMovingHatUp = false;


    void Start()
    {
        hat1Pos = Hats[0].transform.position;
        hat2Pos = Hats[1].transform.position;
        hat3Pos = Hats[2].transform.position;
    }

    void Update()
    {
        if (IsMovingHatUp == true)
        {
            time += Time.deltaTime;
            StartHat.transform.position = Vector2.Lerp(hatStartPosition, hatTargetPosition, Easing(time));

            if (StartHat.transform.position == hatStartPosition)
            {
                Destroy(GameObject.Find("RabbitShellGame(Clone)"));
                Hats[0].GetComponent<HatsMixer>().MixHats();
                Hats[1].GetComponent<HatsMixer>().MixHats();
                Hats[2].GetComponent<HatsMixer>().MixHats();

                IsMovingHatUp = false;
            }
        }
    }

    private float Easing(float x)
    {
        return x < 0.5 ? x * x * 2 : (1 - (1 - x) * (1 - x) * 2);
    }

    public GameObject newBunny()
    {
        StartHat = Hats[Random.Range(0, Hats.Length)];
        Instantiate(Rabbit, StartHat.transform.position, Quaternion.identity);
        return StartHat;
    }

    public void startButton()
    {
        time = 0;
        Hats[0].transform.position = hat1Pos;
        Hats[1].transform.position = hat2Pos;
        Hats[2].transform.position = hat3Pos;
        Destroy(GameObject.Find("RabbitShellGame(Clone)"));

        StartHat = Hats[Random.Range(0, Hats.Length)];
        hatStartPosition = StartHat.transform.position;
        hatTargetPosition = hatStartPosition;
        hatTargetPosition.y += 1.5f;

        Instantiate(Rabbit, hatStartPosition, Quaternion.identity);
        IsMovingHatUp = true;
    }

}
