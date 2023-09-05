using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] Enemy enemyPrefab;
    private float spawnTimer;
    public float levelTimer;
    public static EnemySpawner instance;

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
            Vector3 range = new(Random.Range(-10, 10), Random.Range(-10, 10), 0);
            float spawnAmount = Random.Range(2, 4 + (levelTimer * 0.1f));

            for (int i = 0; i < spawnAmount; i++){

                Enemy enemy = Instantiate(enemyPrefab, transform);
                enemy.transform.position = range + new Vector3(Random.Range(1f, 4f), Random.Range(1f, 4f), 0);

            }


            spawnTimer = Random.Range(1, 3 - (levelTimer * 0.01f)) + spawnAmount / 2;
        }
    }

}
