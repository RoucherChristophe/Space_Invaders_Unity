using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour {

    // variables
    public float Force = 600f, DestroyTime = 1f;
    Rigidbody2D rb;
    private PlayerControler playercontroler;
    // variable pour l'explosion
    public GameObject ExplosionPrefab;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playercontroler = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControler>();
    }

    void Start () {
        rb.AddForce(Vector2.up * Force);
        Destroy(gameObject, DestroyTime);
	}

    // destruction du "Bullet" 
    private void OnDestroy()
    {
        playercontroler.tir = true;
    }

    // fonction pour détecter la collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Alien"))
        {
            Destroy(collision.gameObject);
            Instantiate(ExplosionPrefab, collision.transform.position, Quaternion.identity);
            playercontroler.Score += 50;
            Destroy(this.gameObject);
        }
        if (collision.gameObject.CompareTag("Ufo"))
        {
            Destroy(collision.gameObject);
            Instantiate(ExplosionPrefab, collision.transform.position, Quaternion.identity);
            GameObject.Find("Wave").GetComponent<Wave>().Reste_alien += 1;
            playercontroler.Score += 200;
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BulletAlien"))
        {
            Destroy(collision.gameObject);
            playercontroler.Score += 10;
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
