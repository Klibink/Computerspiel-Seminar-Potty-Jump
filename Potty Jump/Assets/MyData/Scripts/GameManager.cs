using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public int currentLevel = 0;
    public float highScore = 0;

    private float frustumHeight = 0f;
    private float frustumWidth = 0f;

    public float FrustumHeight { get => frustumHeight; set => frustumHeight = value; }
    public float FrustumWidth { get => frustumWidth; set => frustumWidth = value; }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        // sorgt dafür, dass das Spielfeld unabhängig vom Device gleich bleibt(20f, weil der Z-Wert der Kamera -10 beträgt -> 2 * distance)
        //https://docs.unity3d.com/Manual/FrustumSizeAtDistance.html
        frustumHeight = 20f * Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad);
        frustumWidth = frustumHeight * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.instance!=null && highScore < Player.instance.Points)
        {
            highScore = Player.instance.Points;
        }
    }
}
