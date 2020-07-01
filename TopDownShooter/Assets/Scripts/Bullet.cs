using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Obstacle" || collision.collider.tag == "Enemy")
        {
            if (hitEffect != null)
            {
                GameObject effect = Instantiate(hitEffect, transform.position, transform.rotation);
                Destroy(effect, 3);
            }
            Destroy(gameObject);
        }
    }
}
