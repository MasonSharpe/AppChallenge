using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{

    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float bulletSpeed = 150;
    private float shootCooldown = 0;
    [SerializeField] private GameObject bulletPrefab;
    public float health;


    private void Update()
    {
        Vector3 dir = (Player.instance.transform.position - transform.position).normalized;
        transform.Translate(moveSpeed * Time.deltaTime * dir);

        shootCooldown -= Time.deltaTime;

        if (shootCooldown <= 0)
        {
            Bullet bullet = Instantiate(bulletPrefab).GetComponent<Bullet>();
            bullet.transform.position = transform.position;
            bullet.GetComponent<Rigidbody2D>().velocity = bulletSpeed * dir;
            bullet.isFriendly = false;

            Destroy(bullet, 5);

            shootCooldown = 1.5f;
        }
    }

    private void GetHit(float damage)
    {
        health -= damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3 && Sword.instance.canDealDamage)
        {
            print("ow");
            GetHit(Sword.instance.swingDamage);

        }

        if (collision.gameObject.layer == 6)
        {
            if (collision.GetComponent<Bullet>().isFriendly)
            {
                print("owiewowie");
                GetHit(collision.GetComponent<Bullet>().currentStoredDamage);
            }
        }
    }

}
