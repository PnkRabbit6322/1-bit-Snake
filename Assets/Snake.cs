using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using TMPro;

public class Snake : MonoBehaviour
{
    int score = 0;
    Vector2 dir = Vector2.right;
    List<Transform> tail = new List<Transform>();
    bool ate = false;
    public GameObject tailPrefab;
    public TextMeshProUGUI UI_Score;
    static float speed;
    const float maxSpeed = 0.03f;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 v = transform.position;
        for (int i = 0; i <= 2; i++)
        {
            GameObject g = (GameObject)Instantiate(tailPrefab, v - new Vector2(1+i, 0), Quaternion.identity);
            tail.Insert(0, g.transform);
        }
        speed = 0.1f;
        InvokeRepeating("Move", 1f, speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) && (dir != -Vector2.right))
            dir = Vector2.right;
        else if (Input.GetKey(KeyCode.DownArrow) && (dir != Vector2.up))
            dir = -Vector2.up;    // '-up' means 'down'
        else if (Input.GetKey(KeyCode.LeftArrow) && (dir != Vector2.right))
            dir = -Vector2.right; // '-right' means 'left'
        else if (Input.GetKey(KeyCode.UpArrow) && (dir != -Vector2.up))
            dir = Vector2.up;
    }

    void Move() 
    {
        Vector2 v = transform.position;
        transform.Translate(dir);
        
        if (ate) 
        {
            GameObject g = (GameObject)Instantiate(tailPrefab, v, Quaternion.identity);

            tail.Insert(0, g.transform);
            score++;
            UI_Score.text = "Score: " + score.ToString();

            ate = false;
            if (speed > maxSpeed)
            {
                speed -= 0.005f;
                CancelInvoke();
                InvokeRepeating("Move", 0, speed);
            }
        } 
        else if (tail.Count > 0)
        {
            tail.Last().position = v;

            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.name.StartsWith("foodPrefab"))
        {
            ate = true;
            Destroy(coll.gameObject);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}
