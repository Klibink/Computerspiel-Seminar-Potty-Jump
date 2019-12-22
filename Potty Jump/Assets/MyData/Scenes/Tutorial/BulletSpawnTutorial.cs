using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnTutorial : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float rot_range = 90f;
    public bool canShoot = true;
    public float delayInSeconds = .3f;

    private float rot;

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            if (canShoot == true)
            {
                GameObject spawn = GameObject.Find("Spawn");
                //GetComponent<AudioSource>().Play();
                //Rotation des Spawn-Objects zwischen 45 und -45 Grad, lässt sich durch rot_range ändern
                rot = -(rot_range * (Input.mousePosition.x / Screen.width) - (rot_range / 2));
                transform.rotation = Quaternion.Euler(0f, 0f, rot);
                Instantiate(bulletPrefab, spawn.transform.position, spawn.transform.rotation);
                canShoot = false;
                StartCoroutine(ShootDelay());
            }
        }

#elif UNITY_ANDROID
        //TODO: Touch input

        if (Input.touchCount > 0)
        {
            if (canShoot == true)
            {
                Touch touch = Input.GetTouch(0);
                if(touch.phase == TouchPhase.Began)
                    {
                        GameObject spawn = GameObject.Find("Spawn");
                        rot = -(rot_range * (touch.position.x / Screen.width) - (rot_range/2));
                        transform.rotation = Quaternion.Euler(0f, 0f, rot);
                        Instantiate(bulletPrefab, spawn.transform.position, spawn.transform.rotation);
                        canShoot = false;
                        StartCoroutine(ShootDelay());
                    }
                
            }
        }
#endif
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);
        canShoot = true;
    }
}