using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialSave : MonoBehaviour
{

    private bool playerIsClose;
    public GameObject panel;





    private void Update()
    {
        playerIsClose = (Player.instance.transform.position - transform.position).magnitude < 5;
        panel.SetActive(playerIsClose);
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
        {
            SfxManager.instance.PlaySoundEffect(4, 1);
            GameManager.SetSpawn(Player.instance.health,
            Player.instance.transform.position, Player.instance.level, Player.instance.xp, Player.instance.armor, LevelUpScreen.instance.normalUpgradesGotten);

        }


    }

}
