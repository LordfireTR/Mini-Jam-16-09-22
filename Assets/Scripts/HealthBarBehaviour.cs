using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehaviour : MonoBehaviour
{
    [SerializeField] Image fillBar;
    public float maxHealth, currentHealth;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fillBar.fillAmount = currentHealth/maxHealth;
        transform.localScale = transform.parent.localScale;
    }
}
