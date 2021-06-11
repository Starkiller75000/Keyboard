using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeScript : MonoBehaviour
{
    [SerializeField]
    float speed = 2f, rayDist = 2f, agroRange = 2f;
    [SerializeField]
    Transform castPoint, player;

    public Transform patrolPoint;

    Animator animator;

    bool isFacingLeft = true;
    bool isAgro = false;
    bool isSearching;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (CanSeePlayer(agroRange))
        {
            isAgro = true;
        }
        else
        {
            if (isAgro)
            {
                if (!isSearching)
                {
                    isSearching = true;
                    Invoke("StopChasingPlayer", 2);
                }
            }
        }

        if (isAgro)
        {
            ChasePlayer();
        }

        if (!CanSeePlayer(agroRange) && !isAgro && !isSearching)
        {
            Patrol();
        }
    }

    bool CanSeePlayer(float distance)
    {
        bool val = false;
        float castDist = distance;

        if (isFacingLeft)
        {
            castDist = -distance;
        }

        Vector2 endPost = castPoint.position + Vector3.right * castDist;
        RaycastHit2D hit = Physics2D.Linecast(castPoint.position, endPost, 1 << LayerMask.NameToLayer("Default"));

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player"))
            {
                val = true;
            } else
            {
                val = false;
            }
            Debug.DrawLine(castPoint.position, hit.point, Color.yellow);
        } else
        {
            Debug.DrawLine(castPoint.position, endPost, Color.blue);
        }

        return val;
    }

    void ChasePlayer()
    {
        if (transform.position.x < player.position.x)
        {
            animator.SetFloat("Speed", speed);
            transform.Translate(Vector2.right * -speed * Time.fixedDeltaTime);
            transform.eulerAngles = new Vector3(0, -180, 0);
            isFacingLeft = false;
        } else
        {
            animator.SetFloat("Speed", speed);
            transform.Translate(Vector2.left * speed * Time.fixedDeltaTime);
            transform.eulerAngles = Vector3.zero;
            isFacingLeft = true;
        }
    }

    void StopChasingPlayer()
    {
        isAgro = false;
        isSearching = false;
    }

    void Patrol()
    {
        if (transform.position.x < patrolPoint.position.x)
        {
            animator.SetFloat("Speed", speed);
            transform.eulerAngles = new Vector3(0, -180, 0);
            isFacingLeft = false;
        } else
        {
            animator.SetFloat("Speed", speed);
            transform.eulerAngles = Vector3.zero;
            isFacingLeft = true;
        }
        transform.position = Vector2.MoveTowards(transform.position, patrolPoint.position, speed / 2 * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player.GetComponent<PlayerController>().TakeDamage();
        }
    }
}
