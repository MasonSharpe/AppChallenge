using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] Enemy enemyPrefab;
    [SerializeField] Pickup pickupPrefab;
    private float spawnTimer;
    public float levelTimer;
    public static EnemySpawner instance;
    public Transform[] spawnPositions;
    public Transform bulletParent;
    public Transform xpParent;
    public bool isSpawningEnemies = false;
    private bool spawnAnArmor = false;
    private int spawnPickupCooldown;
    public float timeStrength;

    private void Awake()
    {
        instance = this;
    }

    //ude nashi
    private void Start()
    {
        spawnTimer = 2;
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (!NightCycle.instance.isBoss) levelTimer += Time.deltaTime;
        timeStrength = levelTimer / NightCycle.instance.nightLength;

        if (spawnTimer <= 0 && isSpawningEnemies)
        {
            float spawnAmount = Random.Range(2, 2 + (5 * timeStrength));
            int location = Random.Range(0, spawnPositions.Length);
            for (int i = 0; i < 10; i++)
            {
                location = Random.Range(0, spawnPositions.Length);
                if ((Player.instance.transform.position - spawnPositions[location].position).magnitude > 10) break;

            }


            for (int i = 0; i < spawnAmount; i++){

                Enemy enemy = Instantiate(enemyPrefab, transform);
                enemy.bulletParent = bulletParent;
                enemy.xpParent = xpParent;
                enemy.transform.position = spawnPositions[location].position + (Vector3)(Random.insideUnitCircle * 1.5f);
                enemy.isNighttimeEnemy = true;

            }
            if (spawnPickupCooldown <= 0)
            {
                Pickup pickup = Instantiate(pickupPrefab, transform);
                pickup.transform.position = spawnPositions[location].position + (Vector3)(Random.insideUnitCircle * 1.5f);
                if (spawnAnArmor)
                {
                    pickup.type = Pickup.PickupType.Armor;
                    spawnAnArmor = false;
                }
                else
                {
                    pickup.type = Pickup.PickupType.Health;
                    spawnAnArmor = true;
                }
                spawnPickupCooldown = (int)GameManager.ScaleFromNightsBeaten(1, 1.5f);
            }
            spawnPickupCooldown--;


            spawnTimer = Random.Range(4 - (3 * timeStrength), 7 - (3 * timeStrength)) + spawnAmount / 2;
        }
    }

}
