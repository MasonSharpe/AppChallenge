using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static List<bool> nightsBeaten = new();
    public static float saveHealth = 17;
    public static Vector3 savePosition = Vector3.zero;
    public static int saveLevel = 2;
    public static int saveXP = 1;
    public static int saveArmor = 2;
    public static int[] saveUpgrades = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };


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

        if (NightCycle.instance.isNight) NightCycle.instance.EndNight(false);
    }

    public static void SetSpawn(float health, Vector3 position, int level, int xp, int armor, int[] upgrades)
    {
        saveHealth = health;
        savePosition = position;
        saveLevel = level;
        saveXP = xp;
        saveArmor = armor;
        saveUpgrades = upgrades;
    }

    public static float ScaleFromNightsBeaten(float number, float exponent)
    {
        return number + Mathf.Pow(nightsBeaten.FindAll(h => h == true).Count, exponent);
    }
}