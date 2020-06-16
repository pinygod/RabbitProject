using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovements : MonoBehaviour
{    
    public float speed;
    public Text CurrentCoins;
    private int PlayerScore;
    private Animator running;
    private void Start() {
        running = GetComponent<Animator>();
        if (PlayerPrefs.HasKey("score") == false) {
            PlayerPrefs.SetInt("score", 0);
        }
        PlayerScore = PlayerPrefs.GetInt("score");
        CurrentCoins.text = PlayerScore.ToString();
    }
    void Update()
    {
        if(Input.touchCount > 0) {
            if (Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).x > 0.2f) {
                running.SetBool("isRunningLeft", false);
                running.SetBool("isRunningRight", true);
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(Camera.main.orthographicSize * Screen.width / Screen.height - 0.45f, transform.position.y), 3.5f * Time.deltaTime);
            }
            else if (Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).x < -0.2f) {
                running.SetBool("isRunningRight", false);
                running.SetBool("isRunningLeft", true);
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(-Camera.main.orthographicSize * Screen.width / Screen.height + 0.45f, transform.position.y), 3.5f * Time.deltaTime);
            }
        }
        else {
            running.SetBool("isRunningLeft", false);
            running.SetBool("isRunningRight", false);
        }
        
        //transform.position = new Vector3(pos.x, transform.position.y, transform.position.z);

        //float hor = Input.GetAxisRaw("Horizontal");
        //Vector3 dir = new Vector3(hor, 0, 0);
        //transform.Translate(dir.normalized * Time.deltaTime * speed);
    }

    void OnDestroy() {
        PlayerPrefs.SetInt("score", PlayerScore);
    }

    private void OnCollisionStay(Collision collision) {
        if (collision.gameObject.CompareTag("CatchRabbit") && Random.Range(0, 1000) % 10 == 2) {
            Destroy(collision.gameObject);
            PlayerScore++;
            CurrentCoins.text = PlayerScore.ToString();
        }
    }
}
