using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool isFriendly = false;
    public float currentStoredDamage;
    public Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            currentStoredDamage *= 2;
            rb.velocity *= 1.3f;
        }
    }

}
