using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 100f;    
    public float damagePerSecond = 1f;
    private float iFrames = 0f;
    private SpriteRenderer sprite;
  
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (iFrames > 0f) { 
            iFrames -= Time.deltaTime;
            sprite.color = new Color(1f, 1f, 1f, Mathf.Sin(32 * iFrames) / 2 + 0.5f);
            if (iFrames < 0.1f) sprite.color = new Color(1f, 1f, 1f, 1f);
        }
        TakeDamage(damagePerSecond * Time.deltaTime);
    }

    public void TakeDamage(float damage) 
    {
        if (iFrames > 0f || GameManager.playMode == 1) return;
        if (!GameManager.playerIsInvulnerable)
        {
            healthAmount -= damage;
        }
        healthBar.fillAmount = healthAmount / 100f;
        if(healthAmount <= 0)         
        {
            //Destroy (gameObject);
            GameManager.BeginGame();            
        }
    }

    public void TakeDamageFromEnemy(GameObject enemy, float damage, float knockbackForce, float invinsibilityFrames)
    {
        if (iFrames > 0f) return;
        Vector2 knockbackDirection = transform.position - enemy.transform.position;
        knockbackDirection.Normalize();
        Vector2 knockbackForceVector = knockbackDirection * knockbackForce;
        TakeDamage(damage);
        iFrames = invinsibilityFrames;
        GetComponent<Rigidbody2D>().AddForce(knockbackForceVector, ForceMode2D.Impulse);
    }

    public void Heal(float healingAmount)
    {
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);

        healthBar.fillAmount = healthAmount / 100f;
    }

}