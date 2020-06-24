using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class HatsMixer : MonoBehaviour
{
    //
    private HideBunny hideBunny;
    //

    public GameObject shellController;

    private Vector2 hatStartPosition;
    private Vector2 hatTargetPosition;
    public float time = 0;

    private void Start()
    {
        glag = false;
        
        hatStartPosition = transform.position;
        hatTargetPosition = new Vector2(0, 0);
    }

    //
    float speed = 0.1f;
    //

    bool glag = false;
    bool flag = false;
    bool rFlag = false;
    bool wFlag = false;

    float Easing(float x)
    {
        return x < 0.5 ? x * x * 2 : (1 - (1 - x) * (1 - x) * 2);
    }

    float EasingNo(float x) {
        return Mathf.Asin(Mathf.Sin(x));
    }

    float EasingLinear (float x) {
        return (x*x*x) / 2;
    }

    public void kek()
    {
        glag = true;
    }

    public int ctr2 = 0;

    void Update()
    {
        if (glag == true)
        {
            ctr2 ++;
            transform.position = Vector2.Lerp(hatStartPosition, hatTargetPosition, Easing(time));
            time += Time.deltaTime;
        }

        if (flag == false) {
        if ((ctr2 >= 10) && (transform.position.x == hatStartPosition.x) && (transform.position.y == hatStartPosition.y) && (gameObject.name == "top hat 1")) {
            shellController.GetComponent<HideBunny>().newBunny();
            flag = true;
        }
        }

        if (rFlag == true) {
            if (realHat.transform.position == realHatTargetPosition) {
                rFlag = false;
            }
            realHat.transform.position = Vector2.Lerp(realHat.transform.position, realHatTargetPosition, Easing(time));
            time += Time.deltaTime;
            
        }

    }

    public GameObject realHat, chosenHat;
    [SerializeField] private Vector3 realHatTargetPosition;
    public static bool flagster = false;

    public float x, y;

    public void clear () {
        gameObject.transform.position = new Vector2(x, y);
    }

    void OnMouseUpAsButton()
    {
        if (flagster == false) {
        Debug.Log("Up");
        
        wFlag = false;
        rFlag = false;
        glag = false;

        realHat = shellController.GetComponent<HideBunny>().newHat;
        chosenHat = gameObject;

        realHatTargetPosition = realHat.transform.position;
        realHatTargetPosition.y += 1.5f;

        if (realHat.name == chosenHat.name) { 
            time = 0;
            rFlag = true;
        }
        else {
            gameObject.GetComponent<Animation>().Play();

            time = 0;
            rFlag = true;
        }
        flagster = true;
        }
    }

    
}
