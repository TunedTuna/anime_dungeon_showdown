using UnityEngine;

public class TrainingDummyHandler : MonoBehaviour,IDamageable
{
    [SerializeField] private float maxHealth = 50f;
    [SerializeField] private float currentHealth;
    [SerializeField] private Vector3 velocity;
    [SerializeField] private bool knockBackActive = false;
    [Header("in coming force")]
    [SerializeField] private Vector3 dir;
    [SerializeField] private float knockBackStrength;

    [Header("Clock")]
    [SerializeField] private float knockBackDUration = 0.2f;
    [SerializeField] private float currentKnockBacktime = 0.2f;
    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if(knockBackActive)
        {
            DoTheKnockBack();
        }
    }
    public void TakeDamage(float damage)//now part of interface //IDamageable contract
    {
        currentHealth -= damage;

        Debug.Log(gameObject.name + " took damage: " + damage);
        Debug.Log("Current HP: " + currentHealth);
        

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void TakeKnockBack(Vector3 inny,float knockBackStrength) //IDamageable contract
    {
        //temp ref of incoming force, prob change later
        dir= inny;
        this.knockBackStrength = knockBackStrength;
        knockBackActive = true;
        velocity.x=inny.x*knockBackStrength;
        velocity.z=inny.z*knockBackStrength;
        
    }
    private void DoTheKnockBack()
    {
        currentKnockBacktime-= Time.deltaTime;
        if (currentKnockBacktime <= 0f)
        {
            knockBackActive=false;
            currentKnockBacktime = knockBackDUration;
        }
        transform.position += dir * knockBackStrength * Time.deltaTime;
    }

    void Die()
    {
        Debug.Log(gameObject.name + " died");
        Destroy(gameObject);
    }
}
