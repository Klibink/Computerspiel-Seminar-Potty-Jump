using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPlatform : MonoBehaviour
{
    public Sprite[] sprites;
    private EdgeCollider2D edgeCol;
    private Vector2[] colliderPoints;

    private void Awake()
    {
        edgeCol = GetComponent<EdgeCollider2D>();
        colliderPoints = edgeCol.points;
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.GetComponent<SpriteRenderer>().sprite = sprites[GameManager.instance.currentLevel];
        switch (GameManager.instance.currentLevel)
        {
            case 0:
                transform.localScale = new Vector3(1.2f, 1, 1);

                break;
            case 1:
                colliderPoints[0] = new Vector2(-4.594831f, 0.3405508f);
                colliderPoints[1] = new Vector2(4.596944f, 0.3405508f);
                edgeCol.points = colliderPoints;
                transform.localScale = new Vector3(0.13f, 0.13f, 0.13f);

                break;
            case 2:
                colliderPoints[0] = new Vector2(-0.4622137f, 0.04650591f);
                colliderPoints[1] = new Vector2(0.4887137f, 0.04650591f);
                edgeCol.points = colliderPoints;
                transform.localScale = new Vector3(1.2f, 1f, 1f);
                break;
            case 3:
                colliderPoints[0] = new Vector2(-0.4622137f, 0.04650591f);
                colliderPoints[1] = new Vector2(0.4887137f, 0.04650591f);
                edgeCol.points = colliderPoints;
                transform.localScale = new Vector3(1.2f, 1f, 1f);
                break;
            case 4:
                transform.localScale = new Vector3(1.2f, 1, 1);
                break;
            case 5:
                //eventuell noch ändern, wenn neue Plattform vorhanden ist
                transform.localScale = new Vector3(1.2f, 1, 1);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
