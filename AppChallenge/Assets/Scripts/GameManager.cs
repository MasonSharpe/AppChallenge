using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static List<bool> nightsBeaten = new();
    public static float saveHealth = 30;
    public static Vector3 savePosition = Vector3.zero;
    public static int saveLevel = 1;
    public static int saveXP = 0;
    public static int saveArmor = 0;
    public static int[] saveUpgrades = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public static int[] savePickupsLeft = new int[0];
    public static int[] saveEnemiesAlive = new int[0];
    public static List<bool> saveNightsBeaten = new();


    private void Start()
    {
        for (int i = 0; i < 4; i++) nightsBeaten.Add(false);
        for (int i = 0; i < 4; i++) saveNightsBeaten.Add(false);
        savePosition = new Vector3(8, 5, 0);

    }
    public static void Respawn() {
        SceneManager.LoadScene("_FullMap");
        Player.instance.gameObject.transform.position = savePosition;
        Player.instance.xp = saveXP;
        Player.instance.level = saveLevel;
        Player.instance.health = saveHealth;
        Player.instance.armor = saveArmor;
        LevelUpScreen.instance.normalUpgradesGotten = (int[])saveUpgrades.Clone();



        if (Player.instance.health > Player.instance.maxHealth) Player.instance.health = Player.instance.maxHealth;
        if (NightCycle.instance.isNight) NightCycle.instance.EndNight(false);

        nightsBeaten = new List<bool>(saveNightsBeaten);

        NightCycle.instance.dayText.text = "Day " + (nightsBeaten.FindAll(h => h == true).Count + 1);
    }

    public static void SetSpawn()
    {
        saveHealth = Player.instance.health;
        savePosition = Player.instance.gameObject.transform.position;
        saveLevel = Player.instance.level;
        saveXP = Player.instance.xp;
        saveArmor = Player.instance.armor;
        saveUpgrades = (int[])LevelUpScreen.instance.normalUpgradesGotten.Clone();
        saveNightsBeaten = new List<bool>(nightsBeaten);

        Enemy[] enemies = Map.instance.enemyHolder.GetComponentsInChildren<Enemy>();
        saveEnemiesAlive = new int[enemies.Length];
        for (int i = 0; i < enemies.Length; i++) {
            saveEnemiesAlive[i] = enemies[i].ID;
        }

        Pickup[] pickups = Map.instance.pickupHolder.GetComponentsInChildren<Pickup>();
        savePickupsLeft = new int[pickups.Length];
        for (int i = 0; i < pickups.Length; i++) {
            savePickupsLeft[i] = pickups[i].ID;
        }
    }

    public static float ScaleFromNightsBeaten(float number, float exponent)
    {
        return number + Mathf.Pow(nightsBeaten.FindAll(h => h == true).Count, exponent);
    }
}