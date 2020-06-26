using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class HideBunny : MonoBehaviour
{
    public GameObject[] Hats;
    public GameObject Rabbit;
    public Text CurrentCoins, BetSizeText;

    private GameObject StartHat;
    private Vector3 hatStartPosition, hatTargetPosition, hat1StartPos, hat2StartPos, hat3StartPos;
    private float time = 0;
    private bool IsHatMovingUp = false;
    private int playerScore, betSize = 10;

    void Start()
    {
        playerScore = PlayerPrefs.GetInt("score");
        CurrentCoins.text = playerScore.ToString();
        hat1StartPos = Hats[0].transform.position;
        hat2StartPos = Hats[1].transform.position;
        hat3StartPos = Hats[2].transform.position;
    }

    void Update()
    {
        if (IsHatMovingUp == true)
        {
            time += Time.deltaTime;
            StartHat.transform.position = Vector2.Lerp(hatStartPosition, hatTargetPosition, Easing(time));

            if (StartHat.transform.position == hatStartPosition)
            {
                Destroy(GameObject.Find("RabbitShellGame(Clone)"));
                Hats[0].GetComponent<HatsMixer>().MixHats();
                Hats[1].GetComponent<HatsMixer>().MixHats();
                Hats[2].GetComponent<HatsMixer>().MixHats();

                IsHatMovingUp = false;
            }
        }
    }

    private float Easing(float x)
    {
        return x < 0.5 ? x * x * 2 : (1 - (1 - x) * (1 - x) * 2);
    }

    public GameObject newBunny()
    {
        StartHat = Hats[Random.Range(0, Hats.Length)];
        Instantiate(Rabbit, StartHat.transform.position, Quaternion.identity);
        return StartHat;
    }

    public void startButton()
    {
        if (ChangePlayerScore(-betSize))
        {
            time = 0;
            Hats[0].transform.position = hat1StartPos;
            Hats[1].transform.position = hat2StartPos;
            Hats[2].transform.position = hat3StartPos;
            Destroy(GameObject.Find("RabbitShellGame(Clone)"));

            StartHat = Hats[Random.Range(0, Hats.Length)];
            hatStartPosition = StartHat.transform.position;
            hatTargetPosition = hatStartPosition;
            hatTargetPosition.y += 1.5f;

            Instantiate(Rabbit, hatStartPosition, Quaternion.identity);
            IsHatMovingUp = true;
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

    public bool CheckResult(string ChosenHatName)
    {
        if (ChosenHatName == StartHat.name)
        {
            ChangePlayerScore(3 * betSize);
            return true;
        }
        else
        {
            return false;
        }
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

}
