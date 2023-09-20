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
        levelTimer += Time.deltaTime;

        if (spawnTimer <= 0 && isSpawningEnemies)
        {
            float spawnAmount = Random.Range(2, 2 + (levelTimer * 0.02f));
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

                if (Random.Range(0, 10) > 8)
                {
                    Pickup pickup = Instantiate(pickupPrefab, transform);
                    pickup.transform.position = spawnPositions[location].position + (Vector3)(Random.insideUnitCircle * 1.5f);
                    pickup.type = Random.Range(0, 5) == 0 ? Pickup.PickupType.Armor : Pickup.PickupType.Health;
                }


            }


            spawnTimer = Random.Range(0.1f, 4 - (levelTimer * 0.01f)) + spawnAmount / 2;
        }
    }

}
