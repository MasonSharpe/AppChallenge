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

        GameManager.SetSpawn(Player.instance.health, new Vector3(0, 0, 0), 1, 0, 0, new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 });
    }


}
