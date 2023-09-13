using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
	public float moveSpeed;
	[SerializeField] private GameObject sword;
	[SerializeField] private Slider healthSlider;
	[SerializeField] private Slider xpSlider;
	[SerializeField] private TextMeshProUGUI levelText;
	[SerializeField] private GameObject loadedAreas;
	static public Player instance;
	public float health;
	public int xp;
	public int level;
	public float invincPeriod;
	[SerializeField] Rigidbody2D rb;



	private void Awake()
    {
		instance = this;
    }

    private void Start()
	{

		moveSpeed = 10;
		invincPeriod = 0;
		health = 10;
		level = 1;
		xp = 0;
	}


	//TODO AT HOME: add bullet spread, fix health bars

	//make bullet blue

	private void Update()
	{
		invincPeriod -= Time.deltaTime;

		float moveX = Mathf.Clamp(Input.GetAxis("Horizontal") * (1 + LevelUpScreen.instance.normalUpgradesGotten[7] * 0.5f), -1, 1);
		float moveY = Mathf.Clamp(Input.GetAxis("Vertical") * (1 + LevelUpScreen.instance.normalUpgradesGotten[7] * 0.5f), -1, 1);

		healthSlider.value = health;
		xpSlider.value = xp;
		levelText.text = level.ToString();

		moveSpeed = 10 * (1 + LevelUpScreen.instance.normalUpgradesGotten[6] * 0.15f);
		rb.velocity = moveSpeed * new Vector3(moveX, moveY);

		Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		sword.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
		sword.transform.position = transform.position;

		if (Input.GetKeyDown(KeyCode.L))
        {
			LevelUp();
        }
	}

	private void LevelUp()
	{
		level++;
		xp = 0;
		xpSlider.maxValue = Mathf.Pow(level, 1.5f) + 5;
		LevelUpScreen.instance.Show();
	}

	private void OnTriggerEnter2D(Collider2D collision)
    {
		//hit by bullet
        if (collision.gameObject.layer == 6)
        {
			if (!collision.GetComponent<Bullet>().isFriendly && invincPeriod < 0)
            {
				health -= 0.5f;
				invincPeriod = 0.25f;
				Destroy(collision.gameObject);

			}
		}




		//xp
		if (collision.gameObject.layer == 10)
        {
			xp++;
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
				health -= 0.2f;
				invincPeriod = 0.05f;

			}
		}
	}
}
