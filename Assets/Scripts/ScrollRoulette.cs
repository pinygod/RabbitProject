using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollRoulette : MonoBehaviour
{
    public RouletteScript rouletteScript;
    private float x, y;

    private void Start() {
        x = gameObject.transform.position.x;
        y = gameObject.transform.position.y;
    }
    private void Update() {
        if (rouletteScript.isSpinning) {
            gameObject.transform.Translate(new Vector2(0, rouletteScript.scrollSpeed) * Time.deltaTime);
        }
    }
    public void clear() {
        gameObject.transform.position = new Vector2(x, y);
        foreach (Transform child in transform) Destroy(child.gameObject);
        
    }
}
