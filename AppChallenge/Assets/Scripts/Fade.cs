using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour {

    public static Fade instance;
    public RawImage sprite;
    private float fadeTimer = -1;
    private bool isShowing;
    private float duration;

    public AudioSource exploreMusic;
    public AudioSource battleMusic;
    public AudioLowPassFilter exploreFilter;
    public AudioLowPassFilter battleFilter;

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
        if (fadeTimer > -1)
        {
            sprite.color = Helper.SetColorAlpla(Color.black, isShowing ? (duration - fadeTimer) / duration : fadeTimer / duration);
            if (!isShowing) exploreMusic.volume = fadeTimer / duration;
            if (isShowing || duration == 3) battleMusic.volume = (duration - fadeTimer) / duration;
        }

    }

    public void SetFilter(bool value) {
        float amount = value ? 1000 : 22000;
        if (NightCycle.instance.isNight) {
            battleFilter.cutoffFrequency = amount;
        } else {
            exploreFilter.cutoffFrequency = amount;
        }
    }
}