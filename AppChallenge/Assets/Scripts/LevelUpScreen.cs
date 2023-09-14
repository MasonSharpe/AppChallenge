using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpScreen : MonoBehaviour
{

    [SerializeField] private Canvas canvas;

    [System.Serializable]
    public class Upgrade
    {
        public string name;
        public string description;
        public int effectIndex;

    }

    public static LevelUpScreen instance;

    public Upgrade[] allNormalUpgrades;
    public Upgrade[] allSuperUpgrades;

    private List<Upgrade> superUpgradesYetToGet;
    //NORMAL UPGRADES: hitting enemies reduces cooldown, bigger sword, more sword damage, more parry damage, more sweetspot damage
    //parrying reduces cooldown, max speed increase, acceleration increase
    public int[] normalUpgradesGotten;
    private List<Upgrade> superUpgradesGotten;

    private Upgrade[] screenUpgrades;

    public Button[] buttons;
    public TextMeshProUGUI[] buttonNames;
    public TextMeshProUGUI[] buttonDescriptions;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        normalUpgradesGotten = new int[] { 0, 0, 0, 0, 0, 0, 0, 0 };
        canvas.enabled = false;
    }

    public void Show()
    {
        canvas.enabled = true;
        Time.timeScale = 0;

        screenUpgrades = new Upgrade[3];

        int index = Random.Range(0, allNormalUpgrades.Length);

        for (int i = 0; i < 3; i++)
        {
            screenUpgrades[i] = allNormalUpgrades[index];
            index = (int)Mathf.Repeat(index + 1, allNormalUpgrades.Length - 1);
        }


        for (int i = 0; i < 3; i++)
        {
            buttonNames[i].text = screenUpgrades[i].name;
            buttonDescriptions[i].text = screenUpgrades[i].description;
        }
    }

    public void PickChoice(int index)
    {
        normalUpgradesGotten[screenUpgrades[index].effectIndex] += 1;

        canvas.enabled = false;
        Time.timeScale = 1;
    }

}
