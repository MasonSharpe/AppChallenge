using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{

    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float bulletSpeed = 150;
    private float shootCooldown = 0;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject xpPrefab;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] private SpriteRenderer hurtRenderer;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteMask hurtMask;
    [SerializeField] private Animator animator;
    [SerializeField] private RuntimeAnimatorController[] animators;

    private float hurtTimer = 0;
    public float maxHealth;
    public float health;
    public Transform bulletParent;
    public Transform xpParent;
    public int enemyTypeIndex = 0;



    private void Start()
    {
        maxHealth = 3 + (int)(EnemySpawner.instance.levelTimer / 25f);
        health = maxHealth;
        enemyTypeIndex = 0;
        animator.runtimeAnimatorController = animators[enemyTypeIndex];
    }

    private void Update()
    {
        Vector3 dir = (Player.instance.transform.position - transform.position).normalized;
        rb.velocity = moveSpeed * dir;

        shootCooldown -= Time.deltaTime;
        hurtTimer -= Time.deltaTime;

        if (shootCooldown <= 0)
        {
            Bullet bullet = Instantiate(bulletPrefab, bulletParent).GetComponent<Bullet>();
            bullet.transform.position = transform.position;
            bullet.GetComponent<Rigidbody2D>().velocity = bulletSpeed * dir;
            bullet.isFriendly = false;
            bullet.bulletType = enemyTypeIndex;
            bullet.speed = bulletSpeed;


            shootCooldown = 1.5f;
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
            GameObject xp = Instantiate(xpPrefab, xpParent);
            xp.transform.position = transform.position;
            Destroy(gameObject);
        }
        hurtTimer = 0.1f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //hit by sword
        if (collision.gameObject.layer == 3)
        {
            GetHit(maxHealth / Mathf.Clamp(5 - (LevelUpScreen.instance.normalUpgradesGotten[0] * 0.4f), 1.1f, 5) + Sword.instance.swingDamage);
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
