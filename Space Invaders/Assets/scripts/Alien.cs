using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    public float speed = 10;

    private Rigidbody2D rb;

    public Sprite startingImage;

    public Sprite alternativeImage;

    private SpriteRenderer sr;

    public float secBeforeSpritechange = 0.5f;

    public GameObject alienBullet;

    public float minimumFireRateTime = 1.0f;

    public float maximumFireRateTime = 3.0f;

    public float baseWaitTime = 1.0f;

    public Sprite explodedShipImage; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = new Vector2(1,0)*speed;

        sr = GetComponent<SpriteRenderer>();

        StartCoroutine(changeAlienSprite());

        baseWaitTime = baseWaitTime + Random.Range(minimumFireRateTime, maximumFireRateTime);
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
        position.y -= 1;
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

        if (gameObject.tag == "Bullet")
        {
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienDies);
            Destroy(gameObject);
        }
    }

    public IEnumerator changeAlienSprite()
    {
        while (true)
        {
            if (sr.sprite == startingImage)
            {
                sr.sprite = alternativeImage;
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienBuzz1);
            }
            else
            {
                sr.sprite = startingImage ;
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.alienBuzz2);
            }
            yield return new WaitForSeconds(secBeforeSpritechange);
        }
    }

    private void FixedUpdate()
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
            col.GetComponent<SpriteRenderer>().sprite = explodedShipImage;
            Destroy(gameObject);
            GameObject.Destroy(col.gameObject, 0.2f);
        }
    }
}
