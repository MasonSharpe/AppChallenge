using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float moveSpeed;
	[SerializeField] private GameObject sword;
	static public Player instance;
	public float health;

    private void Awake()
    {
		instance = this;
    }

    private void Start()
	{

		moveSpeed = 10;

	}

	private void Update()
	{
		float moveX = Input.GetAxis("Horizontal");
		float moveY = Input.GetAxis("Vertical");

		transform.position += Time.deltaTime * moveSpeed * new Vector3(moveX, moveY);

		Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		sword.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
			if (!collision.GetComponent<Bullet>().isFriendly)
            {
				print("owie");
            }
        }
    }
}
