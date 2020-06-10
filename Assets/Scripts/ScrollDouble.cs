using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollDouble : MonoBehaviour
{
    public DoubleScript doubleScript;
    private float x, y;

    private void Start() {
        x = gameObject.transform.position.x;
        y = gameObject.transform.position.y;
    }
    private void Update() {
        if (doubleScript.isSpinning) {
            gameObject.transform.Translate(new Vector2(doubleScript.scrollSpeed, 0) * Time.deltaTime);
        }
    }
    public void clear() {
        gameObject.transform.position = new Vector2(x, y);
        foreach (Transform child in transform) Destroy(child.gameObject);
        
    }
}
