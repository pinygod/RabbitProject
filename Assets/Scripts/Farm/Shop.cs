using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Shop : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int foodCount, fluffCount, whLevel, whFoodCapacity, whFluffCapacity, playerScore;
    private int foodCost = 1, fluffCost = 2, rabbitCost = 50, cageCost = 250;
    private bool[] cageStatus = new bool[4];
    public GameObject[] Cages = new GameObject[4];
    public GameObject CageChoosePanel, ShopPanel;
    public Text CurrentCoins;
    public ControlValues controlValues;

    public void OnPointerClick(PointerEventData eventData)
    {
        Time.timeScale = 0f;
        GetValues();
        ShopPanel.SetActive(true);

    }

    private void GetValues()
    {
        Debug.Log("Getting values...");

        whLevel = PlayerPrefs.GetInt("WareHouseLevel");
        whFoodCapacity = PlayerPrefs.GetInt("WareHouseFoodCapacity");
        whFluffCapacity = PlayerPrefs.GetInt("WareHouseFluffCapacity");
        foodCount = PlayerPrefs.GetInt("WareHouseFood");
        fluffCount = PlayerPrefs.GetInt("WareHouseFluff");
        playerScore = PlayerPrefs.GetInt("score");
    }

    public void BuyClick(Button button)
    {
        if (button.name == "FoodButton")
        {
            if (whFoodCapacity - foodCount > 0 && ChangePlayerScore(-foodCost))
            {
                foodCount++;
                PlayerPrefs.SetInt("WareHouseFood", foodCount);
            }
        }
        if (button.name == "FluffButton")
        {
            if (whFluffCapacity - fluffCount > 0 && ChangePlayerScore(-fluffCost))
            {
                fluffCount++;
                PlayerPrefs.SetInt("WareHouseFluff", fluffCount);
            }
        }
        if (button.name == "RabbitButton")
        {
            CheckCages();
        }
        if (button.name == "CageButton")
        {
            int cageToActivate = CheckCagesForActivation();
            if (cageToActivate != -1)
            {
                if (ChangePlayerScore(-cageCost))
                {
                    controlValues.ActivateCage(cageToActivate);
                }
            }
            else
            {
                //TODO: all cages already opened message.
            }

        }
    }

    public void ChooseCageClick(string cage)
    {
        Debug.Log(playerScore);
        if (ChangePlayerScore(-rabbitCost))
        {
            int rabbitsInCage = PlayerPrefs.GetInt(cage + "Rabbits");
            int cageCapacity = PlayerPrefs.GetInt(cage + "RabbitsCapacity");
            if (cageCapacity - rabbitsInCage > 0)
            {
                rabbitsInCage++;
                PlayerPrefs.SetInt(cage + "Rabbits", rabbitsInCage);
            }
        }

    }

    private bool ChangePlayerScore(int size)
    {
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
