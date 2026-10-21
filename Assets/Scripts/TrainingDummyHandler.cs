using UnityEngine;

public class TrainingDummyHandler : MonoBehaviour,IDamageable
{
    [SerializeField] private float maxHealth = 50f;
    [SerializeField] private float currentHealth;
    private void OnEnable()
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
        Destroy(gameObject);
    }
}
