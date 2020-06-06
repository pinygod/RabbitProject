using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RabbitsMove : MonoBehaviour { 
    public float speed;
    public GameObject rabbit;
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
        if (transform.position.y < -4.7f || transform.position.x > 3.3f || transform.position.x < -2f)
        {
            Destroy(gameObject);
        }
    }

}
