using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField]
    float speed = 2f;
    [SerializeField]
    int damage;
    [SerializeField]
    GameObject explosionPrefab;
    [SerializeField]
    AudioClip explosionSnd;
    void Start()
    {
        if (transform.rotation.x == 180) {
            GetComponent<Rigidbody2D>().velocity = -transform.right * speed;
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if (transform.rotation.x == 0)
        {
            GetComponent<Rigidbody2D>().velocity = transform.right * speed;
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if (player != null)
        {
            Explose();
            player.TakeDamage();
        } else if (collision.tag == "Ground" || collision.tag == "Tangible" )
        {
            Explose();
        }

        Destroy(gameObject, 10f);
    }

    void Explose()
    {
        var explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        explosion.GetComponent<AudioSource>().PlayOneShot(explosionSnd);
        Destroy(gameObject);
        Destroy(explosion, 1f);
    }
}
