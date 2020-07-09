using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class Garden : MonoBehaviour, IPointerClickHandler
{
    public Sprite[] Sprites = new Sprite[3];
    public GameObject WareHouse;
    [SerializeField] private float gardenWasOffline;
    private static GameObject lastGameObject;
    [SerializeField] private int state = 0;

    void Start()
    {
        if (PlayerPrefs.HasKey(gameObject.name + "LastState"))
        {
            state = PlayerPrefs.GetInt(gameObject.name + "LastState");

            switch (state)
            {
                case 0:
                    gameObject.GetComponent<SpriteRenderer>().sprite = Sprites[0];
                    gardenWasOffline = 0;
                    break;
                case 1:
                    Debug.Log("keking");
                    gameObject.GetComponent<SpriteRenderer>().sprite = Sprites[1];
                    TimeSpan ts = DateTime.Now - DateTime.Parse(PlayerPrefs.GetString(gameObject.name + "LastSession"));
                    gardenWasOffline = ts.Days * 1440 + ts.Hours * 60 + ts.Minutes + (float)ts.Seconds / 60;
                    gardenWasOffline *= 100;
                    break;
                case 2:
                    gameObject.GetComponent<SpriteRenderer>().sprite = Sprites[2];
                    gardenWasOffline = 0;
                    break;
            }
        }
        else
        {
            state = 0;
            gardenWasOffline = 0;
            gameObject.GetComponent<SpriteRenderer>().sprite = Sprites[0];
        }
    }

    void FixedUpdate()
    {
        gardenWasOffline += Time.fixedDeltaTime;
        if (gardenWasOffline >= 100)
        {
            switch (state)
            {
                case 0:
                    gardenWasOffline = 0;
                    break;
                case 1:
                    Debug.Log("keking");
                    state = 2;
                    gameObject.GetComponent<SpriteRenderer>().sprite = Sprites[2];
                    gardenWasOffline = 0;
                    break;
                case 2:
                    gardenWasOffline = 0;
                    break;
            }
        }
    }

    void OnDisable()
    {
        PlayerPrefs.SetInt(gameObject.name + "LastState", state);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        lastGameObject = gameObject;
        Debug.Log("Was initiated: " + gameObject.name);
        switch (state)
        {
            case 0:
                if (WareHouse.GetComponent<WareHouse>().GetSeeds())
                {
                    state = 1;
                    gameObject.GetComponent<SpriteRenderer>().sprite = Sprites[1];
                    gardenWasOffline = 0;
                    PlayerPrefs.SetString(gameObject.name + "LastSession", DateTime.Now.ToString());
                    Debug.Log("Was added to: " + lastGameObject.name);
                }
                break;
            case 1:
                //growing
                break;
            case 2:
                int whFreeSpace = PlayerPrefs.GetInt("WareHouseFreeSpace");
                if (whFreeSpace >= 7)
                {
                    WareHouse.GetComponent<WareHouse>().PutSeeds(2);
                    WareHouse.GetComponent<WareHouse>().PutCarrot(5);
                    state = 0;
                    gameObject.GetComponent<SpriteRenderer>().sprite = Sprites[0];
                }
                break;
        }
    }
}
