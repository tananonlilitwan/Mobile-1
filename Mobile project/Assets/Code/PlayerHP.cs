using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public float maxHealth = 100f; // Maximum player health
    public float currentHealth; // Current player health
    public float healthDecayRate = 1f; // Health decay rate per second

    void Start()
    {
        // Initialize the player's health to the maximum value at the start
        currentHealth = maxHealth;
    }

    void Update()
    {
        // Slowly decay the player's health over time
        DecayHealth(Time.deltaTime);
    }

    // Method to decay health over time
    void DecayHealth(float deltaTime)
    {
        if (currentHealth > 0)
        {
            currentHealth -= healthDecayRate * deltaTime; // Reduce health
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure health doesn't drop below 0
        }

        if (currentHealth <= 0)
        {
            PlayerDeath(); // Call death method when health reaches 0
        }
    }

    // Method to take damage from external sources
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            PlayerDeath(); // Call death method if health reaches 0 due to damage
        }
    }

    // Method to heal the player
    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    // Handle player death
    void PlayerDeath()
    {
        Debug.Log("Player has died.");
        // Add any additional logic for player death, such as respawning or game over
    }
}