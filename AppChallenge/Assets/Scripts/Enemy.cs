using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{

    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float bulletSpeed = 150;
    public float maxHealth;
    public float damage = 1;
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
    public bool isNighttimeEnemy;
    public int ID;

    private float hurtTimer = 0;
    public float health;
    public Transform bulletParent;
    public Transform xpParent;
    public int enemyTypeIndex = 0;
    public GameObject tutorialWall = null;
    private bool canDropXp = true;



    private void Start()
    {

        if (NightCycle.instance.isNight)
        {
            maxHealth = 20 * (enemyTypeIndex + GameManager.nightsBeaten.FindAll(h => h == true).Count + 1) * (int)(1 + EnemySpawner.instance.timeStrength);
            enemyTypeIndex = Mathf.Clamp(GameManager.nightsBeaten.FindAll(h => h == true).Count + Random.Range(-2, 1), 0, 2);

        }
        health = maxHealth;
        animator.runtimeAnimatorController = animators[enemyTypeIndex];
    }

    private void Update()
    {
        Vector2 distance = Player.instance.transform.position - transform.position;
        float magnitude = distance.magnitude;
        if (magnitude < 20 || isNighttimeEnemy)
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
                dir = distance.normalized;
            }
            if (enemyTypeIndex == 1)
            {
                moveSpeed = Mathf.Clamp(10 - Vector3.Distance(Player.instance.transform.position, transform.position), 1, 10);
            }

            if (tutorialWall == null) rb.velocity = moveSpeed * dir;


            shootCooldown -= Time.deltaTime;
            hurtTimer -= Time.deltaTime;
            randomDirTimer -= Time.deltaTime;

            bool shouldShoot;
            shouldShoot = (!(tutorialWall != null && magnitude > 7)) && (!(NightCycle.instance.isNight && !isNighttimeEnemy));
            if (shootCooldown <= 0 && shouldShoot)
            {
                Bullet bullet = Instantiate(bulletPrefab, bulletParent).GetComponent<Bullet>();
                bullet.transform.position = transform.position;
                bullet.GetComponent<Rigidbody2D>().velocity = bulletSpeed * ((enemyTypeIndex == 2) ? Random.insideUnitCircle.normalized : dir);
                bullet.isFriendly = false;
                bullet.bulletType = enemyTypeIndex;
                bullet.speed = bulletSpeed;
                bullet.currentStoredDamage = damage;

                SfxManager.instance.PlaySoundEffect(0, 1, Random.Range(0.9f, 1.1f));

                shootCooldown = (enemyTypeIndex == 2) ? 0.25f : (enemyTypeIndex == 1 ? 5 * (1 / (bulletSpeed + 3)) + 0.75f : 1.5f);
            }

            hurtRenderer.enabled = hurtTimer > 0 ? true : false;
            hurtMask.sprite = spriteRenderer.sprite;

            animator.SetFloat("x", dir.x);
            animator.SetFloat("y", dir.y);
        }

    }

    private void GetHit(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            SfxManager.instance.PlaySoundEffect(7, 0.6f, Random.Range(0.9f, 1.1f));
            if (tutorialWall != null)
            {
                tutorialWall.gameObject.SetActive(false);
            }
            if (NightCycle.instance.isNight || Random.Range(0, 1) == 0 && canDropXp)
            {
                GameObject xp = Instantiate(xpPrefab, xpParent);
                xp.transform.position = transform.position;
                canDropXp = false;

            }
            Destroy(gameObject);


        }
        else
        {
            SfxManager.instance.PlaySoundEffect(2, 0.5f, Random.Range(0.9f, 1.1f));
        }
        hurtTimer = 0.1f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //hit by sword
        if (collision.gameObject.layer == 3)
        {
            GetHit(maxHealth / Mathf.Clamp(4 - (LevelUpScreen.instance.normalUpgradesGotten[0] * 0.4f), 1.1f, 4) + Sword.instance.swingDamage);
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
