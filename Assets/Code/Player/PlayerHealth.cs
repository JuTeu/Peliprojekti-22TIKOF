using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;    //mittaa pelaajan nykyista healthia
    public int maxHealth = 10;  //paljonko max health on

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;  //pelin alussa taytyy tietenkin olla max health
    }

    public void TakeDamage(int amount) //tata funktioo kutsutaan aina kun pelaaja ottaa damagee. Amount = kuinka paljon damagee
    {
        health -= amount;
        if(health <= 0)          //jos health on alle 0, pelaaja tuhotaan
        {
            Destroy (gameObject);            
        }
    }


}