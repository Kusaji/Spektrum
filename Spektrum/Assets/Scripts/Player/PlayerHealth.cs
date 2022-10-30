using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public bool isAlive;
    public int currentHealth;
    public int maxHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        isAlive = true;
    }

    public void TakeDamage()
    {
        currentHealth -= 1;

        if (currentHealth <= 0)
        {
            isAlive = false;
        }
    }

    public void HealthHealth()
    {
        if (currentHealth + 1 < maxHealth)
        {
            currentHealth += 1;
        }
        else
        {
            currentHealth = maxHealth;
        }
    }

}
