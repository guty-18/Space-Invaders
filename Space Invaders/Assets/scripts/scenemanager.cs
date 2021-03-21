using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scenemanager : MonoBehaviour
{
    public int aliens;
    public string scene;
    public bool isdead;
    private string currentscene;

    private void Update()
    {
        if (aliens == 0)
        {
            nextLevel();
        }
    }

    void reload()
    {
        if (isdead == true)
        {
            //reload current scene
            //currentscene = SceneManager.GetActiveScene();
            SceneManager.LoadScene("level1");
        }
        
    }

    void nextLevel()
    {
        //pass to next level
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
