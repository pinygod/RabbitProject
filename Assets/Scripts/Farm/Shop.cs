using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Shop : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int hayCount, fluffCount, seedsCount, whLevel, whFreeSpace, whCapacity, playerScore;
    private int hayCost = 1, fluffCost = 2, rabbitCost = 50, cageCost = 250, seedsCost = 30;
    private bool[] cageStatus = new bool[4];
    public GameObject[] Cages = new GameObject[4];
    public GameObject CageChoosePanel, ShopPanel;
    public Text CurrentCoins;
    public ControlValues controlValues;

    public void OnPointerClick(PointerEventData eventData)
    {
        Time.timeScale = 0f;
        ShopPanel.SetActive(true);

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
        playerScore = PlayerPrefs.GetInt("score");
    }

    public void BuyClick(Button button)
    {
        GetValues();
        if (button.name == "HayButton")
        {
            if (whFreeSpace > 0 && ChangePlayerScore(-hayCost))
            {
                PlayerPrefs.SetInt("WareHouseHay", ++hayCount);
                PlayerPrefs.SetInt("WareHouseFreeSpace", --whFreeSpace);
            }
        }
        if (button.name == "FluffButton")
        {
            if (whFreeSpace > 0 && ChangePlayerScore(-fluffCost))
            {
                PlayerPrefs.SetInt("WareHouseFluff", ++fluffCount);
                PlayerPrefs.SetInt("WareHouseFreeSpace", --whFreeSpace);
            }
        }
        if (button.name == "RabbitButton")
        {
            Debug.Log(playerScore);
            int rabbitsInCage = PlayerPrefs.GetInt("RabbitsCageRabbits");
            int cageCapacity = PlayerPrefs.GetInt("RabbitsCageRabbitsCapacity");
            if (cageCapacity - rabbitsInCage > 0 && ChangePlayerScore(-rabbitCost))
            {
                rabbitsInCage++;
                PlayerPrefs.SetInt("RabbitsCageRabbits", rabbitsInCage);
            }
        }
        // if (button.name == "CageButton")
        // {
        //     int cageToActivate = CheckCagesForActivation();
        //     if (cageToActivate != -1)
        //     {
        //         if (ChangePlayerScore(-cageCost))
        //         {
        //             controlValues.ActivateCage(cageToActivate);
        //         }
        //     }
        //     else
        //     {
        //         //TODO: all cages already opened message.
        //     }
        // }
        if (button.name == "SeedsButton")
        {
            if (whFreeSpace >= 5 && ChangePlayerScore(-seedsCost))
            {
                seedsCount += 5;
                PlayerPrefs.SetInt("WareHouseSeeds", seedsCount);
                whFreeSpace -= 5;
                PlayerPrefs.SetInt("WareHouseFreeSpace", whFreeSpace);
            }
        }
    }

    private bool ChangePlayerScore(int size)
    {
        playerScore = PlayerPrefs.GetInt("score");
        if (size < 0 && playerScore >= -size)
        {
            playerScore += size;
            CurrentCoins.text = playerScore.ToString();
            PlayerPrefs.SetInt("score", playerScore);
            return true;
        }
        else if (size >= 0)
        {
            playerScore += size;
            CurrentCoins.text = playerScore.ToString();
            PlayerPrefs.SetInt("score", playerScore);
            return true;
        }
        else
        {
            return false;
        }

    }

    public void OpenClick()
    {
        Time.timeScale = 0f;
        GetValues();
    }

    public void CloseClick()
    {
        Time.timeScale = 1f;
        ShopPanel.SetActive(false);
    }

    private void CheckCages()
    {
        Debug.Log("Checking...");
        for (int i = 1; i < 5; i++)
        {
            if (PlayerPrefs.GetString("Cage" + i + "Status") == "on")
            {
                cageStatus[i - 1] = true;
                Cages[i - 1].SetActive(true);
            }
            else
            {
                Cages[i - 1].SetActive(false);
            }
        }
    }

    private int CheckCagesForActivation()
    {
        for (int i = 1; i < 5; i++)
        {
            if (PlayerPrefs.GetString("Cage" + i + "Status") == "off")
                return i;
        }
        return -1;
    }

}
