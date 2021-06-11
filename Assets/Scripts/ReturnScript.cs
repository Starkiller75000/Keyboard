using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnScript : MonoBehaviour
{
    [SerializeField]
    Transform target;
    [SerializeField]
    SlimeScript slimeScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Slime")
        {
            slimeScript.patrolPoint = target;
        }
    }
}
