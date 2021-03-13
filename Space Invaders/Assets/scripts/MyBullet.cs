using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyBullet : MonoBehaviour
{
    public float speed = 30f;

    private Rigidbody2D rb;

    public Sprite explodedAlienImage;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = Vector2.up * speed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Wall")
        {
            Destroy(gameObject);
        }

        if (col.tag == "Shield")
        {
            Destroy(gameObject);
        }

        if (col.tag == "Alien")
        {
            SoundManager.Instance.PlayOneShot
                (SoundManager.Instance.alienDies);

            IncreasTextUIScore();

            col.GetComponent<SpriteRenderer>().sprite = explodedAlienImage;

            Destroy(gameObject);

            Object.Destroy(col.gameObject, 0.2f);
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