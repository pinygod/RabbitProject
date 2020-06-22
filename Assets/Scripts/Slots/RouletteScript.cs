
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouletteScript : MonoBehaviour
{
    private ScrollRoulette sr1, sr2, sr3;
    public GameObject[] Slots;
    public GameObject[] Panels;
    public GameObject CenterPanel;
    public Button SpinButton, MinusButton, PlusButton;
    public bool IsSpinning = false, Continue = false;
    public float ScrollSpeed, Fade;
    public Text CurrentCoins, BetSizeText;
    private int playerScore, sum, betSize = 10;
    private RaycastHit2D[] hit;

    private void Start()
    {
        playerScore = PlayerPrefs.GetInt("score");
        CurrentCoins.text = playerScore.ToString();
        sr1 = Panels[0].GetComponent<ScrollRoulette>();
        sr2 = Panels[1].GetComponent<ScrollRoulette>();
        sr3 = Panels[2].GetComponent<ScrollRoulette>();
    }

    private void Update()
    {
        if (IsSpinning == true)
        {
            ScrollSpeed = Mathf.MoveTowards(ScrollSpeed, 0, Fade * Time.deltaTime);
            //scrollSpeed = Mathf.MoveTowards(scrollSpeed, 0, 2.62f * Time.deltaTime);
            if (ScrollSpeed == 0)
            {
                hit = CenterPanel.GetComponent<Raycast>().Raycastt();
                //hit = Physics2D.RaycastAll(new Vector2(0, centerPanel.transform.position.y), new Vector2(10, centerPanel.transform.position.y));  

                if (hit.Length < 3)
                {
                    ScrollSpeed = Mathf.MoveTowards(ScrollSpeed, -5, -Fade * Time.deltaTime);
                    Continue = false;
                }
                else
                    Continue = true;

                if (Continue)
                {
                    IsSpinning = false;
                    for (int i = 0; i < 3; i++)
                    {
                        if (hit[i].collider != null)
                        {
                            sum += int.Parse(hit[i].collider.gameObject.name[4].ToString());
                        }
                    }
                    sum *= betSize / 5;
                    SpinButton.interactable = true;
                }

                ChangePlayerScore(sum);
            }
        }
    }

    public void StartBtn()
    {
        playerScore = PlayerPrefs.GetInt("score");
        if (playerScore >= betSize)
        {
            ChangePlayerScore(-betSize);
            sr1.clear();
            sr2.clear();
            sr3.clear();
            ScrollSpeed = 20f;
            Fade = Random.Range(3.3f, 3.5f);
            sum = 0;
            IsSpinning = true;
            SpinButton.interactable = false;
            Spin(0);
            Spin(1);
            Spin(2);
        }
    }

    private void Spin(int PanelNumber)
    {
        int randomSlot = 0;
        for (int i = 0; i < 40; i++)
        {
            int rand = Random.Range(0, 815);
            if (rand <= 500)
            {
                randomSlot = 0;
            }
            else if (rand > 500 && rand <= 650)
            {
                randomSlot = 1;
            }
            else if (rand > 650 && rand <= 800)
            {
                randomSlot = 2;
            }
            else if (rand > 800)
            {
                randomSlot = 3;
            }
            GameObject obj = Instantiate(Slots[randomSlot], new Vector2(0, 0), Quaternion.identity) as GameObject;
            obj.transform.SetParent(Panels[PanelNumber].transform);
            obj.transform.localScale = new Vector2(1, 1);
        }
    }

    private void ChangePlayerScore(int size)
    {
        playerScore += size;
        CurrentCoins.text = playerScore.ToString();
        PlayerPrefs.SetInt("score", playerScore);
    }

    public void PlusButtonClick()
    {
        betSize += 10;
        BetSizeText.text = betSize.ToString();
    }

    public void MinusButtonClick()
    {
        if (betSize >= 20)
        {
            betSize -= 10;
            BetSizeText.text = betSize.ToString();
        }
    }
}
