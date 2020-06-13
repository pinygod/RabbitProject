using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoubleScript : MonoBehaviour
{
    private ScrollDouble sr1;
    public GameObject[] Colors;
    public GameObject scrollPanel, inputPanel;
    public bool isSpinning = false;
    public float scrollSpeed;
    public Text CurrentCoins;
    private int PlayerScore, sum, playerBet;
    private string playerColor;
    private float fade;

    private void Start() {
        PlayerScore = PlayerPrefs.GetInt("score");
        CurrentCoins.text = PlayerScore.ToString();
        sr1 = scrollPanel.GetComponent<ScrollDouble>();
    }

    private void Update() {
        if (isSpinning == true) {
            scrollSpeed = Mathf.MoveTowards(scrollSpeed, 0, fade * Time.deltaTime);
            RaycastHit2D hit = Physics2D.Raycast(Vector2.down, Vector2.up);
            if (hit.collider != null) {
                if (scrollSpeed == 0) {
                    isSpinning = false;
                    if (playerColor == hit.collider.gameObject.tag) {
                        PlayerScore = PlayerPrefs.GetInt("score") - playerBet + (playerBet * 2);
                        CurrentCoins.text = PlayerScore.ToString();
                        PlayerPrefs.SetInt("score", PlayerScore);
                    }
                    else {
                        PlayerScore = PlayerPrefs.GetInt("score") - playerBet;
                        CurrentCoins.text = PlayerScore.ToString();
                        PlayerPrefs.SetInt("score", PlayerScore);
                    }
                }
            }
            else if (scrollSpeed == 0) {
                scrollSpeed = Mathf.MoveTowards(scrollSpeed, -5, -fade * Time.deltaTime);
            }
        }
    }

    public void StartBtn() {
        inputPanel.SetActive(true);
        fade = Random.Range(3.3f, 3.5f);
    }

    public void red10() {
        playerBet = 10;
        playerColor = "RedD";
        inputPanel.SetActive(false);
        sr1.clear();
        scrollSpeed = -20f;
        isSpinning = true;
        int res = Spin();
    }
        public void red20() {
        playerBet = 20;
        playerColor = "RedD";
        inputPanel.SetActive(false);
        sr1.clear();
        scrollSpeed = -20f;
        isSpinning = true;
        int res = Spin();

    }
        public void red30() {
        playerBet = 30;
        playerColor = "RedD";
        inputPanel.SetActive(false);
        sr1.clear();
        scrollSpeed = -20f;
        isSpinning = true;
        int res = Spin();
    }
        public void green10() {
        playerBet = 10;
        playerColor = "GreenD";
        inputPanel.SetActive(false);
        sr1.clear();
        scrollSpeed = -20f;
        isSpinning = true;
        int res = Spin();
    }
        public void green20() {
        playerBet = 20;
        playerColor = "GreenD";
        inputPanel.SetActive(false);
        sr1.clear();
        scrollSpeed = -20f;
        isSpinning = true;
        int res = Spin();
    }
        public void green30() {
        playerBet = 30;
        playerColor = "GreenD";
        inputPanel.SetActive(false);
        sr1.clear();
        scrollSpeed = -20f;
        isSpinning = true;
        int res = Spin();
    }
        public void black10() {
        playerBet = 10;
        playerColor = "BlackD";
        inputPanel.SetActive(false);
        sr1.clear();
        scrollSpeed = -20f;
        isSpinning = true;
        int res = Spin();
    }
        public void black20() {
        playerBet = 20;
        playerColor = "BlackD";
        inputPanel.SetActive(false);
        sr1.clear();
        scrollSpeed = -20f;
        isSpinning = true;
        int res = Spin();
    }
        public void black30() {
        playerBet = 30;
        playerColor = "BlackD";
        inputPanel.SetActive(false);
        sr1.clear();
        scrollSpeed = -20f;
        isSpinning = true;
        int res = Spin();
    }
    
    private int Spin(){
        int randomColor = 0;
        int prevslot = 0;
        for (int i = 0; i < 40; i++) {
            int rand = Random.Range(0, 14);
            prevslot = randomColor;
            if (rand > 0 && rand <=7) {
                randomColor = 0;
            }   
            else if (rand > 7 && rand <= 14) {
                randomColor = 1;
            }
            else if (rand == 0) {
                randomColor = 2;
            }
            GameObject obj = Instantiate(Colors[randomColor], new Vector2(0, 0), Quaternion.identity) as GameObject;
            obj.transform.SetParent(scrollPanel.transform);
            obj.transform.localScale = new Vector2(1, 1);
        }
        //Debug.Log(prevslot);
        return prevslot;
    }
}
