﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject platformPrefab;
    public GameObject crackingPlatformPrefab;
    public GameObject player;
    public GameObject mainCamera;

    //public int numberOfStartPlatforms = 20;
    //public float levelWidth = 3f;
    //public float minY = .2f;
    //public float maxY = 1.5f;
    //public float levelNo = 1f;
    //private bool spawnPlatforms = false;
    public int numberOfPlatforms = 10;
    public bool spawnCrackingPlatform = true;
    public float crackingPlatformChance = 0.1f;
    public bool spawmSpringPlaftform = false;
    public float springPlatformChance = 0.1f;
    public Vector2 entryPlatform = new Vector2();
    

    public static float SECTION_HEIGHT = 15f;
    private float MAX_JUMP_HEIGHT = 3.2f; // abhängig von Jumpforce. Setzt voraus, dass Spieler diagonal nicht höher springt
    private float currentHeight = -SECTION_HEIGHT;/*-15f * 2f;*/
    private float frustumHeight = 0f;
    private float frustumWidth = 0f;
    private Vector2 tmp = new Vector2();




    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Doodler");
        // sorgt dafür, dass das Spielfeld unabhängig vom Device gleich bleibt(20f, weil der Z-Wert der Kamera -10 beträgt -> 2 * distance)
        //https://docs.unity3d.com/Manual/FrustumSizeAtDistance.html
        frustumHeight = 20f * Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad);
        frustumWidth = frustumHeight * Camera.main.aspect;

        /*
        Vector3 spawnPosition = new Vector3();

        for (int i = 0; i < numberOfStartPlatforms; i++)
        {
            spawnPosition.y += Random.Range(minY, maxY);
            spawnPosition.x = Random.Range(-levelWidth, levelWidth);
            Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        int playerPoints = player.GetComponent<Player>().Points;

        //Debug.Log(playerPoints);

        Vector2 spawnPosition = player.transform.position;
        Debug.Log(currentHeight);

        if (spawnPosition.y < currentHeight - SECTION_HEIGHT)
        {
            return;
        }

        currentHeight += SpawnSection();


        /*
        if (spawnPlatforms)
        {
            spawnPlatforms = false;

            for (int i = 0; i < numberOfPlatforms; i++)
            {
                spawnPosition.y += Random.Range(minY * (1 + levelNo / 5), maxY * (1 + levelNo / 10));
                spawnPosition.x = Random.Range(-levelWidth, levelWidth);

                int randomNum = Random.Range(0, 99);

                if (randomNum > 20)
                {
                    Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
                }
                else
                {// Spawnt Cracking Plattform und normale Plattform ein wenig versetzt, damit die Chance auf unerreichbare Plattformen verringert wird
                    Instantiate(crackingPlatformPrefab, spawnPosition, Quaternion.identity);
                    spawnPosition.x = Random.Range(-levelWidth, levelWidth);
                    spawnPosition.y += Random.Range(0.2f, 0.8f);
                    Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
                }
                
            }
            // wenn Spieler bestimmte Punktzahl erreicht hat werden neue Plattformen gespawnt
        }
        else if (counter * 100 < playerPoints)
        {
            spawnPlatforms = true;
            counter++;
        }

    */

    }

    private float SpawnSection()
    {
        Debug.Log("Spawnsection");
        float startY = entryPlatform.y;
        float avgOff = SECTION_HEIGHT / numberOfPlatforms;

        if (avgOff >= MAX_JUMP_HEIGHT)
        {
            Debug.Log("Fehler. Zu wenige Plattformen in Section");
        }

        tmp.Set(entryPlatform.x, entryPlatform.y);

        for(int p = 0; p < numberOfPlatforms; p++)
        {
            float yPos = Mathf.Min(RandomY(avgOff), MAX_JUMP_HEIGHT);
            float xPos = RandomX();
            Instantiate(platformPrefab, new Vector3(xPos, yPos + tmp.y, 0f),Quaternion.identity);

            if (spawnCrackingPlatform && Random.Range(0f, 1f)<crackingPlatformChance)
            {
                Instantiate(crackingPlatformPrefab, new Vector3(RandomX(), Random.Range(0f,avgOff) + tmp.y, 0f), Quaternion.identity);
            }
            /*
            if (spawnCrackingPlatform && Random.Range(0f, 1f) < crackingPlatformChance)
            {
                Instantiate(crackingPlatformPrefab, new Vector3(RandomX(), Random.Range(0f, avgOff) + tmp.y, 0f), Quaternion.identity);
            }
            */
            tmp.Set(xPos, yPos + tmp.y);
        }

        entryPlatform.Set(tmp.x, tmp.y);

        return tmp.y - startY;
    }

    private float RandomX()
    {
        return  Random.Range(-frustumWidth / 2f, frustumWidth / 2f);
    }

    private float RandomY(float offSet)
    {
        float randomNum = Random.Range(0, offSet * 0.8f);
        return offSet + randomNum - randomNum / 2;
    }
}
