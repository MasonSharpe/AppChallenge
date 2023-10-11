using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sword : MonoBehaviour
{
    public float swordCooldown;
    private float swordActiveTime;
    public float bulletParrySpeed;
    public bool canDealDamage;
    public float swingDamage;
    public float parryDamage;
    [SerializeField] private BoxCollider2D bCollider;
    [SerializeField] private BoxCollider2D sweetBCollider;
    [SerializeField] private Image cooldownVisual;
    [SerializeField] private Animator animator;
    [SerializeField] public Transform rotatePoint;
    static public Sword instance;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        swordCooldown = 0;
        canDealDamage = true;
        swordActiveTime = 0;
        bCollider.enabled = false;
        sweetBCollider.enabled = false;

    }

    private void Update()
    {
        swordCooldown -= Time.deltaTime;
        swordActiveTime -= Time.deltaTime;

        cooldownVisual.fillAmount = 1 - swordCooldown / 1;
        cooldownVisual.enabled = swordCooldown > 0;

        bool swordActive = swordActiveTime > 0;
        bCollider.enabled = swordActive;
        sweetBCollider.enabled = swordActive;


        if (Input.GetMouseButton(0) && swordCooldown <= 0)
        {
            SfxManager.instance.PlaySoundEffect(5, 0.5f, Random.Range(0.9f, 1.1f));
            swordCooldown = 1f;
            swordActiveTime = 0.2f;
            canDealDamage = true;
            sweetBCollider.enabled = true;
            animator.Play("SwordSwing");
        }

        transform.localScale = (Vector3.one * 0.7f) + (0.15f * LevelUpScreen.instance.normalUpgradesGotten[2] * Vector3.one);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //parry bullet
        if (collision.gameObject.layer == 6)
        {
            SfxManager.instance.PlaySoundEffect(8, 0.7f, Random.Range(0.6f, 0.8f));
            canDealDamage = false;
            sweetBCollider.enabled = false;
            Bullet bullet = collision.GetComponent<Bullet>();
            bullet.isFriendly = true;
            bullet.currentStoredDamage = parryDamage * (1 + LevelUpScreen.instance.normalUpgradesGotten[3] * 0.4f);
            swordCooldown -= LevelUpScreen.instance.normalUpgradesGotten[5] * 0.02f;
            bullet.rb.velocity = (Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position)).normalized * bulletParrySpeed;
            if (bullet.hitSweetSpot == false) bullet.spriteRenderer.color = new Color(0.58f, 0.47f, 1, 0.83f);
            Player.instance.cameraShakeTimer = 0.1f;
        }
    }

}
