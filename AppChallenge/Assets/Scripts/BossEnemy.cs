using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour{

    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float bulletSpeed = 150;
    private float shootCooldown = 0;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] private SpriteRenderer hurtRenderer;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteMask hurtMask;
    [SerializeField] private Animator animator;
    private Vector3 randomDir = Vector3.right;
    private float randomDirTimer = 5;

    private float hurtTimer = 0;
    public float maxHealth;
    public float health;
    public Transform bulletParent;
    public Transform xpParent;
    public int phase = 1;
    public float[] bulletSpeeds = new float[] {7, 7 };

    /*PHASES
     * 1. simple enemy1 AI, but with predictive shooting
     * 2. stand in middle and shoot a bullet in all directions, then rotate a little bit, continue
     * 3. move in circle around map and shoot enemy2 bullets VERY quickly
     * 4. calamity style bullet hell, movement speed greatly decreased
     * 5. move towards you while firing random spreads
     * 6. death animation, close in circles of bullets with varying speed and position, increasing in speed, forcing parries
     */

    private void Start()
    {

        health = maxHealth;
    }

    private void Update()
    {
        Vector2 dir = (Player.instance.transform.position - transform.position).normalized;

       // rb.velocity = moveSpeed * dir;


        shootCooldown -= Time.deltaTime;
        hurtTimer -= Time.deltaTime;
        randomDirTimer -= Time.deltaTime;


        if (shootCooldown <= 0)
        {
            Bullet bullet = Instantiate(bulletPrefab, bulletParent).GetComponent<Bullet>();
            bullet.transform.position = transform.position;

            Vector3 velocity;
            if (phase == 1)
            {
                dir = ((Vector2)(Player.instance.transform.position - transform.position) + Player.instance.rb.velocity).normalized;
            }
            velocity = bulletSpeeds[phase] * dir;
            bullet.GetComponent<Rigidbody2D>().velocity = velocity;

            bullet.isFriendly = false;
            bullet.bulletType = phase + 3;
            bullet.speed = bulletSpeed;


            shootCooldown = (phase == 2) ? 0.25f : 1.5f;
        }

        hurtRenderer.enabled = hurtTimer > 0 ? true : false;
        hurtMask.sprite = spriteRenderer.sprite;

        animator.SetFloat("x", dir.x);
        animator.SetFloat("y", dir.y);
    }

    private void GetHit(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        hurtTimer = 0.1f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //hit by sword
        if (collision.gameObject.layer == 3)
        {
            GetHit(maxHealth / Mathf.Clamp(1500 - (LevelUpScreen.instance.normalUpgradesGotten[0] * 50f), 1500, 150) + Sword.instance.swingDamage);
            Sword.instance.swordCooldown -= Mathf.Clamp(LevelUpScreen.instance.normalUpgradesGotten[1] * 0.02f, 0, 0.25f);

        }

        //hit by player bullet
        if (collision.gameObject.layer == 6)
        {
            if (collision.GetComponent<Bullet>().isFriendly)
            {
                GetHit(collision.GetComponent<Bullet>().currentStoredDamage);
            }
        }


    }


}
