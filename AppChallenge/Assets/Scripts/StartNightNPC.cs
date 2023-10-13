using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Tilemaps;

public class StartNightNPC : MonoBehaviour
{

    [SerializeField] GameObject text;
    private bool playerIsClose;
    public int nightIndex;
    [SerializeField] GameObject spawnPositions;
    [SerializeField] private BossEnemy bossPrefab = null;
    [SerializeField] private HealingFountain fountain;
    [SerializeField] private TilemapCollider2D border;

    private void Start()
    {
        border.enabled = false;
        int nights = GameManager.nightsBeaten.FindAll(h => h == true).Count;
        if (nightIndex == 3)
        {

            if (nights == 3)
            {
                text.GetComponentInChildren<TextMeshProUGUI>().text = "A VERY powerful enemy lies ahead... Press E to continue.";
            }
            else
            {
                text.GetComponentInChildren<TextMeshProUGUI>().text = "A VERY powerful enemy lies ahead... There's still places you haven't been to yet! You may press E continue if you like, but it might be wise to come back later...";
            }
        }
        else
        {
            text.GetComponentInChildren<TextMeshProUGUI>().text = "Press E to save and start the night.";
        }


    }




    private void Update()
    {
        bool isNight = NightCycle.instance.isNight;
        playerIsClose = (Player.instance.transform.position - transform.position).magnitude < 5;
        if (!GameManager.nightsBeaten[nightIndex]) text.SetActive(playerIsClose && !isNight);
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose && !(GameManager.nightsBeaten[nightIndex]))
        {
            NightCycle.instance.border = border;
            border.enabled = true;
            SfxManager.instance.PlaySoundEffect(4, 1);
            NightCycle.instance.fountain = fountain;
            EnemySpawner.instance.spawnPositions = spawnPositions.GetComponentsInChildren<Transform>();
            if (bossPrefab != null) NightCycle.instance.isBoss = true;
            NightCycle.instance.SetToNight();

            text.SetActive(false);

            if (bossPrefab != null)
            {
               BossEnemy boss = Instantiate(bossPrefab, spawnPositions.transform);
                boss.transform.position = spawnPositions.GetComponentsInChildren<Transform>()[1].position;

                Player.instance.boss = boss;
                boss.enabled = false;
                TimerManager.CreateTimer(1.5f, () =>
                {
                    boss.enabled = true;
                    Player.instance.bossHealth.gameObject.SetActive(true);
                });
            }

        }

    }

}
