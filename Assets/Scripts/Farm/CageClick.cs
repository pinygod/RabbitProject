using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class CageClick : MonoBehaviour, IPointerClickHandler
{

    public GameObject cagePanel, wareHouse;
    public Text rabbitsInCageText, foodInCageText, MoneyUI, FoodUI, fluffInCageText;
    private static float cageWasOffline, lastFood, lastFluff, rabbitsInCage, newFluff, cageLevel;
    private static GameObject lastGameObject;

    public void OnPointerClick(PointerEventData eventData)
    {
        lastGameObject = gameObject;
        Debug.Log("Was initiated: " + gameObject.name);
        Time.timeScale = 0f;

        if (PlayerPrefs.HasKey(gameObject.name + "Level"))
        {
            cageLevel = PlayerPrefs.GetInt(gameObject.name + "Level");
        }
        else
        {
            PlayerPrefs.SetInt(gameObject.name + "Level", 1);
            cageLevel = 1;
        }

        if (PlayerPrefs.HasKey(gameObject.name + "LastFood"))
        {
            lastFood = PlayerPrefs.GetFloat(gameObject.name + "LastFood");
        }
        else
        {
            lastFood = 0;
        }


        if (PlayerPrefs.HasKey(gameObject.name + "LastFluff"))
        {
            lastFluff = PlayerPrefs.GetFloat(gameObject.name + "LastFluff");

        }
        else
        {
            lastFluff = 0;
        }


        if (PlayerPrefs.HasKey(gameObject.name + "LastRabbits"))
        {
            rabbitsInCage = PlayerPrefs.GetFloat(gameObject.name + "LastRabbits");

        }
        else
        {
            rabbitsInCage = 0;
        }

        if (PlayerPrefs.HasKey(gameObject.name + "LastSession"))
        {
            TimeSpan ts = DateTime.Now - DateTime.Parse(PlayerPrefs.GetString(gameObject.name + "LastSession"));
            cageWasOffline = ts.Days * 1440 + ts.Hours * 60 + ts.Minutes + (float)ts.Seconds / 60;
            Debug.Log(cageWasOffline.ToString());
            if (cageWasOffline >= 1)
            {
                PlayerPrefs.SetString(lastGameObject.name + "LastSession", DateTime.Now.ToString());
                if (rabbitsInCage > 0)
                {
                    if (lastFood < (int)cageWasOffline * rabbitsInCage)
                    {
                        newFluff = lastFood;
                        lastFood -= lastFood;
                    }
                    else
                    {
                        newFluff = (int)cageWasOffline * rabbitsInCage;
                        lastFood -= newFluff;
                    }
                    lastFluff = Math.Min(lastFluff + newFluff, cageLevel * 50);
                }
            }
        }
        else
        {
            cageWasOffline = 0;
            PlayerPrefs.SetString(lastGameObject.name + "LastSession", DateTime.Now.ToString());
        }

        foodInCageText.text = ((int)(lastFood)).ToString() + "/" + cageLevel * 30;
        fluffInCageText.text = ((int)(lastFluff)).ToString() + "/" + cageLevel * 50;
        rabbitsInCageText.text = ((int)(rabbitsInCage)).ToString() + "/" + cageLevel * 5;
        cagePanel.SetActive(true);

    }

    public void OnCloseClick()
    {
        PlayerPrefs.SetFloat(lastGameObject.name + "LastRabbits", rabbitsInCage);
        PlayerPrefs.SetFloat(lastGameObject.name + "LastFluff", lastFluff);
        PlayerPrefs.SetFloat(lastGameObject.name + "LastFood", lastFood);
        cagePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnAddClick()
    {
        if (lastFood < cageLevel * 30)
        {
            if (wareHouse.GetComponent<WareHouse>().getFood())
            {
                lastGameObject.GetComponent<CageClick>().foodInCageText.text = (++lastFood).ToString() + "/" + cageLevel * 30;
                Debug.Log("Was added to: " + lastGameObject.name);
            }
        }
    }

    public void OnClickDelete()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Deleted");
    }

    public void OnClickCollect()
    {
        if (lastFluff > 0)
        {
            lastFluff = wareHouse.GetComponent<WareHouse>().putFluff((int)lastFluff);
            lastGameObject.GetComponent<CageClick>().fluffInCageText.text = lastFluff.ToString() + "/" + cageLevel * 50;
        }
    }
}
