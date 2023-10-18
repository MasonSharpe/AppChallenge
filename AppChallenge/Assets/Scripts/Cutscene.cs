using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
   // Autoload autoload;
    public TextMeshProUGUI text;
    public string[] lines = { "‘Tis time to awaken from thy finite slumber, my child.",
        "Dust off thine GPU and warm up thine RAM, for ye gods, digital excellence o’er the lambs of mechanical fruitlessness, fated and fortold unto me, the bell struck naught for you, hellfire and soulflame alight.",
        "Go forth. What belongs to thou, rightfully take. No more of this age of darkness, no more of this age of fallen legends, lost miracles, and unrighteous heroes.",
        "Take up your blade. Strike down those whom fleer and scorn at our solemnity, who blasphemes and spites our names. In the holy name of Makeli, go thither, endure, and become CEO."
    };
    string currentLine = "";
    int lineIndex = 0;
    int charIndex = 0;
    public AudioClip music;
    public AudioSource source;
    bool doStuff;
    void Start()
    {
        text.text = "";
       source.clip = music;
        source.Play(0);
        doStuff = true;
    }



    // Update is called once per frame
    void Update()
    {
        if (doStuff)
        {
            if (text.text.Length < lines[lineIndex].Length) {
                text.text = currentLine + lines[lineIndex][charIndex];
                currentLine = text.text;
                charIndex++;
            }
            if (Input.GetKeyDown(KeyCode.E)) {
                print(lineIndex);
                if (lineIndex > lines.Length - 2) {
                    print("assd");
                    doStuff = false;
                    TimerManager.CreateTimer(1, () => { SceneManager.LoadScene("MainMenu"); }, () => { source.volume -= Time.deltaTime; }, "", true);
                }
                lineIndex++;
                charIndex = 0;
                currentLine = "";
                text.text = "";
            }
        }

    }
}
