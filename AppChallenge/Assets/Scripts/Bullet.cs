using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool isFriendly = false;
    public float currentStoredDamage;
    public bool hitSweetSpot = false;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;


    private void Start()
    {
        Destroy(gameObject, 10);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //sweetspot
        if (collision.gameObject.layer == 9)
        {
            if (!isFriendly)
            {
                isFriendly = true;
                currentStoredDamage = Sword.instance.parryDamage;
                rb.velocity = (Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position)).normalized * Sword.instance.bulletParrySpeed;
                spriteRenderer.color = Color.cyan;
                hitSweetSpot = true;
            }
            currentStoredDamage *= 2 + LevelUpScreen.instance.normalUpgradesGotten[4] * 0.5f;
            rb.velocity *= 1.5f;
        }

        if (collision.gameObject.layer == 11)
        {
            Destroy(gameObject);
        }
    }

}
