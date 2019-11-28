using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndlessGameManager : MonoBehaviour
{
    public static EndlessGameManager instance = null;
    public GameObject player;
    public GameObject camera;
    public GameObject deathScreen;
    private bool gameIsRunning = true;
    /*
    private float frustumHeight = 0f;
    private float frustumWidth = 0f;
    */
    Vector3 targetPos;
    public int currentLevel = 0;

    public bool GameIsRunning { get => gameIsRunning; set => gameIsRunning = value; }
    /*
    public float FrustumHeight { get => frustumHeight; set => frustumHeight = value; }
    public float FrustumWidth { get => frustumWidth; set => frustumWidth = value; }
    */

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Disable screen dimming, evtl. für Pausen- bzw. Startmenü anpassen
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        /*
        // sorgt dafür, dass das Spielfeld unabhängig vom Device gleich bleibt(20f, weil der Z-Wert der Kamera -10 beträgt -> 2 * distance)
        //https://docs.unity3d.com/Manual/FrustumSizeAtDistance.html
        frustumHeight = 20f * Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad);
        frustumWidth = frustumHeight * Camera.main.aspect;
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (!gameIsRunning)
        {
            camera.transform.position = Vector3.Lerp(camera.transform.position, targetPos, Time.deltaTime * 2.5f);
        }
    }

    public void PlayerDeath()
    {
        Debug.Log("Sie sind tot");
        gameIsRunning = false;
        deathScreen.transform.Find("ScoreEndscreenText").GetComponent<Text>().text = "Score: " + Mathf.Round(EndlessPlayer.instance.Points).ToString();
        deathScreen.transform.Find("HighscoreEndscreenText").GetComponent<Text>().text = "Highscore: " + Mathf.Round(GameManager.instance.highScore).ToString();
        StartCoroutine(ShowDeathScreen());
        
    }

    IEnumerator ShowDeathScreen()
    {
        targetPos = new Vector3(camera.transform.position.x, camera.transform.position.y - 15, camera.transform.position.z);
        yield return new WaitForSeconds(2f);
        deathScreen.SetActive(!deathScreen.activeSelf);

    }
}
