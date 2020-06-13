using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovements : MonoBehaviour
{    
    public float speed;
    public Text CurrentCoins;
    public Sprite[] sprites = new Sprite[5];
    private int PlayerScore, SpriteCnt = 0;
    private void Start() {
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
                SpriteCnt++;
                if (SpriteCnt % 10 <= 5) {
                    gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
                }
                else {
                    gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
                }
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(Camera.main.orthographicSize * Screen.width / Screen.height - 0.45f, transform.position.y), 3.7f * Time.deltaTime);
            }
            else if (Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).x < -0.2f) {
                SpriteCnt++;
                if (SpriteCnt % 10 <= 5) {
                    gameObject.GetComponent<SpriteRenderer>().sprite = sprites[2];
                }
                else {
                    gameObject.GetComponent<SpriteRenderer>().sprite = sprites[3];
                }
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(-Camera.main.orthographicSize * Screen.width / Screen.height + 0.45f, transform.position.y), 3.7f * Time.deltaTime);
            }
        }
        else {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[4];
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
