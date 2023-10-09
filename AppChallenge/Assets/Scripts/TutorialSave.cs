using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialSave : MonoBehaviour
{

    private bool playerIsClose;






    private void Update()
    {
        playerIsClose = (Player.instance.transform.position - transform.position).magnitude < 5;
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {

            GameManager.SetSpawn(Player.instance.health,
            Player.instance.transform.position, Player.instance.level, Player.instance.xp, Player.instance.armor, LevelUpScreen.instance.normalUpgradesGotten);

        }


    }

}
