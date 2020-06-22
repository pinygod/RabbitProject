using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlValues : MonoBehaviour
{
    public GameObject[] Cages = new GameObject[4];

    void Start()
    {
        if (PlayerPrefs.HasKey("score") == false)
        {
            PlayerPrefs.SetInt("score", 0);
        }


        #region WareHouse_Prefs

        if (!PlayerPrefs.HasKey("WareHouseLevel"))
        {
            PlayerPrefs.SetInt("WareHouseLevel", 1);
            PlayerPrefs.SetInt("WareHouseFoodCapacity", 30);
            PlayerPrefs.SetInt("WareHouseFluffCapacity", 60);
        }
        if (!PlayerPrefs.HasKey("WareHouseFood"))
        {
            PlayerPrefs.SetInt("WareHouseFood", 0);
        }
        if (!PlayerPrefs.HasKey("WareHouseFluff"))
        {
            PlayerPrefs.SetInt("WareHouseFluff", 0);
        }

        #endregion


        #region Cages_Prefs

        PlayerPrefs.SetString("Cage1Status", "on");
        for (int i = 1; i <= 4; i++)
        {
            if (!PlayerPrefs.HasKey("Cage" + i + "Status"))
            {
                PlayerPrefs.SetString("Cage" + i + "Status", "off");
            }
            else
            {
                if (PlayerPrefs.GetString("Cage" + i + "Status") == "on")
                {
                    int level;
                    if (!PlayerPrefs.HasKey("Cage" + i + "Level"))
                    {
                        PlayerPrefs.SetInt("Cage" + i + "Level", 1);
                        level = 1;
                    }
                    else
                    {
                        level = PlayerPrefs.GetInt("Cage" + i + "Level");
                    }

                    if (!PlayerPrefs.HasKey("Cage" + i + "Food"))
                    {
                        PlayerPrefs.SetInt("Cage" + i + "Food", 0);
                    }

                    if (!PlayerPrefs.HasKey("Cage" + i + "Fluff"))
                    {
                        PlayerPrefs.SetInt("Cage" + i + "Fluff", 0);
                    }

                    if (!PlayerPrefs.HasKey("Cage" + i + "Rabbits"))
                    {
                        PlayerPrefs.SetInt("Cage" + i + "Rabbits", 0);
                    }

                    if (!PlayerPrefs.HasKey("Cage" + i + "RabbitsCapacity"))
                    {
                        PlayerPrefs.SetInt("Cage" + i + "RabbitsCapacity", level * 5);
                    }

                    if (!PlayerPrefs.HasKey("Cage" + i + "FoodCapacity"))
                    {
                        PlayerPrefs.SetInt("Cage" + i + "FoodCapacity", level * 30);
                    }

                    if (!PlayerPrefs.HasKey("Cage" + i + "FluffCapacity"))
                    {
                        PlayerPrefs.SetInt("Cage" + i + "FluffCapacity", level * 50);
                    }
                }
                else
                {
                    Cages[i - 1].SetActive(false);
                }
            }
        }

        #endregion

    }
}
