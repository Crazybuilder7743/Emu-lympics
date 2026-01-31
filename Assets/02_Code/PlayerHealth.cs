using UnityEngine;

public class PlayerHealth
{
    public float maxHealth = 100;
    public float currentHealth;
    public float baseHealthRegen = 0.1f;
    public float healthRegenModifier = 1;
    public delegate void OnTakeDamage(float previousHealth,float newHealth);
    public OnTakeDamage onTakeDamage;
    public delegate void OnDeath();
    public OnDeath onDeath;

    public void TakeDamage(float damage) 
    {
        float tmp = currentHealth;
        currentHealth -= damage;
        currentHealth = currentHealth < 0? 0: currentHealth;
        onTakeDamage?.Invoke(tmp,currentHealth);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            onDeath?.Invoke();
            //is dead
        }
    }

    public void RegenHealthTick()
    {
        currentHealth = currentHealth+ (baseHealthRegen* healthRegenModifier*Time.deltaTime)>= maxHealth?maxHealth: currentHealth + (baseHealthRegen * healthRegenModifier *Time.deltaTime);
    }
}
