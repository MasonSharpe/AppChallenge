using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    public static AreaManager instance;

    public Transform loadedAreas;
    public AreaLoader[] allAreas;

    public class AreaLoader
    {

        public GameObject[] scenesToLoad;
        public bool unloads;

    }

    private void Awake()
    {
        instance = this;
    }

    public void LoadTriggered(int areaLoaderIndex)
    {
        AreaLoader areaLoader = allAreas[areaLoaderIndex];

        foreach (GameObject gameObject in areaLoader.scenesToLoad)
        {
            if (areaLoader.unloads)
            {
                Destroy(gameObject);
            }
            else
            {
                Instantiate(gameObject, loadedAreas);
            }
        }
    }



}