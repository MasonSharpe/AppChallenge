using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class NightCycle : MonoBehaviour
{
    public static NightCycle instance;
    [SerializeField] private Image visual;
    public float nightLength;
    public bool isNight;
    private Tilemap ground;
    private Tilemap walls;
    public GameObject bulletHolder;
    public GameObject xpHolder;
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

            EndNight(true);
        }

    }

    public void SetToNight()
    {
        GameManager.SetSpawn(Player.instance.health, SceneManager.GetActiveScene().name,
            Player.instance.transform.position, Player.instance.level, Player.instance.xp);

        isNight = true;
        nightLength = 45 + 45 * currentNightIndex;
        EnemySpawner.instance.isSpawningEnemies = true;
        EnemySpawner.instance.levelTimer = 0;
        visual.enabled = true;
        Tilemap[] tilemaps = FindObjectsByType<Tilemap>(FindObjectsSortMode.InstanceID);
        ground = tilemaps[0];
        walls = tilemaps[1];
    }

    public void EndNight(bool victorious)
    {
        isNight = false;
        EnemySpawner.instance.isSpawningEnemies = false;
        EnemySpawner.instance.levelTimer = 0;
        visual.enabled = false;
        if (victorious)
        {
            GameManager.nightsBeaten[currentNightIndex] = true;
            GameManager.SetSpawn(Player.instance.health, SceneManager.GetActiveScene().name,
                Player.instance.transform.position, Player.instance.level, Player.instance.xp);
        }
        else
        {
            foreach (Transform bullet in bulletHolder.GetComponentsInChildren<Transform>())
            {
                if (bullet.name != "BulletHolder") Destroy(bullet.gameObject);
            }
            foreach (Transform xp in xpHolder.GetComponentsInChildren<Transform>())
            {
                if (xp.name != "XPHolder") Destroy(xp.gameObject);
            }
        }

        Transform[] enemies = EnemySpawner.instance.gameObject.GetComponentsInChildren<Transform>();
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].name != "EnemyHolder") Destroy(enemies[i].gameObject);
        }


    }
}
