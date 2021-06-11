using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    UIScript uiScript;
    [SerializeField]
    AudioClip coinSnd;

    AudioSource audioSource;
    SpriteRenderer spriteRenderer;
    BoxCollider2D collider;

    private void Awake()
    {
        uiScript = GameObject.Find("Canvas").GetComponent<UIScript>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            uiScript.coinNb++;
            audioSource.PlayOneShot(coinSnd);
            spriteRenderer.enabled = false;
            collider.enabled = false;
            Destroy(gameObject, 1f);
        }
    }
}
