using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneratorStoryMode : MonoBehaviour
{
    public GameObject[] platformPrefab;
    public GameObject crackingPlatformPrefab;
    public GameObject bouncyPlatformPrefab;
    public GameObject player;
    public GameObject mainCamera;
    public GameObject[] enemyPrefabs;

    //public int numberOfStartPlatforms = 20;
    //public float levelWidth = 3f;
    //public float minY = .2f;
    //public float maxY = 1.5f;
    //public float levelNo = 1f;
    //private bool spawnPlatforms = false;
    public int numberOfPlatforms = 10;
    public bool spawnCrackingPlatform = true;
    public float crackingPlatformChance = 0.1f;
    public bool spawnSpringPlaftform = true;
    public float springPlatformChance = 0.05f;
    public bool spawnEnemy = true;
    public float enemySpawnChance = 0.05f;


    public static float SECTION_HEIGHT = 15f;
    private float MAX_JUMP_HEIGHT = 3.2f; // abhängig von Jumpforce. Setzt voraus, dass Spieler diagonal nicht höher springt
    private float currentHeight = -SECTION_HEIGHT;/*-15f * 2f;*/

    public Vector2 entryPlatform = new Vector2();
    private Vector2 tmp = new Vector2();



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Doodler");


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
        float playerPoints = player.GetComponent<StoryPlayer>().Points;

        //Debug.Log(playerPoints);

        Vector2 spawnPosition = player.transform.position;

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

    */

    }

    private float SpawnSection()
    {
        Debug.Log("Spawnsection");
        Debug.Log(GameManager.instance.FrustumWidth);
        float startY = entryPlatform.y;
        float avgOff = SECTION_HEIGHT / numberOfPlatforms;

        if (avgOff >= MAX_JUMP_HEIGHT)
        {
            Debug.Log("Fehler. Zu wenige Plattformen in Section");
        }

        tmp.Set(entryPlatform.x, entryPlatform.y);

        for (int p = 0; p < numberOfPlatforms; p++)
        {
            float yPos = Mathf.Min(RandomY(avgOff), MAX_JUMP_HEIGHT);
            float xPos = RandomX();
            Instantiate(platformPrefab[GameManager.instance.currentLevel], new Vector3(xPos, yPos + tmp.y, 0f), Quaternion.identity);

            if (spawnEnemy && Random.Range(0f, 1f) < enemySpawnChance)
            {
                Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], new Vector3(xPos, yPos + tmp.y + 0.4f, 0f), Quaternion.identity);
            }

            if (spawnCrackingPlatform && Random.Range(0f, 1f) < crackingPlatformChance)
            {
                Instantiate(crackingPlatformPrefab, new Vector3(RandomX(), Random.Range(0f, avgOff) + tmp.y, 0f), Quaternion.identity);
            }

            if (spawnSpringPlaftform && Random.Range(0f, 1f) < springPlatformChance)
            {
                Instantiate(bouncyPlatformPrefab, new Vector3(RandomX(), Random.Range(0f, avgOff) + tmp.y, 0f), Quaternion.identity);
            }

            /*
            if (spawnCrackingPlatform && Random.Range(0f, 1f) < crackingPlatformChance)
            {
                Instantiate(crackingPlatformPrefab, new Vector3(RandomX(), Random.Range(0f, avgOff) + tmp.y, 0f), Quaternion.identity);
            }
            */
            tmp.Set(xPos, yPos + tmp.y);
        }

        numberOfPlatforms = Random.Range(5, 20);

        entryPlatform.Set(tmp.x, tmp.y);

        return tmp.y - startY;
    }

    private float RandomX()
    {
        return Random.Range(-GameManager.instance.FrustumWidth / 2f, GameManager.instance.FrustumWidth / 2f);
    }

    private float RandomY(float offSet)
    {
        float randomNum = Random.Range(0, offSet * 0.8f);
        return offSet + randomNum - randomNum / 2;
    }
}
