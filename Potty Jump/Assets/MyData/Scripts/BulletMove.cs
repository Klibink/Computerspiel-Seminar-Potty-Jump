using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float speed = .1f;

    void Update()
    {
        transform.Translate(Vector3.forward * speed, Space.Self);
        //Zerstört Bullet wenn 5 > x-Position < -5
        //Evtl. dynamisch an Spielfeldbreite anpassen
        if(transform.position.x > 5 || transform.position.x < -5)
        {
            Destroy(gameObject);
        }
    }
}