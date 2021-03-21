using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpaceShip : MonoBehaviour
{
    public float speed = 30f;

    public float counter;

    public bool canshoot = false;

    public GameObject thebullet;


    private void FixedUpdate()
    {
        float horzMove = Input.GetAxisRaw("Horizontal");

        GetComponent<Rigidbody2D>().velocity = new Vector2(horzMove, 0) * speed;
    }

    // Update is called once per frame
    void Update()
    {
        //counter
        if (counter > 0)
        {
            counter -= Time.deltaTime;
        }
        else
        {
            canshoot = true;
            counter = 0.5f;
        }

        if (Input.GetButtonDown("Jump") && canshoot==true)
        {
            Shoot();
            canshoot = false;
        }


        void Shoot()
        {
            Instantiate(thebullet, transform.position, Quaternion.identity);

            SoundManager.Instance.PlayOneShot(SoundManager.Instance.bulletFire);
        }
    }
}