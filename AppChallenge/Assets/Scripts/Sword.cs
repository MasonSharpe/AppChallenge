using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private float swordCooldown;
    private float swordActiveTime;
    [SerializeField] private float bulletParrySpeed;
    public bool canDealDamage;
    public float swingDamage;
    public float parryDamage;
    [SerializeField] private GameObject visual;
    [SerializeField] private BoxCollider2D bCollider;
    [SerializeField] private BoxCollider2D sweetBCollider;
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
        visual.SetActive(false);
        bCollider.enabled = false;
        sweetBCollider.enabled = false;

    }

    private void Update()
    {
        swordCooldown -= Time.deltaTime;
        swordActiveTime -= Time.deltaTime;

        bool swordActive = swordActiveTime > 0;
        visual.SetActive(swordActive);
        bCollider.enabled = swordActive;
        sweetBCollider.enabled = swordActive;

        if (Input.GetKey(KeyCode.Space) && swordCooldown <= 0)
        {

            swordCooldown = 1f;
            swordActiveTime = 0.2f;
            canDealDamage = true;
            sweetBCollider.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //parry bullet
        if (collision.gameObject.layer == 6)
        {
            canDealDamage = false;
            sweetBCollider.enabled = false;
            collision.GetComponent<Bullet>().isFriendly = true;
            collision.GetComponent<Bullet>().currentStoredDamage = parryDamage;
            collision.GetComponent<Rigidbody2D>().velocity = (Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position)).normalized * bulletParrySpeed;
        }
    }

}
