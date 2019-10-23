using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject platformPrefab;
    public GameObject player;
    private int playerPoints;
    private int counter = 1;
    public int numberOfStartPlatforms = 20;
    public int numberOfPlatforms = 10;
    public float levelWidth = 3f;
    public float minY = .2f;
    public float maxY = 1.5f;
    public float levelNo = 1f;
    private bool spawnPlatforms = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Doodler");

        Vector3 spawnPosition = new Vector3();

        for (int i = 0; i < numberOfStartPlatforms; i++)
        {
            spawnPosition.y += Random.Range(minY, maxY);
            spawnPosition.x = Random.Range(-levelWidth, levelWidth);
            Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerPoints = player.GetComponent<Player>().Points;

        Debug.Log(playerPoints);

        Vector3 spawnPosition = player.transform.position;
        // Y-Wert des Vektors versetzt, damit Plattformen außerhalb des Sichtfeldes spawnen
        spawnPosition.y += 6;

        if (spawnPlatforms)
        {
            spawnPlatforms = false;

            for (int i = 0; i < numberOfPlatforms; i++)
            {
                spawnPosition.y += Random.Range(minY * (1 + levelNo / 5), maxY * (1 + levelNo / 10));
                spawnPosition.x = Random.Range(-levelWidth, levelWidth);
                Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
            }
            // wenn Spieler bestimmte Punktzahl erreicht hat werden neue Plattformen gespawnt
        }
        else if (counter * 100 < playerPoints)
        {
            spawnPlatforms = true;
            counter++;
        }



    }
}
