using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoubleScript : MonoBehaviour
{
    public GameObject[] Colors;
    public GameObject scrollPanel, inputPanel, WinBackgroundPanel;
    public Text CurrentCoins, BetSizeText, WinSizeText;
    public Button SpinButton;
    public bool IsSpinning = false;
    public float scrollSpeed;

    private ScrollDouble sr1;
    private int playerScore, betSize = 10;
    private string playerColor;
    private float fade;

    private void Start()
    {
        playerScore = PlayerPrefs.GetInt("score");
        CurrentCoins.text = playerScore.ToString();
        sr1 = scrollPanel.GetComponent<ScrollDouble>();
    }

    private void Update()
    {
        if (IsSpinning == true)
        {
            scrollSpeed = Mathf.MoveTowards(scrollSpeed, 0, fade * Time.deltaTime);
            RaycastHit2D hit = Physics2D.Raycast(Vector2.down, Vector2.up);
            if (hit.collider != null)
            {
                if (scrollSpeed == 0)
                {
                    IsSpinning = false;
                    if (playerColor == hit.collider.gameObject.tag)
                    {
                        int winSize;
                        if (playerColor == "GreenD")
                        {
                            winSize = betSize * 7;
                        }
                        else
                        {
                            winSize = betSize * 2;
                        }
                        ChangePlayerScore(winSize);
                        WinSizeText.text = winSize.ToString();
                        WinBackgroundPanel.SetActive(true);
                    }
                    SpinButton.interactable = true;
                }
            }
            else if (scrollSpeed == 0)
            {
                scrollSpeed = Mathf.MoveTowards(scrollSpeed, -5, -fade * Time.deltaTime);
            }
        }
    }

    public void StartBtn()
    {
        
        SpinButton.interactable = false;
        inputPanel.SetActive(true);

    }

    public void BetDone(Button button)
    {
        WinBackgroundPanel.SetActive(false);
        fade = Random.Range(3.3f, 3.5f);
        playerColor = button.tag;
        if (playerScore >= betSize)
        {
            Spin();
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
        if (betSize + 10 <= playerScore)
        {
            betSize += 10;
            BetSizeText.text = betSize.ToString();
        }
    }

    public void MinusButtonClick()
    {
        if (betSize >= 20)
        {
            betSize -= 10;
            BetSizeText.text = betSize.ToString();
        }
    }

    private void Spin()
    {
        ChangePlayerScore(-betSize);
        inputPanel.SetActive(false);
        sr1.Clear();
        scrollSpeed = -20f;
        IsSpinning = true;
        int randomColor = 0;
        for (int i = 0; i < 45; i++)
        {
            int rand = Random.Range(0, 14);
            if (rand > 0 && rand <= 7)
            {
                randomColor = 0;
            }
            else if (rand > 7 && rand <= 14)
            {
                randomColor = 1;
            }
            else if (rand == 0)
            {
                randomColor = 2;
            }
            GameObject obj = Instantiate(Colors[randomColor], new Vector2(0, 0), Quaternion.identity) as GameObject;
            obj.transform.SetParent(scrollPanel.transform, true);
            obj.transform.localScale = new Vector2(1, 1);
        }
    }
}
