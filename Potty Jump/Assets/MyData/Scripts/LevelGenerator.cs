using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject platformPrefab;
    public GameObject crackingPlatformPrefab;
    public GameObject bouncyPlatformPrefab;
    public GameObject player;
    public GameObject mainCamera;
    private int playerPoints;
    private int counter = 1;
    public int numberOfStartPlatforms = 20;
    public int numberOfPlatforms = 10;
    public float levelWidth = 3f;
    public float minY = .2f;
    public float maxY = 1.5f;
    public float levelNo = 1f;
    private bool spawnPlatforms = false;
    private bool spawnedSpecialPlatform = false;

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

        Vector2 spawnPosition = Camera.main.transform.position;
        
        // Y-Wert des Vektors versetzt, damit Plattformen außerhalb des Sichtfeldes spawnen
        spawnPosition.y += 6;

        if (spawnPlatforms)
        {
            spawnPlatforms = false;

            for (int i = 0; i < numberOfPlatforms; i++)
            {
                spawnPosition.y += Random.Range(minY * (1 + levelNo / 5), maxY * (1 + levelNo / 10));
                spawnPosition.x = Random.Range(-levelWidth, levelWidth);

                int randomNum = Random.Range(0, 99);

                if (randomNum >= 40)
                {
                    Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
                }
                else if (randomNum < 40 && randomNum > 20)
                {// Spawnt Cracking Plattform und normale Plattform ein wenig versetzt, damit die Chance auf unerreichbare Plattformen verringert wird
                    Instantiate(crackingPlatformPrefab, spawnPosition, Quaternion.identity);
                    spawnPosition.x = Random.Range(-levelWidth, levelWidth);
                    spawnPosition.y += Random.Range(0.2f, 0.8f);
                    Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
                } else {
                    Instantiate(bouncyPlatformPrefab, spawnPosition, Quaternion.identity);
                }
                
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
