using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawn : MonoBehaviour
{
    public GameObject bulletPrefab;

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject spawn = GameObject.Find("Spawn");
            Instantiate(bulletPrefab, spawn.transform.position, spawn.transform.rotation);
        }

#elif UNITY_ANDROID
        //TODO: Touch input

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject spawn = GameObject.Find("Spawn");
            Instantiate(bulletPrefab, spawn.transform.position, spawn.transform.rotation);
        }
#endif
    }
}