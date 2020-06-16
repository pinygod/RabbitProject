using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CageClick : MonoBehaviour
{

    public GameObject cagePanel;
    public Text rabbitsInCage, foodInCage, Money, Food;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnMouseUpAsButton() {
       switch (gameObject.name) {
            case "Cage1":
                Time.timeScale = 0f;
                cagePanel.SetActive(true);
                rabbitsInCage.text = (10).ToString();
                break;
            case "Cage2":
                Time.timeScale = 0f;
                cagePanel.SetActive(true);
                rabbitsInCage.text = (10).ToString();
                break;
            case "Cage3":
                Time.timeScale = 0f;
                cagePanel.SetActive(true);
                rabbitsInCage.text = (10).ToString();
                break;
            case "Cage4":
                Time.timeScale = 0f;
                cagePanel.SetActive(true);
                rabbitsInCage.text = (10).ToString();
                break;                         
       }
   }

    public void OnCloseClick() {
        cagePanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
