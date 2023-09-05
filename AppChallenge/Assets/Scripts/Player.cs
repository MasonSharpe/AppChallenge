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
	static public Player instance;
	public float health;
	public int xp;
	public int level;
	public float invincPeriod;

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

	private void Update()
	{
		invincPeriod -= Time.deltaTime;

		float moveX = Input.GetAxis("Horizontal");
		float moveY = Input.GetAxis("Vertical");

		healthSlider.value = health;
		xpSlider.value = xp;
		levelText.text = level.ToString();

		transform.position += Time.deltaTime * moveSpeed * new Vector3(moveX, moveY);

		Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		sword.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

		if (Input.GetKeyDown(KeyCode.R))
        {
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
		//hit by bullet
        if (collision.gameObject.layer == 6)
        {
			if (!collision.GetComponent<Bullet>().isFriendly && invincPeriod < 0)
            {
				health -= 1;
				invincPeriod = 0.5f;
				Destroy(collision.gameObject);
            }
        }
		//xp
		if(collision.gameObject.layer == 10)
        {
			xp++;
			if (xp >= level * 3)
            {
				level++;
				xp = 0;
				xpSlider.maxValue = level * level;
            }

			Destroy(collision.gameObject);
		}
    }
}
