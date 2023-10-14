using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour{

    [SerializeField] private float moveSpeed = 5;
    private float shootCooldown = 0;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] private SpriteRenderer hurtRenderer;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private SpriteMask hurtMask;
    [SerializeField] private Animator animator;
    private float phaseRotation = 0;

    private float hurtTimer = 0;
    public float maxHealth;
    public float health;
    public Transform bulletParent;
    public Transform xpParent;
    public int phase = 1;
    private bool canAttack;
    private float alpha;
    private float alphaDirection;

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
        alpha = 1;
        canAttack = true;
    }

    private void Update()
    {

        shootCooldown -= Time.deltaTime;
        hurtTimer -= Time.deltaTime;

        alpha += alphaDirection * Time.deltaTime;
        if (alpha < 0)
        {
            alphaDirection = 1;
            alpha = 0.01f;
            if (phase == 2 || phase == 5)
            {
                transform.localPosition = new Vector3(23, 0, 0);
                canAttack = true;
            } else if (phase == 3)
            {
                transform.localPosition = new Vector3(20, 0, 0);
                canAttack = true;
            }


        }
        else if (alpha > 1)
        {


            alphaDirection = 0;
        }
        spriteRenderer.color = Helper.SetColorAlpla(Color.white, alpha);


        canAttack = (spriteRenderer.color.a >= 1);


        Vector2 dir = (Player.instance.transform.position - transform.position).normalized;


        if (shootCooldown <= 0 && canAttack)
        {
            switch (phase)
            {
                case 2:
                    for (int i = 0; i < 18; i++)
                    {
                        SpawnBullet(
                            (Quaternion.Euler(0, 0, (i * 20) + phaseRotation) * Vector2.one).normalized,
                            transform.position,
                            0.75f,
                            10,
                            5
                            );
                    }
                    SpawnBullet(
                        (Player.instance.transform.position - transform.position).normalized,
                        transform.position,
                        0.75f,
                        12,
                        1
                        );

                    phaseRotation += 6;
                    break;
                case 3:
                    SpawnBullet(
                        (Player.instance.transform.position - transform.position).normalized,
                        transform.position,
                        0.1f,
                        10,
                        1
                        );


                    break;
                case 4:



                    for (int i = 0; i < 20; i++)
                    {
                        Vector2 shootDir = Vector2.zero;
                        float size = 30;
                        if (Random.Range(0, 2) == 1) shootDir.x = (Random.Range(0, 2) * 2) - 1; else shootDir.y = (Random.Range(0, 2) * 2) - 1;
                        SpawnBullet(
                            shootDir,
                            new Vector3(-90, 172, 0) + new Vector3(-shootDir.x * size + Random.Range(-Mathf.Abs(shootDir.y) * size, Mathf.Abs(shootDir.y) * size), -shootDir.y * size + Random.Range(-Mathf.Abs(shootDir.x) * size, Mathf.Abs(shootDir.x) * size), 0),
                            1f,
                            9 + Random.Range(-2, 3),
                            2
                            );
                    }
                    
                    
                    break;
                case 5:
                    float distance = Random.Range(6, 10);

                    for (int i = 0; i < 6; i++)
                    {
                        Vector3 pos = Player.instance.transform.position + (Quaternion.Euler(0, 0, i * 60) * Vector2.one * distance);
                        dir = ((Vector2)(Player.instance.transform.position - pos) + Player.instance.rb.velocity).normalized;
                        SpawnBullet(
                            dir,
                            pos,
                            0.75f,
                            8 + Random.Range(-4, 1),
                            4
                            );
                    }



                    break;
                default:

                    rb.velocity = moveSpeed * dir;

                    SpawnBullet(
                        ((Vector2)(Player.instance.transform.position - transform.position) + Player.instance.rb.velocity).normalized,
                        transform.position,
                        0.2f,
                        20,
                        4
                        ); break;
            }



        }

        if (phase == 3)
        {
            transform.localPosition = Quaternion.Euler(0, 0, phaseRotation) * Vector2.one * 17 + new Vector3(21, 0, 0);
            phaseRotation += (40 + Player.instance.rb.velocity.magnitude * 1.5f) * Time.deltaTime;
        }else if (phase == 4)
        {
            rb.velocity = moveSpeed / 2 * dir;

        }


        if (health / maxHealth <= 1 - (phase * 0.2f)) TriggerPhase(phase + 1);


        hurtRenderer.enabled = hurtTimer > 0;
        hurtMask.sprite = spriteRenderer.sprite;

        animator.SetFloat("x", dir.x);
        animator.SetFloat("y", dir.y);


    }

    private void TriggerPhase(int phase)
    {
        if (phase == 2)
        {
            alphaDirection = -1;
            canAttack = false;
            rb.velocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else if (phase == 3)
        {
            alphaDirection = -1;
            canAttack = false;
        }
        else if (phase == 4)
        {
            canAttack = true;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else if (phase == 5)
        {
            alphaDirection = -1;
            canAttack = false;
            rb.velocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }



            this.phase = phase;
    }

    private void SpawnBullet(Vector2 dir, Vector3 position, float cooldown, float bulletSpeed, int bulletType) {

        Bullet bullet = Instantiate(bulletPrefab, bulletParent).GetComponent<Bullet>();
        bullet.transform.position = position;


        bullet.GetComponent<Rigidbody2D>().velocity = bulletSpeed * dir;

        if (phase != 2 && phase != 4) SfxManager.instance.PlaySoundEffect(0, 0.6f, Random.Range(0.9f, 1.1f));

        bullet.isFriendly = false;
        bullet.bulletType = bulletType;
        bullet.speed = bulletSpeed;


        shootCooldown = cooldown;
    }

    private void GetHit(float damage)
    {
        health -= damage;
        SfxManager.instance.PlaySoundEffect(7, 0.6f, Random.Range(0.9f, 1.1f));
        if (health <= 0)
        {
            Destroy(gameObject);
            SfxManager.instance.PlaySoundEffect(2, 0.5f, Random.Range(0.9f, 1.1f));
        }
        hurtTimer = 0.1f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //hit by sword
        if (collision.gameObject.layer == 3)
        {
            GetHit(maxHealth / Mathf.Clamp(1500 - (LevelUpScreen.instance.normalUpgradesGotten[0] * 100f), 50, 1500) + Sword.instance.swingDamage + 1);
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
