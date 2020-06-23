using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementsFarm : MonoBehaviour
{
    public float speed;
    public Text CurrentCoins;
    private int PlayerScore;
    private Animator running;
    private float touchBorder = 1.5f;
    private Vector2 touchPosition, playerPosition;

    private void Start()
    {
        running = GetComponent<Animator>();
        PlayerScore = PlayerPrefs.GetInt("score");
        CurrentCoins.text = PlayerScore.ToString();
    }
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            playerPosition = transform.position;
            if (IsInRange(touchPosition.y, playerPosition.y + touchBorder, playerPosition.y + (touchBorder * 4)))
            {
                running.SetBool("isRunningLeft", false);
                running.SetBool("isRunningRight", false);
                running.SetBool("isRunningDown", false);
                running.SetBool("isRunningUp", true);
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, 1000f), 3.5f * Time.deltaTime);
            }
            else if (IsInRange(touchPosition.y, playerPosition.y - (touchBorder * 4), playerPosition.y - touchBorder))
            {
                running.SetBool("isRunningLeft", false);
                running.SetBool("isRunningRight", false);
                running.SetBool("isRunningUp", false);
                running.SetBool("isRunningDown", true);
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, -1000f), 3.5f * Time.deltaTime);
            }
            else if (IsInRange(touchPosition.x, playerPosition.x + touchBorder, playerPosition.x + (touchBorder * 4)))
            {
                running.SetBool("isRunningUp", false);
                running.SetBool("isRunningDown", false);
                running.SetBool("isRunningLeft", false);
                running.SetBool("isRunningRight", true);
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(1000f, transform.position.y), 3.5f * Time.deltaTime);
            }
            else if (IsInRange(touchPosition.x, playerPosition.x - (touchBorder * 4), playerPosition.x - touchBorder))
            {
                running.SetBool("isRunningUp", false);
                running.SetBool("isRunningDown", false);
                running.SetBool("isRunningRight", false);
                running.SetBool("isRunningLeft", true);
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(-1000f, transform.position.y), 3.5f * Time.deltaTime);
            }
            else
            {
                running.SetBool("isRunningUp", false);
                running.SetBool("isRunningDown", false);
                running.SetBool("isRunningLeft", false);
                running.SetBool("isRunningRight", false);
            }
        }
        else
        {
            running.SetBool("isRunningUp", false);
            running.SetBool("isRunningDown", false);
            running.SetBool("isRunningLeft", false);
            running.SetBool("isRunningRight", false);
        }

        //transform.position = new Vector3(pos.x, transform.position.y, transform.position.z);

        //float hor = Input.GetAxisRaw("Horizontal");
        //Vector3 dir = new Vector3(hor, 0, 0);
        //transform.Translate(dir.normalized * Time.deltaTime * speed);
    }

    private bool IsInRange(float numberToCheck, float bottom, float top)
    {
        return (numberToCheck >= bottom && numberToCheck <= top);
    }

}
