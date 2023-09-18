using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool[] nightsBeaten = { false, false, false };
    public static float saveHealth = 17;
    public static string saveLocation = "Area1";
    public static Vector3 savePosition = Vector3.zero;
    public static int saveLevel = 2;
    public static int saveXP = 1;

    public static void Respawn()
    {
        SceneManager.LoadScene(saveLocation);
        Player.instance.gameObject.transform.position = savePosition;
        Player.instance.xp = saveXP;
        Player.instance.level = saveLevel;
        Player.instance.health = saveHealth;

        if (NightCycle.instance.isNight) NightCycle.instance.EndNight(false);
    }

    public static void SetSpawn(float health, string location, Vector3 position, int level, int xp)
    {
        saveHealth = health;
        saveLocation = location;
        savePosition = position;
        saveLevel = level;
        saveXP = xp;
    }
}