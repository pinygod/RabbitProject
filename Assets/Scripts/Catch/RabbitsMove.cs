using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RabbitsMove : MonoBehaviour { 
    public Sprite[] sprites = new Sprite[2];
    private int SpriteCnt = 0;

    void Update()
    {
        transform.Translate(new Vector3(transform.position.z, -5f, transform.position.z) * Time.deltaTime);
        if (SpriteCnt % 20 < 10) {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
        }
        else {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
        }
        SpriteCnt++;
        if (transform.position.y < -(Camera.main.orthographicSize) || transform.position.x > (Camera.main.orthographicSize * Screen.width / Screen.height) || transform.position.x < -(Camera.main.orthographicSize * Screen.width / Screen.height))
        {
            GameObject.Find("PlayerRabbit").GetComponent<PlayerMovements>().ChangePlayerScore(-1);
            Destroy(gameObject);
        }
    }

}
