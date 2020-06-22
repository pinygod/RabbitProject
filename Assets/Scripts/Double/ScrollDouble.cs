using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollDouble : MonoBehaviour
{
    public DoubleScript doubleScript;
    [SerializeField] private float x, y, z;

    private void Start()
    {
        x = gameObject.transform.position.x;
        y = gameObject.transform.position.y;
        z = gameObject.transform.position.z;
    }
    private void Update()
    {
        if (doubleScript.IsSpinning)
        {
            gameObject.transform.Translate(new Vector3(doubleScript.scrollSpeed, 0, 0) * Time.deltaTime);
        }
    }
    public void Clear()
    {
        gameObject.transform.position = new Vector3(x, y, z);
        foreach (Transform child in transform) Destroy(child.gameObject);
    }
}
