using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyBullet : MonoBehaviour
{
    public float speed = 30f;

    private Rigidbody2D rb;

    public Sprite explodedAlienImage;

    private GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        obj = GameObject.FindGameObjectWithTag("scenemanager");

        rb.velocity = Vector2.up * speed;

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Wall")
        {
            Destroy(gameObject);
        }

        if (col.tag == "Alien")
        {
            IncreasTextUIScore();

            obj.GetComponent<scenemanager>().aliens -= 1;

            Destroy(gameObject);

        }

        if (col.tag == "Shield")
        {
            Destroy(gameObject);
            Object.Destroy(col.gameObject);
        }

    }
    void OneBecomeInvisible()
    {
        Destroy(gameObject);
    }

    void IncreasTextUIScore()
    {
        var textUIComp = GameObject.Find("Score").GetComponent<Text>();

        int score = int.Parse(textUIComp.text);
        score += 1;

        textUIComp.text = score.ToString();
    }
}