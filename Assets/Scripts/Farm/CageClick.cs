using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class CageClick : MonoBehaviour, IPointerClickHandler
{

    public GameObject CagePanel, WareHouse;
    public Text RabbitsInCageText, FoodInCageText, FluffInCageText, PlayerMoney;
    private static float cageWasOffline;
    private static int lastFood, lastFluff, rabbitsInCage, newFluff, cageLevel, cageFoodCapacity, cageFluffCapacity, cageRabbitsCapacity;
    private static GameObject lastGameObject;

    public void OnPointerClick(PointerEventData eventData)
    {
        lastGameObject = gameObject;
        Debug.Log("Was initiated: " + gameObject.name);
        Time.timeScale = 0f;
        getValues();
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

        FoodInCageText.text = lastFood.ToString() + "/" + cageFoodCapacity;
        FluffInCageText.text = lastFluff.ToString() + "/" + cageFluffCapacity;
        RabbitsInCageText.text = rabbitsInCage.ToString() + "/" + cageRabbitsCapacity;
        CagePanel.SetActive(true);

    }

    private void getValues()
    {
        cageLevel = PlayerPrefs.GetInt(gameObject.name + "Level");
        lastFood = PlayerPrefs.GetInt(gameObject.name + "Food");
        lastFluff = PlayerPrefs.GetInt(gameObject.name + "Fluff");
        rabbitsInCage = PlayerPrefs.GetInt(gameObject.name + "Rabbits");
        cageRabbitsCapacity = PlayerPrefs.GetInt(gameObject.name + "RabbitsCapacity");
        cageFoodCapacity = PlayerPrefs.GetInt(gameObject.name + "FoodCapacity");
        cageFluffCapacity = PlayerPrefs.GetInt(gameObject.name + "FluffCapacity");
    }

    public void OnCloseClick()
    {
        PlayerPrefs.SetInt(lastGameObject.name + "Rabbits", rabbitsInCage);
        PlayerPrefs.SetInt(lastGameObject.name + "Fluff", lastFluff);
        PlayerPrefs.SetInt(lastGameObject.name + "Food", lastFood);
        CagePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnAddClick()
    {
        if (lastFood < cageFoodCapacity)
        {
            if (WareHouse.GetComponent<WareHouse>().GetHay())
            {
                FoodInCageText.text = (++lastFood).ToString() + "/" + cageFoodCapacity;
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
            lastFluff = WareHouse.GetComponent<WareHouse>().PutFluff(lastFluff);
            FluffInCageText.text = lastFluff.ToString() + "/" + cageFluffCapacity;
        }
    }

    public void OnClickUpgrade()
    {
        int pMoney = PlayerPrefs.GetInt("score");
        Debug.Log(pMoney);
        if (pMoney >= cageLevel * 1000)
        {
            pMoney -= cageLevel * 1000;
            Debug.Log(pMoney);
            cageLevel++;
            cageFoodCapacity = cageLevel * 30;
            cageFluffCapacity = cageLevel * 50;
            cageRabbitsCapacity = cageLevel * 5;
            PlayerPrefs.SetInt(lastGameObject.name + "Level", cageLevel);
            PlayerPrefs.SetInt(lastGameObject.name + "FoodCapacity", cageFoodCapacity);
            PlayerPrefs.SetInt(lastGameObject.name + "FluffCapacity", cageFluffCapacity);
            PlayerPrefs.SetInt(lastGameObject.name + "RabbitsCapacity", cageRabbitsCapacity);
            FluffInCageText.text = lastFluff.ToString() + "/" + cageFluffCapacity;
            FoodInCageText.text = lastFood.ToString() + "/" + cageFoodCapacity;
            RabbitsInCageText.text = rabbitsInCage.ToString() + "/" + cageRabbitsCapacity;
            PlayerPrefs.SetInt("score", pMoney);
            PlayerMoney.text = pMoney.ToString();
        }
        else
        {
            //TODO: Not enough money msg
        }
    }
}
