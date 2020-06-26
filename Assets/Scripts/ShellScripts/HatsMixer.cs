using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class HatsMixer : MonoBehaviour
{
    public GameObject shellController;
    private GameObject realHat, chosenHat;
    private Vector3 hatStartPosition;
    private Vector3 hatTargetPosition = new Vector2(0, 0);
    private Vector3 realHatTargetPosition;
    private float time = 0;
    private static bool IsAbleToChooseHat = false;
    private bool rFlag = false, IsHatsMixing = false;

    private void Start()
    {
        hatStartPosition = transform.position;
    }

    void Update()
    {
        if (IsHatsMixing == true)
        {
            Debug.Log(gameObject.name);
            time += Time.deltaTime;
            transform.position = Vector2.Lerp(hatStartPosition, hatTargetPosition, Easing(time));
        }

        if (IsHatsMixing && (transform.position == hatStartPosition))
        {
            IsHatsMixing = false;
            IsAbleToChooseHat = true;
        }

        if (rFlag == true)
        {
            realHat.transform.position = Vector2.Lerp(realHat.transform.position, realHatTargetPosition, time * time);
            time += Time.deltaTime;
            if (realHat.transform.position == realHatTargetPosition)
                rFlag = false;
        }
    }

    private float Easing(float x)
    {
        return x < 0.5 ? x * x * 2 : (1 - (1 - x) * (1 - x) * 2);
    }

    public void MixHats()
    {
        time = 0;
        rFlag = false;
        IsHatsMixing = true;
    }

    void OnMouseUpAsButton()
    {
        if (IsAbleToChooseHat)
        {
            IsAbleToChooseHat = false;
            IsHatsMixing = false;
            realHat = shellController.GetComponent<HideBunny>().newBunny();
            chosenHat = gameObject;

            realHatTargetPosition = realHat.transform.position;
            realHatTargetPosition.y += 1.5f;

            if (!(shellController.GetComponent<HideBunny>().CheckResult(chosenHat.name)))
            {
                gameObject.GetComponent<Animation>().Play();
            }
            time = 0;
            rFlag = true;
        }
    }

}
