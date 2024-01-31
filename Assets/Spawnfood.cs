using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnfood : MonoBehaviour
{
    public GameObject foodPrefab;

    public Transform border_top;
    public Transform border_bottom;
    public Transform border_left;
    public Transform border_right;

    void Update()
    {
        if (GameObject.Find("foodPrefab(Clone)") == null)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        int x = (int)Random.Range(border_left.position.x, border_right.position.x);

        int y = (int)Random.Range(border_top.position.y, border_bottom.position.y);

        Instantiate(foodPrefab, new Vector2(x, y), Quaternion.identity);
    }
}
