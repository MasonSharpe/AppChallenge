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
    public float maxHealth;
    public float health;
    public Transform bulletParent;
    public Transform xpParent;



    private void Start()
    {
        maxHealth = 3 + (int)(EnemySpawner.instance.levelTimer / 25f);
        health = maxHealth;
    }

    private void Update()
    {
        Vector3 dir = (Player.instance.transform.position - transform.position).normalized;
        rb.velocity = moveSpeed * dir;

        shootCooldown -= Time.deltaTime;

        if (shootCooldown <= 0)
        {
            Bullet bullet = Instantiate(bulletPrefab, bulletParent).GetComponent<Bullet>();
            bullet.transform.position = transform.position;
            bullet.GetComponent<Rigidbody2D>().velocity = bulletSpeed * dir;
            bullet.isFriendly = false;


            shootCooldown = 1.5f;
        }
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //hit by sword
        if (collision.gameObject.layer == 3)
        {
            GetHit(maxHealth / (5 - (LevelUpScreen.instance.normalUpgradesGotten[0] * 0.4f)) + Sword.instance.swingDamage);
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
