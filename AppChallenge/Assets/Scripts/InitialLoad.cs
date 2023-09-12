using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialLoad : MonoBehaviour
{


    private void Start()
    {
        DontDestroyOnLoad(this);


        SceneManager.LoadScene("Area1");
    }


}
