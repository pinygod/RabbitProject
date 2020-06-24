using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WareHouse : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private int foodCount, fluffCount, whLevel, whFoodCapacity, whFluffCapacity;
    [Header("Text fields")]
    public Text FoodText, FluffText, PlayerMoney;
    [Header("Warehouse menu Panel")]
    public GameObject whPanel;

    public void OnPointerClick(PointerEventData eventData)
    {
        Time.timeScale = 0f;
        GetValues();

        FoodText.text = foodCount.ToString() + " / " + whFoodCapacity.ToString();
        FluffText.text = fluffCount.ToString() + " / " + whFluffCapacity.ToString();

        whPanel.SetActive(true);

    }

    private void GetValues()
    {
        Debug.Log("Getting values...");
        whLevel = PlayerPrefs.GetInt("WareHouseLevel");
        whFoodCapacity = PlayerPrefs.GetInt("WareHouseFoodCapacity");
        whFluffCapacity = PlayerPrefs.GetInt("WareHouseFluffCapacity");
        foodCount = PlayerPrefs.GetInt("WareHouseFood");
        fluffCount = PlayerPrefs.GetInt("WareHouseFluff");
    }

    private void UpdateValues()
    {
        Debug.Log("Updating values...");
        PlayerPrefs.SetInt("WareHouseFood", foodCount);
        PlayerPrefs.SetInt("WareHouseFluff", fluffCount);
        PlayerPrefs.SetInt("WareHouseLevel", whLevel);
        PlayerPrefs.SetInt("WareHouseFoodCapacity", whFoodCapacity);
        PlayerPrefs.SetInt("WareHouseFluffCapacity", whFluffCapacity);
        FoodText.text = foodCount.ToString() + " / " + whFoodCapacity.ToString();
        FluffText.text = fluffCount.ToString() + " / " + whFluffCapacity.ToString();
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
            UpdateValues();
            PlayerPrefs.SetInt("score", pMoney);
            PlayerMoney.text = pMoney.ToString();
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
            UpdateValues();
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
            UpdateValues();
            PlayerPrefs.SetInt("score", pMoney);
            PlayerMoney.text = pMoney.ToString();
        }
        else
        {
            //TODO: Not enough money msg
        }
    }
    #endregion


    #region Interactions: cage with warehouse items 
    public int PutFluff(int newFluff)
    {
        GetValues();
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

    public bool GetFood()
    {
        GetValues();
        if (foodCount > 0)
        {
            PlayerPrefs.SetInt("WareHouseFood", --foodCount);
            return true;
        }
        else return false;
    }
    #endregion
}
