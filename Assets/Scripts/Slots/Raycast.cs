using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public RaycastHit2D[] Raycastt() {
        RaycastHit2D[] hits;
        hits = Physics2D.RaycastAll(new Vector2(transform.position.x, transform.position.y), new Vector2(transform.position.x+10000, transform.position.y));
        return hits;
    }
}
