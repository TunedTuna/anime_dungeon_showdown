using UnityEngine;

public class PlayerHealth : MonoBehaviour,IDamageable
{
    [Header("Health")]
    public float maxHealth = 100f;
    [SerializeField ]private float currentHealth;
    public delegate void PlayerDamaged(float hp);
    public static event PlayerDamaged OnPlayerDamaged;

    [SerializeField] private KnockBackHandler knockBackHandler;



    private PlayerMovement PlayerMovement;
    private void Awake()
    {
        currentHealth = maxHealth;
    }
    private void Start()
    {
        
    }
    void OnEnable()
    {
        
        PlayerMovement=GetComponent<PlayerMovement>();
        knockBackHandler = GetComponent<KnockBackHandler>();
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
      knockBackHandler.TakeKnockBack(inny, knockBackStrength);

    }
    public void PassOnUIdata()
    {
        //Debug.Log(GetComponent<ICharacterValues>().GetCharacterName()+" "+ maxHealth +" "+ currentHealth);
        HealthBarUI.Instance.DecorateWithTestCharacter(GetComponent<ICharacterValues>().GetCharacterName(), maxHealth,currentHealth);
    }
    void Die()
    {
        Debug.Log(gameObject.name + " died");
        GlobalEventManager.Fire_OnCharacterHasDied(gameObject);
        gameObject.SetActive(false);
    }
}