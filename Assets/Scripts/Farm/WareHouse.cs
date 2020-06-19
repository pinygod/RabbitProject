using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WareHouse : MonoBehaviour, IPointerClickHandler
{
    private int foodCount, fluffCount, whLevel, whFoodCapacity, whFluffCapacity;
    [Header("Text fields")]
    public Text foodText, fluffText, playerMoney;
    [Header("Warehouse menu Panel")]
    public GameObject whPanel;

    public void OnPointerClick(PointerEventData eventData)
    {
        Time.timeScale = 0f;
        getValues();

        foodText.text = foodCount.ToString() + " / " + whFoodCapacity.ToString();
        fluffText.text = fluffCount.ToString() + " / " + whFluffCapacity.ToString();

        whPanel.SetActive(true);

    }

    private void getValues()
    {
        Debug.Log("Getting values...");
        if (PlayerPrefs.HasKey("WareHouseLevel"))
        {
            whLevel = PlayerPrefs.GetInt("WareHouseLevel");
            whFoodCapacity = PlayerPrefs.GetInt("WareHouseFoodCapacity");
            whFluffCapacity = PlayerPrefs.GetInt("WareHouseFluffCapacity");
        }
        else
        {
            whLevel = 1;
            PlayerPrefs.SetInt("WareHouseLevel", 1);
            PlayerPrefs.SetInt("WareHouseFoodCapacity", whLevel * 30);
            PlayerPrefs.SetInt("WareHouseFluffCapacity", whLevel * 60);
        }
        if (PlayerPrefs.HasKey("WareHouseFood"))
        {
            foodCount = PlayerPrefs.GetInt("WareHouseFood");
        }
        else
        {
            foodCount = 0;
            PlayerPrefs.SetInt("WareHouseFood", 0);
        }
        if (PlayerPrefs.HasKey("WareHouseFluff"))
        {
            fluffCount = PlayerPrefs.GetInt("WareHouseFluff");
        }
        else
        {
            fluffCount = 0;
            PlayerPrefs.SetInt("WareHouseFluff", 0);
        }
    }

    private void updateValues()
    {
        Debug.Log("Updating values...");
        PlayerPrefs.SetInt("WareHouseFood", foodCount);
        PlayerPrefs.SetInt("WareHouseFluff", fluffCount);
        PlayerPrefs.SetInt("WareHouseLevel", whLevel);
        PlayerPrefs.SetInt("WareHouseFoodCapacity", whFoodCapacity);
        PlayerPrefs.SetInt("WareHouseFluffCapacity", whFluffCapacity);
        foodText.text = foodCount.ToString() + " / " + whFoodCapacity.ToString();
        fluffText.text = fluffCount.ToString() + " / " + whFluffCapacity.ToString();
    }

    #region Click events (buttons) inside warehouse
    public void OnCloseClick()
    {
        whPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnClickSell()
    {
        if (fluffCount > 0)
        {
            int pMoney = PlayerPrefs.GetInt("score") + Random.Range(fluffCount - fluffCount / 4, fluffCount + fluffCount / 4);
            fluffCount = 0;
            updateValues();
            PlayerPrefs.SetInt("score", pMoney);
            playerMoney.text = pMoney.ToString();
        }
        else
        {
            //TODO: Not enough fluff msg
        }
    }

    public void OnClickTrade()
    {
        if (fluffCount > 0)
        {
            int maxFoodCapacity = whFoodCapacity - foodCount;

            if (fluffCount > maxFoodCapacity)
            {
                int traded = Random.Range(maxFoodCapacity, maxFoodCapacity + maxFoodCapacity / 5);
                foodCount += Mathf.Min(maxFoodCapacity, traded);
                fluffCount -= maxFoodCapacity - (traded - maxFoodCapacity);
            }
            else
            {
                int traded = Random.Range(fluffCount, fluffCount + fluffCount / 5);
                foodCount += Mathf.Min(maxFoodCapacity, traded);
                fluffCount = 0;
            }
            updateValues();
        }
        else
        {
            //TODO: Not enough fluff msg
        }
    }

    public void OnClickUpgrade()
    {
        int pMoney = PlayerPrefs.GetInt("score");
        if (pMoney >= whLevel * 1000)
        {
            pMoney -= whLevel * 1000;
            whLevel++;
            whFoodCapacity = whLevel * 30;
            whFluffCapacity = whLevel * 60;
            updateValues();
            PlayerPrefs.SetInt("score", pMoney);
            playerMoney.text = pMoney.ToString();
        }
        else
        {
            //TODO: Not enough money msg
        }
    }
    #endregion


    #region Interactions: cage with warehouse items 
    public int putFluff(int newFluff)
    {
        getValues();
        int freeSpace = whFluffCapacity - fluffCount, leftover;
        if (freeSpace >= newFluff)
        {
            PlayerPrefs.SetInt("WareHouseFluff", fluffCount + newFluff);
            leftover = 0;
        }
        else
        {
            PlayerPrefs.SetInt("WareHouseFluff", fluffCount + freeSpace);
            leftover = newFluff - freeSpace;
        }

        return leftover;
    }

    public bool getFood()
    {
        getValues();
        if (foodCount > 0)
        {
            PlayerPrefs.SetInt("WareHouseFood", --foodCount);
            return true;
        }
        else return false;
    }
    #endregion
}
