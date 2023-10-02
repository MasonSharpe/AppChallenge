using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Restart()
    {
        //whatever mason wants for the restart, function
    }
    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
