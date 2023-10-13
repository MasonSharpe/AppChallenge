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
    public static Pickup[] savePickupsObtained = new Pickup[5];
    public static Enemy[] saveEnemiesKilled = new Enemy[5];


    private void Start()
    {
        for (int i = 0; i < 4; i++) nightsBeaten.Add(false);


    }
    public static void Respawn()
    {
        SceneManager.LoadScene("_FullMap");
        Player.instance.gameObject.transform.position = savePosition;
        Player.instance.xp = saveXP;
        Player.instance.level = saveLevel;
        Player.instance.health = saveHealth;
        Player.instance.armor = saveArmor;
        LevelUpScreen.instance.normalUpgradesGotten = saveUpgrades;

        foreach (Enemy enemy in Map.instance.enemyHolder.GetComponentsInChildren<Enemy>()) {
            if (saveEnemiesKilled.Contains(enemy)) enemy.enabled = false;
        }
        foreach (Pickup pickup in Map.instance.pickupHolder.GetComponentsInChildren<Pickup>()) {
            if (savePickupsObtained.Contains(pickup)) pickup.enabled = false;
        }

        if (Player.instance.health > Player.instance.maxHealth) Player.instance.health = Player.instance.maxHealth;
        if (NightCycle.instance.isNight) NightCycle.instance.EndNight(false);
    }

    public static void SetSpawn()
    {
        saveHealth = Player.instance.health;
        savePosition = Player.instance.gameObject.transform.position;
        saveLevel = Player.instance.level;
        saveXP = Player.instance.xp;
        saveArmor = Player.instance.armor;
        saveUpgrades = LevelUpScreen.instance.normalUpgradesGotten;
        saveEnemiesKilled = Map.instance.enemyHolder.GetComponentsInChildren<Enemy>();
        savePickupsObtained = Map.instance.pickupHolder.GetComponentsInChildren<Pickup>();
    }

    public static float ScaleFromNightsBeaten(float number, float exponent)
    {
        return number + Mathf.Pow(nightsBeaten.FindAll(h => h == true).Count, exponent);
    }
}