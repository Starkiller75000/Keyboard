using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidColliderScript : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage();
        }
    }
}
