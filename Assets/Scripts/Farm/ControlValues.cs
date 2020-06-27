using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlValues : MonoBehaviour
{
    public GameObject[] Cages = new GameObject[4];
    public GameObject[] Gardens = new GameObject[4];

    void Awake()
    {
        if (!PlayerPrefs.HasKey("score"))
        {
            PlayerPrefs.SetInt("score", 21456);
        }


        #region WareHouse_Prefs

        if (!PlayerPrefs.HasKey("WareHouseLevel"))
        {
            PlayerPrefs.SetInt("WareHouseLevel", 1);
            PlayerPrefs.SetInt("WareHouseCapacity", 100);
            PlayerPrefs.SetInt("WareHouseFreeSpace", 100);
        }
        if (!PlayerPrefs.HasKey("WareHouseHay"))
        {
            PlayerPrefs.SetInt("WareHouseHay", 0);
        }
        if (!PlayerPrefs.HasKey("WareHouseCarrot"))
        {
            PlayerPrefs.SetInt("WareHouseCarrot", 0);
        }
        if (!PlayerPrefs.HasKey("WareHouseSeeds"))
        {
            PlayerPrefs.SetInt("WareHouseSeeds", 0);
        }
        if (!PlayerPrefs.HasKey("WareHouseFluff"))
        {
            PlayerPrefs.SetInt("WareHouseFluff", 0);
        }

        #endregion


        #region Cages_Prefs

        int level;
        if (!PlayerPrefs.HasKey("RabbitsCageLevel"))
        {
            level = 1;
            PlayerPrefs.SetInt("RabbitsCageLevel", level);
        }
        else
        {
            level = PlayerPrefs.GetInt("RabbitsCageLevel");
        }

        if (!PlayerPrefs.HasKey("RabbitsCageFood"))
        {
            PlayerPrefs.SetInt("RabbitsCageFood", 0);
        }

        if (!PlayerPrefs.HasKey("RabbitsCageFluff"))
        {
            PlayerPrefs.SetInt("RabbitsCageFluff", 0);
        }

        if (!PlayerPrefs.HasKey("RabbitsCageRabbits"))
        {
            PlayerPrefs.SetInt("RabbitsCageRabbits", 0);
        }

        if (!PlayerPrefs.HasKey("RabbitsCageRabbitsCapacity"))
        {
            PlayerPrefs.SetInt("RabbitsCageRabbitsCapacity", level * 5);
        }

        if (!PlayerPrefs.HasKey("RabbitsCageFoodCapacity"))
        {
            PlayerPrefs.SetInt("RabbitsCageFoodCapacity", level * 30);
        }

        if (!PlayerPrefs.HasKey("RabbitsCageFluffCapacity"))
        {
            PlayerPrefs.SetInt("RabbitsCageFluffCapacity", level * 50);
        }

        #endregion


        #region Gardens_Prefs

        for (int i = 1; i <= 4; i++)
        {
            if (!PlayerPrefs.HasKey("Garden" + i + "Seeds"))
            {
                PlayerPrefs.SetInt("Garden" + i + "Seeds", 0);
            }

            if (!PlayerPrefs.HasKey("Garden" + i + "Carrot"))
            {
                PlayerPrefs.SetInt("Cage" + i + "Fluff", 0);
            }
        }

        #endregion

    }

    public void ActivateCage(int cageNumber)
    {
        PlayerPrefs.SetString("Cage" + cageNumber + "Status", "on");
        int level = 1;
        PlayerPrefs.SetInt("Cage" + cageNumber + "Level", level);
        PlayerPrefs.SetInt("Cage" + cageNumber + "Food", 0);
        PlayerPrefs.SetInt("Cage" + cageNumber + "Fluff", 0);
        PlayerPrefs.SetInt("Cage" + cageNumber + "Rabbits", 0);
        PlayerPrefs.SetInt("Cage" + cageNumber + "RabbitsCapacity", level * 5);
        PlayerPrefs.SetInt("Cage" + cageNumber + "FoodCapacity", level * 30);
        PlayerPrefs.SetInt("Cage" + cageNumber + "FluffCapacity", level * 50);
        Cages[cageNumber - 1].SetActive(true);
    }
}
