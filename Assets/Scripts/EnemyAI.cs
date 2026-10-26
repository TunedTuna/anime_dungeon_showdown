using System;
using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour,IDamageable
{
    [Header("Stats")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("Movement")]
    public float moveSpeed = 3f;
    public float chaseRange = 30f;
    [Header("KnockbackRef")]
    [SerializeField] private KnockBackHandler knockBackHandler;

    [Header("Attack")]
    public float attackRange = 2f;
    public float attackDamage = 10f;
    public float attackCooldown = 1.5f;
    private bool canAttack = true;

    [Header("Target")]
    public Transform target;

    private float originalSpeed;
    private bool burning = false;
    [Header("VIsual calls")]
    [SerializeField] private ActorVisuals av;

    //notify the enemy spawner that they are dead
    public delegate void EnemyDied();
    public static event EnemyDied OnEnemyDied;

    void Start()
    {
        currentHealth = maxHealth;
        originalSpeed = moveSpeed;
        knockBackHandler = GetComponent<KnockBackHandler>();
    }

    void Update()
    {
        FindTarget();

        if (target == null) return;

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= chaseRange)
        {
            Vector3 dir = (target.position - transform.position).normalized;
            dir.y = 0f;

            transform.position += dir * moveSpeed * Time.deltaTime;

            if (dir != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(dir);
        }

        if (distance <= attackRange && canAttack)
        {
            StartCoroutine(Attack());
        }
    }

    void FindTarget()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null && player.activeInHierarchy)
        {
            target = player.transform;
        }
    }

    IEnumerator Attack()
    {
        canAttack = false;

        IDamageable playerHealth = target.GetComponent<IDamageable>();

        if (playerHealth != null)
        {
            
            playerHealth.TakeDamage(attackDamage);
            playerHealth.TakeKnockBack(transform.forward, attackDamage);
        }

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
    
    public void TakeDamage(float damage)//now part of an interface //IDamageable contract
    {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " took damage: " + damage);
        av.OnTakeDamage();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void ApplyBurn(float damage, float duration)
    {
        if (!burning)
            StartCoroutine(Burn(damage, duration));
    }

    IEnumerator Burn(float damage, float duration)
    {
        burning = true;
        float timer = 0f;

        while (timer < duration)
        {
            timer += 1f;
            TakeDamage(damage);
            yield return new WaitForSeconds(1f);
        }

        burning = false;
    }

    public void ApplySlow(float slowAmount, float duration)
    {
        StartCoroutine(Slow(slowAmount, duration));
    }

    IEnumerator Slow(float slowAmount, float duration)
    {
        moveSpeed *= slowAmount;
        yield return new WaitForSeconds(duration);
        moveSpeed = originalSpeed;
    }

    private Vector3 OutGoingKnockBackDirectionCalculation()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        Vector3 dir = (target.position - transform.position).normalized;
        dir.y = 0f;
        return dir; 
    }
    public void TakeKnockBack(Vector3 knockBackDir, float knockBackStrength) //IDamageable contract
    {
        //this is so dumb...
        knockBackHandler.TakeKnockBack(knockBackDir, knockBackStrength);
    }
    void Die()
    {
        OnEnemyDied?.Invoke();
        Destroy(gameObject);
    }
}