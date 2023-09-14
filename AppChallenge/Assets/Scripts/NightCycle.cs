using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class NightCycle : MonoBehaviour
{
    public static NightCycle instance;
    [SerializeField] private Image visual;
    public float nightLength;
    public bool isNight;
    private Tilemap ground;
    private Tilemap walls;
    public int currentNightIndex = -1;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        float amount = EnemySpawner.instance.levelTimer / nightLength;
        visual.fillAmount = 1 - amount;
        float colorAmount =  (amount / 2) + 0.25f;
        if (isNight)
        {
            ground.color = new Color(colorAmount, colorAmount, colorAmount);
            walls.color = new Color(colorAmount, colorAmount, colorAmount);
        }

        if (amount >= 1 && isNight)
        {

            EndNight();
        }

    }

    public void SetToNight()
    {
        isNight = true;
        nightLength = 445 + 45 * currentNightIndex;
        EnemySpawner.instance.isSpawningEnemies = true;
        EnemySpawner.instance.levelTimer = 0;
        visual.enabled = true;
        Tilemap[] tilemaps = FindObjectsByType<Tilemap>(FindObjectsSortMode.InstanceID);
        ground = tilemaps[0];
        walls = tilemaps[1];
    }

    public void EndNight()
    {
        isNight = false;
        EnemySpawner.instance.isSpawningEnemies = false;
        visual.enabled = false;
        GameManager.nightsBeaten[currentNightIndex] = true;

        Enemy[] enemies = EnemySpawner.instance.gameObject.GetComponentsInChildren<Enemy>();
        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i].gameObject);
        }
    }
}
