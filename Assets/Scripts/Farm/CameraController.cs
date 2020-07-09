using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public GameObject Player, ArrowsCanvas;
    private float currentX, currentY, lastX, lastY;
    private Vector3 targetPosition;
    private Vector2 offset = new Vector2(1f, -2f);
    private bool isMovingLeft, isMovingRight, isMovingUp, isMovingDown, isMoving = false;
    private float bottomLimit = -9f, upperLimit = 11f, leftLimit = -18f, rightLimit = 18f;

    void Start()
    {
        lastX = (Player.transform.position.x);
        lastY = (Player.transform.position.y);
        targetPosition = transform.position;
        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "GI"))
        {
            DisableArrowss();
        }
    }

    void Update()
    {
        currentX = (Player.transform.position.x);
        currentY = (Player.transform.position.y);

        if (currentY != lastY)
        {
            isMovingDown = currentY < lastY;
            isMovingUp = !isMovingDown;
            isMoving = true;
        }
        else if (currentX != lastX)
        {
            isMovingLeft = currentX < lastX;
            isMovingRight = !isMovingLeft;
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        lastX = currentX;
        lastY = currentY;

        if (isMoving)
        {
            if (isMovingRight)
            {
                targetPosition = new Vector3(currentX, currentY - offset.y, transform.position.z);
            }
            else if (isMovingLeft)
            {
                targetPosition = new Vector3(currentX, currentY - offset.y, transform.position.z);
            }
            else if (isMovingUp)
            {
                targetPosition = new Vector3(currentX, currentY - offset.y, transform.position.z);
            }
            else if (isMovingDown)
            {
                targetPosition = new Vector3(currentX, currentY - offset.y, transform.position.z);
            }

            transform.position = Vector3.Lerp(transform.position, targetPosition, 1f);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftLimit, rightLimit), Mathf.Clamp(transform.position.y, bottomLimit, upperLimit), transform.position.z);
        }

    }

    public void DisableArrowss(){
        Invoke("DisableArrows", 10);
    }

    private void DisableArrows(){
        ArrowsCanvas.SetActive(false);
    }
}
