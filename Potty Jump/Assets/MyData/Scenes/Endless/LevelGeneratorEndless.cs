using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneratorEndless : MonoBehaviour
{
    public GameObject background;
    public Sprite[] backgroundSprites;
    public AudioSource music;
    public AudioClip[] songs;
    public GameObject[] platformPrefab;
    public GameObject[] specialPlatformPrefab;
    public GameObject crackingPlatformPrefab;
    public GameObject breakingPlatformPrefab;
    public GameObject bouncyPlatformPrefab;
    public GameObject movingPlatformPrefab;
    public GameObject collectableItem;
    public GameObject player;
    public GameObject mainCamera;
    private GameObject[] currentEnemyPrefabs;
    public GameObject[] enemyPrefabsLevelEins;
    public GameObject[] enemyPrefabsLevelZwei;
    public GameObject[] enemyPrefabsLevelDrei;
    private GameObject[] currentPowerUps;
    public GameObject[] powerUpPrefabsLevelEins;
    public GameObject[] powerUpPrefabsLevelZwei;
    public GameObject[] powerUpPrefabsLevelDrei;

    //public int numberOfStartPlatforms = 20;
    //public float levelWidth = 3f;
    //public float minY = .2f;
    //public float maxY = 1.5f;
    //public float levelNo = 1f;
    //private bool spawnPlatforms = false;
    private int roundsToWait = 3;
    public int numberOfPlatforms = 15;
    public int completedLevel = 0;
    public bool spawnSpecialPlaform = true;
    public float specialPlatformChance = 0.2f;
    public bool spawnCrackingPlatform = true;
    public float crackingPlatformChance = 0.1f;
    public bool spawnBreakingPlatform = true;
    public float breakingPlatformChance = 0.05f;
    public bool spawnSpringPlaftform = true;
    public float springPlatformChance = 0.05f;
    public bool spawnMovingPlatform = true;
    public float movingPlatformChance = 0.2f;
    public bool spawnCollectableItem = true;
    public float collectableItemChance = 0.1f;
    public bool spawnEnemy = true;
    public float enemySpawnChance = 0.1f;
    public bool spawnPowerUp = true;
    public float powerUpSpawnChance = 0.0005f;
    

    public static float SECTION_HEIGHT = 15f;
    private float MAX_JUMP_HEIGHT = 3.2f; // abhängig von Jumpforce. Setzt voraus, dass Spieler diagonal nicht höher springt
    private float currentHeight = -SECTION_HEIGHT;/*-15f * 2f;*/
    
    public Vector2 entryPlatform = new Vector2();
    private Vector2 tmp = new Vector2();

    
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Potty");
        background.GetComponent<SpriteRenderer>().sprite = backgroundSprites[GameManager.instance.currentLevel];
        music.clip = songs[GameManager.instance.currentLevel];
        music.Play();

        switch (GameManager.instance.currentLevel)
        {
            case 0:
                spawnSpecialPlaform = true;
                spawnCrackingPlatform = true;
                spawnBreakingPlatform = false;
                spawnSpringPlaftform = true;
                spawnMovingPlatform = false;
                spawnEnemy = false;
                spawnPowerUp = false;
                currentEnemyPrefabs = new GameObject[enemyPrefabsLevelEins.Length];
                for(int i = 0; i < enemyPrefabsLevelEins.Length; i++)
                {
                    currentEnemyPrefabs[i] = enemyPrefabsLevelEins[i];
                }

                currentPowerUps = new GameObject[powerUpPrefabsLevelEins.Length];
                for (int i = 0; i < powerUpPrefabsLevelEins.Length; i++)
                {
                    currentPowerUps[i] = powerUpPrefabsLevelEins[i];
                }
                break;

            case 1:
                spawnSpecialPlaform = false; 
                spawnCrackingPlatform = false;
                spawnBreakingPlatform = true;
                spawnSpringPlaftform = true;
                spawnMovingPlatform = false;
                spawnEnemy = false;
                spawnPowerUp = false;
                currentEnemyPrefabs = new GameObject[enemyPrefabsLevelZwei.Length];
                for (int i = 0; i < enemyPrefabsLevelZwei.Length; i++)
                {
                    currentEnemyPrefabs[i] = enemyPrefabsLevelZwei[i];
                }

                currentPowerUps = new GameObject[powerUpPrefabsLevelZwei.Length];
                for (int i = 0; i < powerUpPrefabsLevelZwei.Length; i++)
                {
                    currentPowerUps[i] = powerUpPrefabsLevelZwei[i];
                }
                break;

            case 2:
                spawnSpecialPlaform = false; 
                spawnCrackingPlatform = false;
                spawnBreakingPlatform = false;
                spawnSpringPlaftform = true;
                spawnMovingPlatform = false;
                spawnEnemy = false;
                spawnPowerUp = false;
                currentEnemyPrefabs = new GameObject[enemyPrefabsLevelDrei.Length];
                for (int i = 0; i < enemyPrefabsLevelDrei.Length; i++)
                {
                    currentEnemyPrefabs[i] = enemyPrefabsLevelDrei[i];
                }

                currentPowerUps = new GameObject[powerUpPrefabsLevelZwei.Length];
                for (int i = 0; i < powerUpPrefabsLevelZwei.Length; i++)
                {
                    currentPowerUps[i] = powerUpPrefabsLevelZwei[i];
                }
                break;

            default:
                spawnSpecialPlaform = true;
                spawnCrackingPlatform = true;
                spawnBreakingPlatform = false;
                spawnSpringPlaftform = true;
                spawnMovingPlatform = true;
                spawnEnemy = false;
                spawnPowerUp = false;
                currentEnemyPrefabs = new GameObject[enemyPrefabsLevelEins.Length];
                for (int i = 0; i < enemyPrefabsLevelEins.Length; i++)
                {
                    currentEnemyPrefabs[i] = enemyPrefabsLevelEins[i];
                }

                currentPowerUps = new GameObject[powerUpPrefabsLevelEins.Length];
                for (int i = 0; i < powerUpPrefabsLevelEins.Length; i++)
                {
                    currentPowerUps[i] = powerUpPrefabsLevelEins[i];
                }
                break;
        }
        

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
        float playerPoints = player.GetComponent<EndlessPlayer>().Points;

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
        completedLevel++;
        float startY = entryPlatform.y;
        float avgOff = SECTION_HEIGHT / numberOfPlatforms;
        bool powerUpSpawned;

        if (avgOff >= MAX_JUMP_HEIGHT)
        {
            Debug.Log("Fehler. Zu wenige Plattformen in Section");
        }

        if (completedLevel > 30)
        {
            enemySpawnChance = 0.2f;
        }

        tmp.Set(entryPlatform.x, entryPlatform.y);

        for (int p = 0; p < numberOfPlatforms; p++)
        {
            float yPos = Mathf.Min(RandomY(avgOff), MAX_JUMP_HEIGHT);
            float xPos = RandomX();
            if(numberOfPlatforms < 7 && spawnSpecialPlaform) 
            {
                Instantiate(specialPlatformPrefab[GameManager.instance.currentLevel], new Vector3(xPos, yPos + tmp.y, 0f), Quaternion.identity);
            }
            else
            {
                Instantiate(platformPrefab[GameManager.instance.currentLevel], new Vector3(xPos, yPos + tmp.y, 0f), Quaternion.identity);
            }

            if(GameManager.instance.currentLevel == GameManager.instance.availableLevel - 1 && GameManager.instance.availableLevel < GameManager.instance.maxLevel && Random.Range(0f, 3f) < collectableItemChance && EndlessPlayer.instance.Points > 300)
            {
                Instantiate(collectableItem, new Vector3(xPos, yPos + tmp.y + 0.4f, 0f), Quaternion.identity);
            }

            if (spawnEnemy && Random.Range(0f, 2f) < enemySpawnChance)
            {
                Instantiate(currentEnemyPrefabs[Random.Range(0, currentEnemyPrefabs.Length)], new Vector3(xPos, yPos + tmp.y + 0.4f, 0f), Quaternion.identity);
            }

            if (spawnPowerUp && Random.Range(0f, 2f) < powerUpSpawnChance)
            {
                if (currentPowerUps.Length > 0)
                {
                    Instantiate(currentPowerUps[Random.Range(0, currentPowerUps.Length)], new Vector3(xPos, yPos + tmp.y + 0.4f, 0f), Quaternion.identity);
                }
                spawnPowerUp = false;
            }

            if (spawnCrackingPlatform && Random.Range(0f, 1f) < crackingPlatformChance)
            {
                Instantiate(crackingPlatformPrefab, new Vector3(RandomX(), Random.Range(0f, avgOff) + tmp.y + 0.2f, 0f), Quaternion.identity);
            }

            if (spawnBreakingPlatform && Random.Range(0f, 1f) < breakingPlatformChance)
            {
                Instantiate(breakingPlatformPrefab, new Vector3(RandomX(), Random.Range(0f, avgOff) + tmp.y, 0f), Quaternion.identity);
            }

            if (spawnSpringPlaftform && Random.Range(0f, 1f) < springPlatformChance)
            {
                Instantiate(bouncyPlatformPrefab, new Vector3(RandomX(), Random.Range(0f, avgOff) + tmp.y + 0.2f, 0f), Quaternion.identity);
            }

            
            if (spawnMovingPlatform && Random.Range(0f, 1f) < movingPlatformChance)
            {
                Instantiate(movingPlatformPrefab, new Vector3(RandomX(), Random.Range(0f, avgOff) + tmp.y, 0f), Quaternion.identity);
            }
            

            /*
           if (spawnCrackingPlatform && Random.Range(0f, 1f) < crackingPlatformChance)
           {
               Instantiate(crackingPlatformPrefab, new Vector3(RandomX(), Random.Range(0f, avgOff) + tmp.y, 0f), Quaternion.identity);
           }
           */
            tmp.Set(xPos, yPos + tmp.y);
        }

        if (roundsToWait > 0)
        {
            spawnEnemy = false;
            numberOfPlatforms = Random.Range(15, 25);
            roundsToWait--;
        }
        else
        {
            numberOfPlatforms = Random.Range(5, 20);
            //bei geringer Anzahl von Plattformen wird Standard-Prefab mit Special-Prefab ausgetauscht und verhindert, dass zusätzliche Plattformen spawnen
            if (numberOfPlatforms < 7)
            {
                switch (GameManager.instance.currentLevel)
                {
                    case 0:
                        spawnSpecialPlaform = true;
                        spawnCrackingPlatform = false;
                        spawnBreakingPlatform = false;
                        spawnSpringPlaftform = false;
                        spawnMovingPlatform = false;
                        spawnEnemy = false;
                        spawnPowerUp = false;
                        
                        break;

                    case 1:
                        spawnSpecialPlaform = false;
                        spawnCrackingPlatform = false;
                        spawnBreakingPlatform = false;
                        spawnSpringPlaftform = false;
                        spawnMovingPlatform = false;
                        spawnEnemy = false;
                        spawnPowerUp = false;

                        break;

                    case 2:
                        spawnSpecialPlaform = false;
                        spawnCrackingPlatform = false;
                        spawnBreakingPlatform = false;
                        spawnSpringPlaftform = false;
                        spawnMovingPlatform = false;
                        spawnEnemy = false;
                        spawnPowerUp = false;

                        break;

                    default:
                        spawnSpecialPlaform = true;
                        spawnCrackingPlatform = false;
                        spawnBreakingPlatform = false;
                        spawnSpringPlaftform = false;
                        spawnMovingPlatform = false;
                        spawnEnemy = false;
                        spawnPowerUp = false;

                        break;
                }
            }
            else
            {
                switch (GameManager.instance.currentLevel)
                {
                    case 0:
                        spawnSpecialPlaform = true;
                        spawnCrackingPlatform = true;
                        spawnBreakingPlatform = false;
                        spawnSpringPlaftform = true;
                        spawnMovingPlatform = false;
                        spawnEnemy = true;
                        spawnPowerUp = true;

                        break;

                    case 1:
                        spawnSpecialPlaform = false;
                        spawnCrackingPlatform = false;
                        spawnBreakingPlatform = true;
                        spawnSpringPlaftform = true;
                        spawnMovingPlatform = false;
                        spawnEnemy = true;
                        spawnPowerUp = true;

                        break;

                    case 2:
                        spawnSpecialPlaform = false;
                        spawnCrackingPlatform = false;
                        spawnBreakingPlatform = false;
                        spawnSpringPlaftform = true;
                        spawnMovingPlatform = false;
                        spawnEnemy = true;
                        spawnPowerUp = true;

                        break;

                    default:
                        spawnSpecialPlaform = true;
                        spawnCrackingPlatform = true;
                        spawnBreakingPlatform = false;
                        spawnSpringPlaftform = true;
                        spawnMovingPlatform = true;
                        spawnEnemy = true;
                        spawnPowerUp = true;

                        break;
                }
            }

            
        }

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
