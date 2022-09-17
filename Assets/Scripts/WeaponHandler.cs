using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public List<GameObject> weaponList = new List<GameObject>();
    GameObject spear, sword;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            WeaponSwap(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            WeaponSwap(2);
        }
    }

    void WeaponSwap(int i)
    {
        foreach (GameObject item in weaponList)
        {
            item.SetActive(false);
        }
        switch (i)
        {
            case 1:
            weaponList[0].SetActive(true);
            break;

            case 2:
            weaponList[1].SetActive(true);
            break;
            
            default:
            weaponList[0].SetActive(true);
            break;
        }
    }
    
    
}
