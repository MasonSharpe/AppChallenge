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
    public bool isBoss = false;
    public GameObject bulletHolder;
    public GameObject xpHolder;
    public int currentNightIndex = -1;
    private Tilemap[] tilemaps;

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
            foreach (Tilemap tilemap in tilemaps)
            {
                tilemap.color = new Color(colorAmount, colorAmount, colorAmount);

            }


        }

        if (amount >= 1 && isNight)
        {

            EndNight(true);
        }

    }

    public void SetToNight()
    {
        Fade.instance.Show(0.75f);
        TimerManager.CreateTimer(1.5f, () =>
        {
            GameManager.SetSpawn(Player.instance.health, SceneManager.GetActiveScene().name,
            Player.instance.transform.position, Player.instance.level, Player.instance.xp, Player.instance.armor);

            isNight = true;
            if (!isBoss)
            {
                nightLength = 45 + 45 * currentNightIndex;
                EnemySpawner.instance.isSpawningEnemies = true;
                visual.enabled = true;
            }

            EnemySpawner.instance.levelTimer = 0;
            tilemaps = FindObjectsByType<Tilemap>(FindObjectsSortMode.None);
            Fade.instance.Hide(0.5f);
        });


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
                Player.instance.transform.position, Player.instance.level, Player.instance.xp, Player.instance.armor);
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

        Player.instance.bossHealth.gameObject.SetActive(false);
        Transform[] enemies = EnemySpawner.instance.gameObject.GetComponentsInChildren<Transform>();
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].name != "EnemyHolder") Destroy(enemies[i].gameObject);
        }


    }
}
