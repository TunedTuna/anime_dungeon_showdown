using UnityEngine;

public class PlayerHealth : MonoBehaviour,IDamageable
{
    [Header("Health")]
    public float maxHealth = 100f;
    private float currentHealth;

    void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)//now part of interface
    {
        currentHealth -= damage;

        Debug.Log(gameObject.name + " took damage: " + damage);
        Debug.Log("Current HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " died");
        gameObject.SetActive(false);
    }
}