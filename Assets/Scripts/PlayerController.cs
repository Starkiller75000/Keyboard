using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float speed = 4f, jumpForce = 10f;

    [SerializeField]
    GameObject[] hearts;

    bool jump = false;

    [SerializeField]
    AudioClip ADdamage, ADdeath, ADJump;

    public Hashtable Keys = new Hashtable();

    int life;

    SpriteRenderer spriteRenderer;

    Animator animator;

    Rigidbody2D rb;

    AudioSource audioSource;
    private void Awake()
    {
        Physics.IgnoreLayerCollision(0, 8);
        life = hearts.Length;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        Keys.Add("left", true);
        Keys.Add("right", true);
        Keys.Add("space", true);
    }

    public void block(string block)
    {
        Keys[block] = false;
    }

    public void realease(string block)
    {
        Keys[block] = true;
    }

    private void FixedUpdate()
    {
        bool okay = true;
        float right = Input.GetAxis("Horizontal");
        if (right < 0)
        {
            spriteRenderer.flipX = true;
            if ((bool)Keys["left"] == false)
            {
                okay = false;
            }
        }

        if (right > 0)
        {
            spriteRenderer.flipX = false;
            if ((bool)Keys["right"] == false)
            {
                okay = false;
            }
        }

        if (Input.GetButton("Jump") && !jump)
        {
            if ((bool)Keys["space"] == false)
            {
                right = 0f;
            }
            else 
            {
                rb.velocity = Vector2.up * jumpForce;
                jump = true;
                audioSource.PlayOneShot(ADJump);
                animator.SetBool("Jumping", true);
            }
        }
        if ( okay )
        {
            animator.SetFloat("Speed", Mathf.Abs(right));
            transform.Translate(Vector3.right * speed * Input.GetAxis("Horizontal") * Time.fixedDeltaTime);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" || collision.collider.tag == "Tangible" || collision.collider.tag == "Canon")
        {
            jump = false;
            animator.SetBool("Jumping", false);
        }

        if ( collision.collider.tag == "Spike" )
        {
            TakeDamage();
        }
    }

    public void TakeDamage(int damage = 1)
    {
        if (life >= 1)
        {
            animator.SetTrigger("isHit");
            life -= damage;
            audioSource.PlayOneShot(ADdamage);
            Destroy(hearts[life]);
            if (life < 1)
            {
                Die();
            }
        }
    }

    void Die()
    {
        GameObject Transition = GameObject.FindGameObjectWithTag("End");
        audioSource.PlayOneShot(ADdeath);
        Destroy(gameObject, 1f);
        if (Transition)
        {
            Transition.GetComponent<EffectManager>().ReLoad();
        }
    }
}
