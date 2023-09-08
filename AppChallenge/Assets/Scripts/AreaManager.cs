using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaManager : MonoBehaviour
{
    public static AreaManager instance;

    public Transform loadedAreas;

    [System.Serializable]
    public class AreaLoader
    {

        public string sceneToLoad;
        ///public bool unloads;
       /// public int id;
       /// public GameObject itself;

    }

    private void Awake()
    {
        instance = this;
    }

    public void LoadTriggered(AreaLoader areaLoader)
    {
        SceneManager.LoadScene(areaLoader.sceneToLoad);
    /*foreach (GameObject gameObject in areaLoader.scenesToLoad)
        {

            AreaLoader areaExists = CheckIfAreaExists(areaLoader);

            if (areaLoader.unloads)
            {
                if (areaExists != null) Destroy(areaExists.itself);
            }
            else
            {
                if (areaExists == null) Instantiate(gameObject, loadedAreas);
            }
        } */
    }

    /*public AreaLoader CheckIfAreaExists(AreaLoader areaLoader)
    {
        AreaLoader doesExist = null;

        foreach (Transform transform in GetComponentInChildren<Transform>())
        {
            AreaLoader loadedArea = transform.GetComponent<AreaLoader>();
            if (loadedArea.id == areaLoader.id)
            {
                doesExist = loadedArea; break;
            }
        }
        return doesExist;
    }*/



}