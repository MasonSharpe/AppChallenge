using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour {

    public static Fade instance;
    [SerializeField] private RawImage sprite;
    private float fadeTimer;
    private bool isShowing;
    private float duration;

    private void Awake() {
        instance = this;
    }

    public void Show(float duration = 0.2f) {
        isShowing = true;
        fadeTimer = duration;
        this.duration = duration;
    }

    public void Hide(float duration = 0.2f) {
        isShowing = false;
        fadeTimer = duration;
    }

    private void Update() {
        fadeTimer -= Time.deltaTime;
        sprite.color = Helper.SetColorAlpla(Color.black, isShowing ? (duration - fadeTimer) / duration : fadeTimer / duration);
    }
}