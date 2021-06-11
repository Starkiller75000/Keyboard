using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleScript : MonoBehaviour
{
    [SerializeField]
    float speed = 4f, rayDist = 2f;
    [SerializeField]
    Transform groundDetect;
    bool isMovingRight;
    private void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.fixedDeltaTime);
        RaycastHit2D groundCheck = Physics2D.Raycast(groundDetect.position, Vector2.down, rayDist);
        Debug.DrawRay(groundDetect.position, Vector3.down * rayDist, Color.blue);

        if (groundCheck.collider == false)
        {
            if (isMovingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                isMovingRight = false;
            } else
            {
                transform.eulerAngles = Vector3.zero;
                isMovingRight = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage();
        }
    }
}
