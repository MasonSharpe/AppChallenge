using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartNightNPC : MonoBehaviour
{

    [SerializeField] GameObject text;
    private bool playerIsClose;
    public int nightIndex;
    [SerializeField] GameObject spawnPositions;

    private void Start()
    {
        if (GameManager.nightsBeaten.FindAll(h => h == true).Count != 0)
        {
            text.GetComponentInChildren<TextMeshProUGUI>().text = "Press E to start the night.";
        }
    }

    private void Update()
    {
        playerIsClose = (Player.instance.transform.position - transform.position).magnitude < 5;
        text.SetActive(playerIsClose && !NightCycle.instance.isNight);
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            NightCycle.instance.currentNightIndex = nightIndex;
            EnemySpawner.instance.spawnPositions = spawnPositions.GetComponentsInChildren<Transform>();


            NightCycle.instance.SetToNight();

            text.SetActive(false);

        }

        enabled = !GameManager.nightsBeaten[nightIndex] && !NightCycle.instance.isNight;

    }

}
