using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    public float speed = 10;

    private Rigidbody2D rb;

    private SpriteRenderer sr;

    public GameObject alienBullet;

    public float minimumFireRateTime = 1.0f;

    public float maximumFireRateTime = 3.0f;

    public float baseWaitTime = 1.0f;

    public Sprite alien_1_Image;

    public Sprite alien_2_Image;

    public Sprite explodedAlienImage;

    public bool canAnimate = true;

    public float pos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = new Vector2(1,0)*speed;

        sr = GetComponent<SpriteRenderer>();

        baseWaitTime = baseWaitTime + Random.Range(minimumFireRateTime, maximumFireRateTime);

        StartCoroutine(animations());
    }

    //animate

    IEnumerator animations()
    {
        while (canAnimate == true)
        {
            if (sr.sprite == alien_1_Image)
            {
                sr.sprite = alien_2_Image;
            }
            else
            {
                sr.sprite = alien_1_Image;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    void Turn(int direction)
    {
        Vector2 newVelocity = rb.velocity;
        newVelocity.x = speed * direction;
        rb.velocity = newVelocity;
    }

    void moveDown()
    {
        Vector2 position = transform.position;
        position.y -= pos;
        transform.position = position;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "LeftWall")
        {
            Turn(1);
            moveDown();
        }

        if (col.gameObject.name == "RightWall")
        {
            Turn(-1);
            moveDown();
        }
    }

    void Update()
    {
        if (Time.time > baseWaitTime)
        {
            baseWaitTime = baseWaitTime + Random.Range(minimumFireRateTime, maximumFireRateTime);

            Instantiate(alienBullet, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.shipExplosion);
            Destroy(gameObject, 1f);
            GameObject.Destroy(col.gameObject, 0.2f);
        }

        if (col.gameObject.tag == "Bullet")
        {
            canAnimate = false;
            sr.sprite = explodedAlienImage;
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienDies);
            Destroy(gameObject, 0.15f);
        }
    }
}
