using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingFountain : MonoBehaviour
{

    [SerializeField] GameObject text;
    private bool playerIsClose;
    public bool canHealFrom = false;

    private void Update()
    {
        playerIsClose = (Player.instance.transform.position - transform.position).magnitude < 5;
        text.SetActive(playerIsClose && !NightCycle.instance.isNight);
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose && !NightCycle.instance.isNight)
        {


            Player.instance.health = Player.instance.maxHealth;
            canHealFrom = false;

        }


    }

}
