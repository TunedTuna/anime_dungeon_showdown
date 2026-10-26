using UnityEngine;
//todo change all abilities into Interfaces
public class CharacterA : MonoBehaviour, ICharacterValues
{
    [Header("Base Attack (Hold Mouse)")]
    public float baseDamage = 50f;
    public float attackRate = 1f;
    public float attackRadius = 2f;
    public Transform attackPoint;
    public GameObject slashEffect;

    [SerializeField] private float knockBackStrength= 15f; //should have a base lvl determined by character. then adjust by item

    public string characterName = "Ruby";

    private float attackTimer;
    
    
    void Update()
    {
        HandleBaseAttack();
    }

    void HandleBaseAttack()
    {
        if (Input.GetMouseButton(0))
        {
            
            attackTimer += Time.deltaTime;

            if (attackTimer >= attackRate)
            {
                attackTimer = 0f;
                BaseAttack();
            }
        }
        else
        {
            attackTimer = attackRate;
        }
    }

    void BaseAttack()
    {
        if (slashEffect != null)
            Instantiate(slashEffect, attackPoint.position, attackPoint.rotation);

        Collider[] hits = Physics.OverlapSphere(attackPoint.position, attackRadius);

        foreach (var hit in hits)
        {
            Vector3 dir = (hit.transform.position - transform.position).normalized;
            Debug.Log($"Attack position?: {attackPoint.transform.forward}");
            if (Vector3.Dot(attackPoint.transform.forward, dir) > 0.5f)
            {
                IDamageable enemy = hit.GetComponent<IDamageable>();

                if (enemy != null)
                {
                    //Vector3 normAim= attackPoint.transform.position.normalized;
                    enemy.TakeDamage(baseDamage);
                    enemy.TakeKnockBack(attackPoint.transform.forward, knockBackStrength);
                }
            }
        }
    }
    public string GetCharacterName()
    {
        return characterName;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
        }
    }
}