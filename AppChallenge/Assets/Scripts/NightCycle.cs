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
    [SerializeField] private Image visual2;
    public float nightLength;
    public bool isNight;
    public bool isBoss = false;
    public GameObject bulletHolder;
    public GameObject xpHolder;
    public int currentNightIndex = 0;
    public HealingFountain fountain;




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
            for (int i = 0; i < Tilemaps.tilemaps.Length; i++)
            {
                if (Tilemaps.tilemaps[i] == null)
                {
                    print(i);
                }
                else
                {
                    Tilemaps.tilemaps[i].color = new Color(colorAmount, colorAmount, colorAmount);

                }

            }


        }

        if (amount >= 1 && isNight)
        {

            EndNight(true);
        }

    }

    public void SetToNight()
    {
        Fade.instance.battleMusic.Play(0);
        Fade.instance.Show(0.75f);
        Player.instance.canInteract = false;
        Player.instance.rb.velocity = Vector3.zero;
        TimerManager.CreateTimer(1.5f, () =>
        {
            Player.instance.health = Player.instance.maxHealth;

            GameManager.SetSpawn();
            Player.instance.canInteract = true;

            isNight = true;
            if (!isBoss)
            {
                nightLength = 50 + 50 * currentNightIndex;
                EnemySpawner.instance.isSpawningEnemies = true;
                visual.enabled = true;
                visual2.enabled = true;
            }

            EnemySpawner.instance.levelTimer = 0;
            
            Fade.instance.Hide(0.5f);
        });


    }

    public void EndNight(bool victorious)
    {
        isNight = false;
        EnemySpawner.instance.isSpawningEnemies = false;
        EnemySpawner.instance.levelTimer = 0;
        fountain.canHealFrom = true;
        visual.enabled = false;
        visual2.enabled = false;
        if (victorious)
        {
            instance.currentNightIndex++;
            GameManager.nightsBeaten[currentNightIndex] = true;
            GameManager.SetSpawn();
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

        Fade.instance.exploreMusic.time = 0;
        TimerManager.CreateTimer(2, () => { Fade.instance.battleMusic.Stop(); }, () =>
        {
            Fade.instance.battleMusic.volume -= Time.deltaTime / 2;
            Fade.instance.exploreMusic.volume += Time.deltaTime / 2;
        }, "", true);

    }
}
