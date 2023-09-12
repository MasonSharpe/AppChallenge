using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartNightNPC : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI text;
    private bool playerIsClose;
    public int nightIndex;

    private void Update()
    {
        playerIsClose = (Player.instance.transform.position - transform.position).magnitude < 5;
        text.enabled = playerIsClose;
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            NightCycle.instance.currentNightIndex = nightIndex;

            NightCycle.instance.SetToNight();
        }

        enabled = GameManager.nightsBeaten[nightIndex];

    }

}
