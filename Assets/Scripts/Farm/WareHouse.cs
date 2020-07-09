using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WareHouse : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private int hayCount, fluffCount, seedsCount, carrotCount, whLevel, whCapacity, whFreeSpace;
    [Header("Text fields")]
    public Text FoodText, FluffText, SeedsText, CarrotText, PlayerMoney, CapacityText;
    [Header("Warehouse menu Panel")]
    public GameObject whPanel;

    public void OnPointerClick(PointerEventData eventData)
    {
        Time.timeScale = 0f;
        GetValues();
        CapacityText.text = (whCapacity - whFreeSpace).ToString() + " / " + whCapacity.ToString();
        FoodText.text = hayCount.ToString();
        FluffText.text = fluffCount.ToString();
        SeedsText.text = seedsCount.ToString();
        CarrotText.text = carrotCount.ToString();

        whPanel.SetActive(true);

    }

    private void GetValues()
    {
        Debug.Log("Getting values...");
        whLevel = PlayerPrefs.GetInt("WareHouseLevel");
        whCapacity = PlayerPrefs.GetInt("WareHouseCapacity");
        whFreeSpace = PlayerPrefs.GetInt("WareHouseFreeSpace");
        hayCount = PlayerPrefs.GetInt("WareHouseHay");
        fluffCount = PlayerPrefs.GetInt("WareHouseFluff");
        seedsCount = PlayerPrefs.GetInt("WareHouseSeeds");
        carrotCount = PlayerPrefs.GetInt("WareHouseCarrot");
    }

    private void UpdateValues()
    {
        Debug.Log("Updating values...");
        PlayerPrefs.SetInt("WareHouseHay", hayCount);
        PlayerPrefs.SetInt("WareHouseFluff", fluffCount);
        PlayerPrefs.SetInt("WareHouseLevel", whLevel);
        PlayerPrefs.SetInt("WareHouseCapacity", whCapacity);
        PlayerPrefs.SetInt("WareHouseFreeSpace", whFreeSpace);
        FoodText.text = hayCount.ToString();
        FluffText.text = fluffCount.ToString();
        SeedsText.text = seedsCount.ToString();
        CarrotText.text = carrotCount.ToString();
        CapacityText.text = whFreeSpace.ToString() + " / " + whCapacity.ToString();
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
            whFreeSpace += fluffCount;
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
            int maxFoodCapacity = Mathf.Max(whFreeSpace, fluffCount);

            if (fluffCount > maxFoodCapacity)
            {
                int traded = Random.Range(maxFoodCapacity, maxFoodCapacity + maxFoodCapacity / 5);
                int hayDiff = Mathf.Min(maxFoodCapacity, traded);
                hayCount += hayDiff;
                int fluffDiff = maxFoodCapacity - (traded - maxFoodCapacity);
                whFreeSpace = whFreeSpace - hayDiff + fluffDiff;
                fluffCount -= fluffDiff;
            }
            else
            {
                int traded = Random.Range(fluffCount, fluffCount + fluffCount / 5);
                int hayDiff = Mathf.Min(maxFoodCapacity, traded);
                hayCount += hayDiff;
                whFreeSpace = whFreeSpace - hayDiff + fluffCount;
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
            whCapacity = whLevel * 100;
            whFreeSpace += 100;
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
        int leftover;
        if (whFreeSpace >= newFluff)
        {
            PlayerPrefs.SetInt("WareHouseFluff", fluffCount + newFluff);
            leftover = 0;
            whFreeSpace -= newFluff;
        }
        else
        {
            PlayerPrefs.SetInt("WareHouseFluff", fluffCount + whFreeSpace);
            leftover = newFluff - whFreeSpace;
            whFreeSpace = 0;
        }
        PlayerPrefs.SetInt("WareHouseFreeSpace", whFreeSpace);

        return leftover;
    }

    public int PutSeeds(int seeds)
    {
        GetValues();
        int leftover;
        if (whFreeSpace >= seeds)
        {
            PlayerPrefs.SetInt("WareHouseSeeds", seedsCount + seeds);
            leftover = 0;
            whFreeSpace -= seeds;
        }
        else
        {
            PlayerPrefs.SetInt("WareHouseSeeds", seedsCount + whFreeSpace);
            leftover = seeds - whFreeSpace;
            whFreeSpace = 0;
        }
        PlayerPrefs.SetInt("WareHouseFreeSpace", whFreeSpace);

        return leftover;
    }

    public int PutCarrot(int carrot)
    {
        GetValues();
        int leftover;
        if (whFreeSpace >= carrot)
        {
            PlayerPrefs.SetInt("WareHouseCarrot", carrotCount + carrot);
            leftover = 0;
            whFreeSpace -= carrot;
        }
        else
        {
            PlayerPrefs.SetInt("WareHouseCarrot", carrotCount + whFreeSpace);
            leftover = carrot - whFreeSpace;
            whFreeSpace = 0;
        }
        PlayerPrefs.SetInt("WareHouseFreeSpace", whFreeSpace);

        return leftover;
    }

    public bool GetSeeds()
    {
        GetValues();
        if (seedsCount >= 2)
        {
            seedsCount -= 2;
            PlayerPrefs.SetInt("WareHouseSeeds", seedsCount);
            whFreeSpace = PlayerPrefs.GetInt("WareHouseFreeSpace");
            whFreeSpace += 2;
            PlayerPrefs.SetInt("WareHouseFreeSpace", whFreeSpace);
            return true;
        }
        else return false;
    }

    public bool GetHay()
    {
        GetValues();
        if (hayCount > 0)
        {
            PlayerPrefs.SetInt("WareHouseHay", --hayCount);
            whFreeSpace = PlayerPrefs.GetInt("WareHouseFreeSpace");
            PlayerPrefs.SetInt("WareHouseFreeSpace", ++whFreeSpace);
            return true;
        }
        else if (carrotCount > 0) {
            PlayerPrefs.SetInt("WareHouseCarrot", --carrotCount);
            whFreeSpace = PlayerPrefs.GetInt("WareHouseFreeSpace");
            PlayerPrefs.SetInt("WareHouseFreeSpace", ++whFreeSpace);
            return true;
        }
        else return false;
    }
    #endregion
}
