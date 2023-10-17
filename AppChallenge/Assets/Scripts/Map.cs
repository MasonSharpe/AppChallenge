using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Map : MonoBehaviour {

    public static Map instance;

    public GameObject enemyHolder;
    public GameObject pickupHolder;


    private void Awake() {
        instance = this;
    }

    private void Start() {
        if (GameManager.savePosition.y == 5) {
            GameManager.SetSpawn();
            
            Fade.instance.exploreMusic.Play(0);

        }




        foreach (Enemy enemy in enemyHolder.GetComponentsInChildren<Enemy>()) {
            if (GameManager.saveEnemiesAlive.ToList().Find(element => element == enemy.ID) == 0) enemy.gameObject.SetActive(false);
        }
        foreach (Pickup pickup in pickupHolder.GetComponentsInChildren<Pickup>()) {
            if (GameManager.savePickupsLeft.ToList().Find(element => element == pickup.ID) == 0) pickup.gameObject.SetActive(false);
        }
    }
}
