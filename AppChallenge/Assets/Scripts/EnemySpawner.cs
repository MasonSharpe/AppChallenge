using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] Enemy enemyPrefab;
    private float spawnTimer;
    public float levelTimer;
    public static EnemySpawner instance;
    public Transform[] spawnPositions; 

    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        spawnTimer = 2;
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;
        levelTimer -= Time.deltaTime;

        if (spawnTimer <= 0)
        {
            float spawnAmount = Random.Range(1, 2 + (levelTimer * 0.1f));
            int location = Random.Range(0, spawnPositions.Length);


            for (int i = 0; i < spawnAmount; i++){

                Enemy enemy = Instantiate(enemyPrefab, transform);
                for (int e = 0; e < 10; e++)
                {
                    enemy.transform.position = spawnPositions[location].position + (Vector3)(Random.insideUnitCircle * 2);
                    if ((Player.instance.transform.position - enemy.transform.position).magnitude > 5) break;
                }

            }


            spawnTimer = Random.Range(1, 3 - (levelTimer * 0.01f)) + spawnAmount / 2;
        }
    }

}
