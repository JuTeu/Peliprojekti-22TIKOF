using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 100f;    
  

    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void TakeDamage(float damage) 
    {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 100f;
        if(healthAmount <= 0)         
        {
            Destroy (gameObject);            
        }
    }
    public void Heal(float healingAmount)
    {
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);

        healthBar.fillAmount = healthAmount / 100f;
    }

}