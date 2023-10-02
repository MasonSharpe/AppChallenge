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
    private Vector3 randomDir = Vector3.right;
    private float randomDirTimer = 5;

    private float hurtTimer = 0;
    public float maxHealth;
    public float health;
    public Transform bulletParent;
    public Transform xpParent;
    public int enemyTypeIndex = 0;
    public GameObject tutorialWall = null;



    private void Start()
    {

        if (NightCycle.instance.isNight) enemyTypeIndex = Mathf.Clamp(GameManager.nightsBeaten.FindAll(h => h == true).Count + Random.Range(-2, 1), 0, 2);
        maxHealth = 3 + (enemyTypeIndex + GameManager.nightsBeaten.FindAll(h => h == true).Count) * 3 + (int)(3 * EnemySpawner.instance.timeStrength);
        health = maxHealth;
        animator.runtimeAnimatorController = animators[enemyTypeIndex];
    }

    private void Update()
    {
        Vector2 dir;
        if (enemyTypeIndex == 2)
        {
            dir = randomDir;
            if (randomDirTimer <= 0)
            {
                randomDir = Random.insideUnitCircle.normalized;
                randomDirTimer = 5;
            }
        }
        else
        {
            dir = (Player.instance.transform.position - transform.position).normalized;
        }
        if (enemyTypeIndex == 1)
        {
            moveSpeed = Mathf.Clamp(10 - Vector3.Distance(Player.instance.transform.position, transform.position), 1, 10);
        }

        if (tutorialWall == null) rb.velocity = moveSpeed * dir;


        shootCooldown -= Time.deltaTime;
        hurtTimer -= Time.deltaTime;
        randomDirTimer -= Time.deltaTime;


        if (shootCooldown <= 0 && !(tutorialWall != null && (Player.instance.transform.position - transform.position).magnitude > 7))
        {
            Bullet bullet = Instantiate(bulletPrefab, bulletParent).GetComponent<Bullet>();
            bullet.transform.position = transform.position;
            bullet.GetComponent<Rigidbody2D>().velocity = bulletSpeed * ((enemyTypeIndex == 2) ? Random.insideUnitCircle.normalized : dir);
            bullet.isFriendly = false;
            bullet.bulletType = enemyTypeIndex;
            bullet.speed = bulletSpeed;


            shootCooldown = (enemyTypeIndex == 2) ? 0.25f : 1.5f;
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
            if (tutorialWall != null)
            {
                print("sda");
                Destroy(tutorialWall);
            }
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

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 11 && enemyTypeIndex == 2)
        {
            randomDir = Random.insideUnitCircle.normalized;
        }
    }

}
