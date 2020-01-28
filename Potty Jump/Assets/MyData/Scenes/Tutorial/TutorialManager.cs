using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public GameObject enemy;
    public GameObject[] platforms1;
    public GameObject[] platforms2;
    public GameObject item;
    public GameObject uiElement1;
    public GameObject uiElement2;
    public GameObject uiElement3;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnPlatforms());
        CheckMute();
    }

    // Update is called once per frame
    void Update()
    {
        CheckMute();
        if (enemy == null)
        {
            for(int i=0;i< platforms2.Length; i++)
            {
                platforms2[i].SetActive(true);
            }

            if (item != null)
            {
                item.SetActive(true);
                uiElement2.SetActive(true);
            }
            else
            {
                uiElement2.SetActive(false);
                uiElement3.SetActive(true);
                StartCoroutine(LoadScene());
            }
            uiElement1.SetActive(false);
            //uiElement2.SetActive(true);
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void CheckMute()
    {
        if (GameManager.instance.mute == true)
        {
            AudioSource[] sources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            for (int index = 0; index < sources.Length; ++index)
            {
                sources[index].mute = GameManager.instance.mute;
            }
        }
    }

    IEnumerator SpawnPlatforms()
    {
        yield return new WaitForSeconds(3f);
        for (int i = 0; i < platforms1.Length; i++)
        {
            platforms1[i].SetActive(true);
        }
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MainMenu");
    }
}
