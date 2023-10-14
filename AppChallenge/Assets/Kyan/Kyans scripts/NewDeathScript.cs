using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewDeathScript : MonoBehaviour {

    private float fadeTimer;
    private float fadeDirection;
    private Image[] sprites;
    private Text[] text;

    public static NewDeathScript instance;


    private void Awake() {
        instance = this;
    }

    void Start() {

        GetComponent<Canvas>().enabled = false;
        fadeTimer = 0;
        fadeDirection = 0;
        sprites = GetComponentsInChildren<Image>();
        text = GetComponentsInChildren<Text>();
    }

    void Update() {
        fadeTimer += Time.unscaledDeltaTime * fadeDirection;
        if (fadeTimer > 0 && fadeTimer < 0.6f) {
            foreach (var sprite in sprites) {
                sprite.color = Helper.SetColorAlpla(sprite.color, fadeTimer);
            }
            foreach (var text in text) {
                text.color = Helper.SetColorAlpla(text.color, fadeTimer);
            }
        }

    }

    public void Show() {
        Time.timeScale = 0;
        fadeTimer = 0;
        fadeDirection = 3f;
        Fade.instance.SetFilter(true);

        foreach (var sprite in sprites) {
            sprite.color = Helper.SetColorAlpla(sprite.color, 0);
        }
        foreach (var text in text) {
            text.color = Helper.SetColorAlpla(text.color, fadeTimer);
        }

        GetComponent<Canvas>().enabled = true;
    }

    public void Resume() {
        Time.timeScale = 1;
        Fade.instance.SetFilter(false);

        GetComponent<Canvas>().enabled = false;
    }

    public void Restart() {

        Time.timeScale = 1;
        Fade.instance.SetFilter(false);

        GetComponent<Canvas>().enabled = false;
        GameManager.Respawn();
    }
    public void LoadMainMenu() {
        Fade.instance.SetFilter(false);

        SceneManager.LoadScene("MainMenu");
    }


}
