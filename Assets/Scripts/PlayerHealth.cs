using UnityEngine;

public class PlayerHealth : MonoBehaviour,IDamageable
{
    [Header("Health")]
    public float maxHealth = 100f;
    private float currentHealth;
    public delegate void PlayerDamaged(float hp);
    public static event PlayerDamaged OnPlayerDamaged;

    private PlayerMovement PlayerMovement;

    void OnEnable()
    {
        currentHealth = maxHealth;
        PlayerMovement=GetComponent<PlayerMovement>();
    }

    public void TakeDamage(float damage)//now part of interface
    {
        if(PlayerMovement.CheckIfInvincible()) return; //if this mofo Invincible, dont bother
        currentHealth -= damage;

        Debug.Log(gameObject.name + " took damage: " + damage);
        Debug.Log("Current HP: " + currentHealth);
        OnPlayerDamaged?.Invoke(currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void TakeKnockBack(Vector3 inny, float knockBackStrength) //IDamageable contract
    {
      

    }

    void Die()
    {
        Debug.Log(gameObject.name + " died");
        gameObject.SetActive(false);
    }
}