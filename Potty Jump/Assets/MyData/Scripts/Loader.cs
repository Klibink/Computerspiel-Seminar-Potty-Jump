using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{

    public void CheckGameStatus()
    {
        if (GameManager.instance.firstTime)
        {
            LoadScene("Comic");
        }
        else
        {
            LoadScene("Main");
        }
    }


    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
