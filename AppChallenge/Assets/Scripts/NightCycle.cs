using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NightCycle : MonoBehaviour
{
    public static NightCycle instance;
    [SerializeField] private Image visual;
    public float nightLength;
    public bool isNight;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        visual.fillAmount = 1 - EnemySpawner.instance.levelTimer / nightLength;
    }

    public void SetToNight()
    {
        isNight = true;
        nightLength = 30;
        EnemySpawner.instance.isSpawningEnemies = true;
        EnemySpawner.instance.levelTimer = 0;
        visual.enabled = true;
    }
}
