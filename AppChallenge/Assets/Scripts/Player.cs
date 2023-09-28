using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
	public float moveSpeed;
	[SerializeField] private Sword sword;
	[SerializeField] private SpriteRenderer swordRenderer;
	[SerializeField] private SpriteRenderer hurtRenderer;
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private SpriteMask hurtMask;
	[SerializeField] private Slider healthSlider;
	[SerializeField] private Slider xpSlider;
	[SerializeField] private TextMeshProUGUI levelText;
	[SerializeField] private TextMeshProUGUI armorText;
	[SerializeField] private GameObject loadedAreas;
	public Slider bossHealth;
	public BossEnemy boss;
	static public Player instance;
	public float health;
	public float maxHealth;
	public int armor;
	public int xp;
	public int level;
	public float invincPeriod;
	public float cameraShakeTimer;
	public Rigidbody2D rb;
	public float accelerationX;
	public float accelerationY;
	public bool canInteract;
	[SerializeField] Animator animator;

	//armor and health pickups, armor permanantly increases defense which decreases damage taken
	//enemies can get hit by the same swing multiple times
	// make the game better
	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{

		moveSpeed = 10;
		invincPeriod = 0;
		maxHealth = 10;
		armor = 0;
		health = maxHealth;
		level = 1;
		xp = 0;
		cameraShakeTimer = 0;
		canInteract = true;
	}




	private void Update()
	{
		invincPeriod -= Time.deltaTime;
		cameraShakeTimer -= Time.unscaledDeltaTime;


		float moveX = Input.GetAxisRaw("Horizontal");
		float moveY = Input.GetAxisRaw("Vertical");
		float acc = Time.deltaTime * (3 + (LevelUpScreen.instance.normalUpgradesGotten[7] * 2));
		float top = 1;


		accelerationX += moveX * acc;
		accelerationX = Mathf.Clamp(accelerationX, -top, top);
		accelerationY += moveY * acc;
		accelerationY = Mathf.Clamp(accelerationY, -top, top);

		animator.SetFloat("x", accelerationX);
		animator.SetFloat("y", accelerationY);

		string animation = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
		swordRenderer.sortingOrder = animation == "walkUp" || animation == "walkLeft" ? -2 : 0;
		animator.speed = 0.5f + (LevelUpScreen.instance.normalUpgradesGotten[6] * 0.25f);

		hurtRenderer.enabled = invincPeriod > 0 ? true : false;
		hurtMask.sprite = spriteRenderer.sprite;

		healthSlider.value = health;
		xpSlider.value = xp;
		if (NightCycle.instance.isBoss) bossHealth.value = boss.health;
		levelText.text = level.ToString();
		armorText.text = armor.ToString();

		moveSpeed = 10 + LevelUpScreen.instance.normalUpgradesGotten[6] * 2.5f;
		if (canInteract) rb.velocity = moveSpeed * new Vector3(accelerationX, accelerationY);

		Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		sword.transform.localRotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
		sword.transform.position = transform.position;

		if (Input.GetKeyDown(KeyCode.L))
        {
			LevelUp();
        }

		Camera.main.gameObject.transform.localPosition = new Vector3(0, 0, -10);
		if (cameraShakeTimer > 0)
        {
			Vector2 pos = Random.insideUnitCircle;
			Camera.main.gameObject.transform.localPosition = new Vector3(pos.x * 0.1f, pos.y * 0.1f, -10);
        }

        if (moveX == 0) {
			if (accelerationX > 0) {
				accelerationX = Mathf.Max(0, accelerationX - acc);
			}
            else if (accelerationX < 0) {
                accelerationX = Mathf.Min(0, accelerationX + acc);
            }
		}
        if (moveY == 0) {
            if (accelerationY > 0) {
                accelerationY = Mathf.Max(0, accelerationY - acc);
            } else if (accelerationY < 0) {
                accelerationY = Mathf.Min(0, accelerationY + acc);
            }
        }
    }

	private void LevelUp()
	{
		level++;
		xp = 0;
		xpSlider.maxValue = Mathf.Pow(level, 1.5f) + 5;
		LevelUpScreen.instance.Show();
	}

	private void GetHit(float damage, float invincPeriod)
    {
		//health -= Mathf.Clamp(damage * (1 - armor / 40f), 0.1f, 1000);
		this.invincPeriod = invincPeriod * (1 + LevelUpScreen.instance.normalUpgradesGotten[8] * 0.5f);

		if (health <= 0)
        {
			GameManager.Respawn();
        }
	}

	private void OnTriggerEnter2D(Collider2D collision)
    {
		//hit by bullet
        if (collision.gameObject.layer == 6)
        {
			if (!collision.GetComponent<Bullet>().isFriendly && invincPeriod < 0)
            {
				if (NightCycle.instance.isBoss)
				{
					GetHit(4, 0.4f);
				}
				else
				{
					GetHit(1f * (1 + GameManager.nightsBeaten.FindAll(h => h == true).Count) * (1 + (collision.GetComponent<Bullet>().bulletType * 0.5f)), 0.05f);
				}
				Destroy(collision.gameObject);
				cameraShakeTimer = 0.1f;

			}
		}




		//xp
		if (collision.gameObject.layer == 10)
        {
			xp += 1 + Mathf.RoundToInt(Mathf.Pow(GameManager.nightsBeaten.FindAll( h => h == true ).Count, 2));
			if (xp >= Mathf.RoundToInt(Mathf.Pow(level, 1.5f) + 5))
            {
				LevelUp();
            }

			Destroy(collision.gameObject);
		}
		//areaTrigger
		if (collision.gameObject.layer == 12)
        {
			AreaManager.instance.LoadTriggered(collision.GetComponent<AreaTrigger>().areaLoader);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
		//touch enemy
		if (collision.gameObject.layer == 7)
		{
			if (invincPeriod < 0)
			{
				if (NightCycle.instance.isBoss)
                {
					GetHit(5, 0.2f);
                }
                else
                {
					GetHit(0.5f * (1 + GameManager.nightsBeaten.FindAll(h => h == true).Count) * (1 + (collision.GetComponent<Enemy>().enemyTypeIndex * 0.5f)), 0.15f);
				}

			}
		}
		//touch pickup
		if (collision.gameObject.layer == 14)
		{
			if (invincPeriod < 0)
			{
				Pickup.PickupType pickupType = collision.GetComponent<Pickup>().type;

				if (pickupType == Pickup.PickupType.Health)
                {
					health = Mathf.Clamp(health + maxHealth / 6, 0, maxHealth);
                }
				else if (pickupType == Pickup.PickupType.Armor)
				{
					armor++;
				}

			}
			Destroy(collision.gameObject);
		}
	}
}
