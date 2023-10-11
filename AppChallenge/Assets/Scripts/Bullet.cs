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
    public SpriteRenderer ballRenderer;
    public int bulletType;
    public float timer = 0;
    private float alphaTimer = 0;
    public float magnitude = 1;
    public float speed;
    
    private bool isInFirstState = true;
    private Vector3 newVelocity = Vector3.one;


    private void Start()
    {
        Destroy(gameObject, 8);
        if (bulletType == 1)
        {
            rb.velocity /= 5;
            magnitude = rb.velocity.magnitude;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //sweetspot
        if (collision.gameObject.layer == 9)
        {
            if (!isFriendly)
            {
                isFriendly = true;
                SfxManager.instance.PlaySoundEffect(8, 1, Random.Range(0.9f, 1.2f));
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

    private void Update()
    {
        alphaTimer += Time.deltaTime;
        ballRenderer.color = Helper.SetColorAlpla(Color.white, alphaTimer * 5);


        if (bulletType == 1 &&!isFriendly)
        {
            magnitude += (magnitude * Time.deltaTime);

            if (timer < 1)
            {
                newVelocity = (Player.instance.transform.position - transform.position).normalized;
            }else if (isInFirstState)
            {
                newVelocity = newVelocity.normalized;
                isInFirstState = false;
            }

            rb.velocity = magnitude * newVelocity;


            timer += Time.deltaTime;

        }
    }

}
