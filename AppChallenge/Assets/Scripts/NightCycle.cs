using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NightCycle : MonoBehaviour
{

    [SerializeField] private Image visual;
    public float nightLength;

    private void Update()
    {
        visual.fillAmount = 1 - EnemySpawner.instance.levelTimer / nightLength;
    }
}
