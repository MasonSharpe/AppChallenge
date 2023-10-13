using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    public static Map instance;

    public GameObject enemyHolder;
    public GameObject pickupHolder;


    private void Awake() {
        instance = this;
    }

    private void Start() {
        if (GameManager.saveLevel == 1) {

            GameManager.SetSpawn();
            Fade.instance.exploreMusic.Play(0);

        }
    }
}
