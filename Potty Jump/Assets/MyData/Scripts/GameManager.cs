using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public GameObject player;
    public GameObject camera;
    public GameObject deathScreen;
    private bool gameIsRunning = true;
    Vector3 targetPos;

    public bool GameIsRunning { get => gameIsRunning; set => gameIsRunning = value; }

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
        StartCoroutine(ShowDeathScreen());
        
    }

    IEnumerator ShowDeathScreen()
    {
        targetPos = new Vector3(camera.transform.position.x, camera.transform.position.y - 15, camera.transform.position.z);
        yield return new WaitForSeconds(2f);
        deathScreen.SetActive(!deathScreen.activeSelf);

    }

}
