using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouletteScript : MonoBehaviour
{
    private ScrollRoulette sr1, sr2, sr3;
    public GameObject[] Slots;
    public GameObject[] Panels;
    public bool isSpinning = false;
    public float scrollSpeed;
    public Text CurrentCoins;
    private int PlayerScore, sum;

    private void Start() {
        PlayerScore = PlayerPrefs.GetInt("score");
        CurrentCoins.text = PlayerScore.ToString();
        sr1 = Panels[0].GetComponent<ScrollRoulette>();
        sr2 = Panels[1].GetComponent<ScrollRoulette>();
        sr3 = Panels[2].GetComponent<ScrollRoulette>();
    }

    private void Update() {
        if (isSpinning == true) {
            scrollSpeed = Mathf.MoveTowards(scrollSpeed, 0, 2.62f * Time.deltaTime);
            if (scrollSpeed == 0) {
                isSpinning = false;
                PlayerScore = PlayerPrefs.GetInt("score") + sum;
                CurrentCoins.text = PlayerScore.ToString();
                PlayerPrefs.SetInt("score", PlayerScore);
            }
        }
    }

    public void StartBtn() {
        sr1.clear();
        sr2.clear();
        sr3.clear();
        scrollSpeed = 20f;
        sum = 0;    
        isSpinning = true;
        sum = (int)((float)(Spin(0) + Spin(1) + Spin(2)) / 3 - 2);
    }
    
    private int Spin(int PanelNumber){
        int randomSlot = 0;
        int prevslot = 0;
        for (int i = 0; i < 40; i++) {
            int rand = Random.Range(0, 815);
            prevslot = randomSlot;
            if (rand <=700) {
                randomSlot = 0;
            }   
            else if (rand > 600 && rand <= 750) {
                randomSlot = 1;
            }
            else if (rand > 750 && rand <= 800) {
                randomSlot = 2;
            }
            else if (rand > 800) {
                randomSlot = 3;
            }  
            GameObject obj = Instantiate(Slots[randomSlot], new Vector2(0, 0), Quaternion.identity) as GameObject;
            obj.transform.SetParent(Panels[PanelNumber].transform);
            obj.transform.localScale = new Vector2(1, 1);
        }
        //Debug.Log(prevslot);
        return prevslot;
    }
}
