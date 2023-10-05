using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialLoad : MonoBehaviour
{


    private void Start()
    {
        DontDestroyOnLoad(this);


        SceneManager.LoadScene("_FullMap");

        GameManager.SetSpawn(Player.instance.health, new Vector3(125, 75, 0), Player.instance.level, Player.instance.xp, Player.instance.armor);
    }


}
