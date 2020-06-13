
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouletteScript : MonoBehaviour
{
    private ScrollRoulette sr1, sr2, sr3;
    public GameObject[] Slots;
    public GameObject[] Panels;
    public bool isSpinning = false, cont = false;
    public float scrollSpeed, fade;
    public Text CurrentCoins;
    public int PlayerScore, sum;
    private RaycastHit2D[] hit;

    private void Start() {
        PlayerScore = PlayerPrefs.GetInt("score");
        CurrentCoins.text = PlayerScore.ToString();
        sr1 = Panels[0].GetComponent<ScrollRoulette>();
        sr2 = Panels[1].GetComponent<ScrollRoulette>();
        sr3 = Panels[2].GetComponent<ScrollRoulette>();
    }

    private void Update() {
        if (isSpinning == true) {
            scrollSpeed = Mathf.MoveTowards(scrollSpeed, 0, fade * Time.deltaTime);
            //scrollSpeed = Mathf.MoveTowards(scrollSpeed, 0, 2.62f * Time.deltaTime);
            if (scrollSpeed == 0) { 
                hit = Physics2D.RaycastAll(Vector2.left, Vector2.right);                      
                for (int i = 0; i < 3; i++) {
                    if (hit[i].collider == null) {
                        scrollSpeed = Mathf.MoveTowards(scrollSpeed, -5, -fade * Time.deltaTime);
                        cont = false;
                    }   
                    else
                        cont = true;
                }
                if (cont)
                isSpinning = false;                
                for (int i = 0; i < 3; i++) {
                    if (hit[i].collider != null) {
                        sum += int.Parse(hit[i].collider.gameObject.name[4].ToString());
                    }   
                }

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
        //fade = Random.Range(3.3f, 3.5f);
        fade = 3.31474f;
        sum = 0;    
        isSpinning = true;
        Spin(0);
        Spin(1);
        Spin(2);
    }
    
    private void Spin(int PanelNumber){
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
    }
}
