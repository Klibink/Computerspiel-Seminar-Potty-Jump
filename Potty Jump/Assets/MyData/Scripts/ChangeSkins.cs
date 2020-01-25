using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkins : MonoBehaviour
{
    private int skinsUnlocked;
    int temp;
    // Start is called before the first frame update
    void Start()
    {
        temp = GameManager.instance.currentSkin;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSkin(int skinNumber)
    {
        GameManager.instance.currentSkin = skinNumber;
    }

    public void ChangeFlowerUp()
    {
        for (int i = GameManager.instance.currentFlower; i < GameManager.instance.unlockSkins.Length - 1;)
        {
            i++;
            if (GameManager.instance.unlockFlowers[i] == true)
            {
                GameManager.instance.currentFlower = i;
                break;
            }
            else
            {

            }
        }
    }

    public void ChangeFlowerDown()
    {
        for (int i = GameManager.instance.currentFlower; i > 0;)
        {
            i--;
            if (GameManager.instance.unlockFlowers[i] == true)
            {
                GameManager.instance.currentFlower = i;
                break;
            }
            else
            {

            }
        }
    }

    public void ChangeSkinUp()
    {
        for(int i= GameManager.instance.currentSkin; i < GameManager.instance.unlockSkins.Length - 1; )
        {
            i++;
            if (GameManager.instance.unlockSkins[i] == true)
            {
                GameManager.instance.currentSkin = i;
                break;
            }
            else
            {

            }
        }
        
        
        

        /*temp++;
        if (GameManager.instance.unlockSkins[temp] == true)
        {

        }



        for (int i = GameManager.instance.currentSkin; i < GameManager.instance.unlockSkins.Length - 1; ++i)
        {
            if (temp == GameManager.instance.currentSkin)
            {
                if (GameManager.instance.unlockSkins[i] == true)
                {
                    GameManager.instance.currentSkin = i;
                }
                else
                {
                    i++;
                }
            }
            else break;
                
        }
        */
            
       
        
    }

    public void ChangeSkinDown()
    {
        for (int i = GameManager.instance.currentSkin; i > 0;)
        {
            i--;
            if (GameManager.instance.unlockSkins[i] == true)
            {
                GameManager.instance.currentSkin = i;
                break;
            }
            else
            {

            }
        }
    }
}
