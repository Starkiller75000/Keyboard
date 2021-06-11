using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonScript : MonoBehaviour
{
    [SerializeField]
    float fireInterval = 4f;
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    Transform firePoint;
    bool canShoot = true;
    private void Start()
    {
        if (GetComponent<SpriteRenderer>().flipX)
        {
            firePoint.Rotate(0f, 180f, 0f);
        }

        firePoint.Rotate(0f, 0f, 0f);
    }
    private void FixedUpdate()
    {
        if (canShoot)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        GetComponent<AudioSource>().Play();
        canShoot = false;
        yield return new WaitForSeconds(fireInterval);
        canShoot = true;
    }
}
