using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    private int foodCount, fluffCount, whLevel, whFoodCapacity, whFluffCapacity, playerScore;
    private int foodCost = 1, fluffCost = 2, rabbitCost = 50;
    private bool[] cageStatus = new bool[4];
    public GameObject[] Cages = new GameObject[4];
    public GameObject CageChoosePanel;
    public Text CurrentCoins;

    private void getValues()
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
            if (whFoodCapacity - foodCount > 0 && playerScore > foodCost)
            {
                ChangePlayerScore(-1);
                foodCount++;
                PlayerPrefs.SetInt("WareHouseFood", foodCount);
            }
        }
        if (button.name == "FluffButton")
        {
            if (whFluffCapacity - fluffCount > 0 && playerScore > fluffCost)
            {
                ChangePlayerScore(-2);
                fluffCount++;
                PlayerPrefs.SetInt("WareHouseFluff", fluffCount);
            }
        }
        if (button.name == "RabbitButton")
        {
            CheckCages();
        }
    }

    public void ChooseCageClick(string cage)
    {
        Debug.Log(playerScore);
        if (playerScore >= rabbitCost)
        {
            int rabbitsInCage = PlayerPrefs.GetInt(cage + "Rabbits");
            int cageCapacity = PlayerPrefs.GetInt(cage + "RabbitsCapacity");
            if (cageCapacity - rabbitsInCage > 0)
            {
                rabbitsInCage++;
                PlayerPrefs.SetInt(cage + "Rabbits", rabbitsInCage);
                ChangePlayerScore(-rabbitCost);
            }
        }

    }

    private void ChangePlayerScore(int size)
    {
        playerScore += size;
        CurrentCoins.text = playerScore.ToString();
        PlayerPrefs.SetInt("score", playerScore);
    }

    public void OpenClick()
    {
        Time.timeScale = 0f;
        getValues();
    }

    public void CloseClick()
    {
        Time.timeScale = 1f;
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

}
