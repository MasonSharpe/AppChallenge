using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaManager : MonoBehaviour
{
    public static AreaManager instance;


    [System.Serializable]
    public class AreaLoader
    {

        public string sceneToLoad;
        public float xToGo = -1;
        public float yToGo = -1;
        ///public bool unloads;
        /// public int id;
        /// public GameObject itself;

    }


    private void Awake()
    {
        instance = this;
    }

    /*public void LoadTriggered(AreaLoader areaLoader)
    {
        if (!NightCycle.instance.isNight)
        {
            SceneManager.LoadScene(areaLoader.sceneToLoad);
            Player.instance.transform.position = new Vector3(
                areaLoader.xToGo != -1 ? areaLoader.xToGo : Player.instance.transform.position.x,
                areaLoader.yToGo != -1 ? areaLoader.xToGo : Player.instance.transform.position.y,
                0);
        }

        proccess for adding areas
         * make new scene
         * copy the main grid from the previous scene and paste it TWICE
         * delete everything from the second grid and rename it to the new scene
         * make the tilemap as normal
         * go back to the previous scene and copy paste the main grid from the new scene
         * bazinga you're done
         

        proccess for adding the triggers for the scenes
         * add a load trigger to an area in the area scene
         * make sure the destination is slightly ahead of where it should be
         * cut and paste this trigger to the starting scene
         * do the same thing but from the previous scene to this scene
         * 
         

    }*/
}