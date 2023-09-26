using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class leveltimer : MonoBehaviour
{
    public float startingTime;

    public TextMeshProUGUI TimeText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        startingTime -= Time.deltaTime;

        TimeText.text = "" + Mathf.Round(startingTime);

        if (startingTime <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
