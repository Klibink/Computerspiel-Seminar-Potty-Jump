using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject platformPrefab;
    public int numberOfPlatforms = 80;
    public float levelWidth = 4f;
    public float minY = .2f;
    public float maxY = 1.5f;
    public float levelNo = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 spawnPosition = new Vector3();

        for(int i=0; i < numberOfPlatforms; i++)
        {
            spawnPosition.y += Random.Range(minY*(1+levelNo/5), maxY*(1+levelNo/10));
            spawnPosition.x = Random.Range(-levelWidth, levelWidth);
            Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
