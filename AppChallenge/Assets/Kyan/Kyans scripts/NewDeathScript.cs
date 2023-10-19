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
    public Canvas canvas;

    public static NewDeathScript instance;


    private void Awake() {
        instance = this;
    }

    void Start() {

        canvas.enabled = false;
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

        canvas.enabled = true;
    }

    public void Resume() {
        Time.timeScale = 1;
        Fade.instance.SetFilter(false);

        canvas.enabled = false;
    }

    public void Restart() {

        Time.timeScale = 1;
        NightCycle.instance.EndNight(false);
        Fade.instance.SetFilter(false);

        canvas.enabled = false;
        GameManager.Respawn();
    }
    public void LoadMainMenu() {
        NightCycle.instance.EndNight(false);
        Fade.instance.SetFilter(false);
        canvas.enabled = false;
        Time.timeScale = 1;

        SceneManager.LoadScene("MainMenu");
    }


}
