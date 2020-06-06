using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovements : MonoBehaviour
{    
    public float speed;
    public Text CurrentCoins;
    private int PlayerScore;
    private void Start() {
        if (PlayerPrefs.HasKey("score") == false) {
            PlayerPrefs.SetInt("score", 0);
        }
        PlayerScore = PlayerPrefs.GetInt("score");
        CurrentCoins.text = PlayerScore.ToString();
    }
    void Update()
    {
        //Vector3 pos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        //transform.position = new Vector3(pos.x, transform.position.y, transform.position.z);

        float hor = Input.GetAxisRaw("Horizontal");
        Vector3 dir = new Vector3(hor, 0, 0);
        transform.Translate(dir.normalized * Time.deltaTime * speed);
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
