using UnityEngine;
using System.Collections;

public class CharacterB : MonoBehaviour,ICharacterValues
{
    //todo, remove all instants of inputs and move to a centralized location since these inputs will be shared amonst characters
    [SerializeField] private CharacterData_SO chd_SO;

    [Header("Base Attack")]
    public float baseDamage = 50f;
    public float attackRate = 1f;
    public Transform shootPoint;
    public GameObject shootEffect; //<-this dude should hold bulletGame object script for bullet travel
    private float attackTimer;
    [SerializeField] private int reserveBullets;
    [SerializeField] private int maxClip;
    [SerializeField] private int currentClip;

    [Header("Kamehameha Beam")]
    public KeyCode ultKey = KeyCode.Q;
    public float beamDuration = 5f;
    public float beamCooldown = 5f;
    public float beamDamage = 20f;
    public float beamRange = 30f;

    [Header("Beam Hitbox Size")]
    public float beamWidth = 2f;
    public float beamHeight = 6f;

    public Transform beamSpawn;
    public GameObject beamEffect;
    private bool canUseUlt = true;

    [Header("Stun Projectile")]
    public KeyCode stunKey = KeyCode.E;
    public GameObject stunProjectilePrefab;
    public Transform stunSpawn;
    public float stunSpeed = 20f;
    public float stunCooldown = 5f;
    private bool canUseStun = true;

    [Header("Needler")]
    public KeyCode needleKey = KeyCode.R;
    public GameObject needlePrefab;
    public float needleSpeed = 25f;
    public float needleCooldown = 5f;
    private bool canUseNeedler = true;

    public string characterName = "Kilku";

    [Header("Misc")]
    [SerializeField] private bool isUsingAbilityManager=false;
    void Update()
    {
        HandleBaseAttack();
        if (!isUsingAbilityManager)
        {
            if (Input.GetKeyDown(ultKey) && canUseUlt)
                StartCoroutine(UseKamehameha());

            if (Input.GetKeyDown(stunKey) && canUseStun)
                StartCoroutine(UseStun());

            if (Input.GetKeyDown(needleKey) && canUseNeedler)
                StartCoroutine(UseNeedler());
        }


    }
    //move timers into update?

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
        if (currentClip == 0)
        {
            //reload
            currentClip = maxClip;
            GlobalEventManager.Fire_OnRangedCharacterReload();
            return;
        }
        else
        {
            currentClip -= 1;
            GlobalEventManager.Fire_OnRangedCharacterFired(currentClip);
        }
        if (shootEffect != null)
        {
            GameObject ww = Instantiate(shootEffect, shootPoint.position, shootPoint.rotation);
            ww.GetComponent<BulletHandler>().SetDamage(baseDamage);

        }
       
        //if (Physics.Raycast(shootPoint.position, shootPoint.forward, out RaycastHit hit, Mathf.Infinity))
        //{
        //    IDamageable enemy = hit.collider.GetComponent<IDamageable>();

        //    if (enemy != null)
        //        enemy.TakeDamage(baseDamage);
        //}
    }

    IEnumerator UseKamehameha()
    {
        canUseUlt = false;
        Debug.Log("Beam activated");
        
        if (beamEffect != null)
            beamEffect.SetActive(true);

        float timer = 0f;

        while (timer < beamDuration)
        {
            timer += Time.deltaTime;
            BeamDamageBox();
            yield return null;
        }

        if (beamEffect != null)
            beamEffect.SetActive(false);
        yield return new WaitForSeconds(beamCooldown);
        canUseUlt = true;
    }

    void BeamDamageBox()
    {
        Vector3 center = beamSpawn.position + beamSpawn.forward * (beamRange / 2f);

        Vector3 halfExtents = new Vector3(
            beamWidth / 2f,
            beamHeight / 2f,
            beamRange / 2f
        );

        Collider[] hits = Physics.OverlapBox(
            center,
            halfExtents,
            beamSpawn.rotation
        );

        foreach (Collider hit in hits)
        {
            IDamageable enemy = hit.GetComponent<IDamageable>();

            if (enemy != null)
            {
                enemy.TakeDamage(beamDamage * Time.deltaTime);
            }
        }
    }

    IEnumerator UseStun()
    {
        canUseStun = false;

        if (stunProjectilePrefab != null && stunSpawn != null)
        {
            GameObject stun = Instantiate(
                stunProjectilePrefab,
                stunSpawn.position,
                stunSpawn.rotation
            );

            Rigidbody rb = stun.GetComponent<Rigidbody>();

            if (rb != null)
                rb.linearVelocity = stunSpawn.forward * stunSpeed;
        }
        yield return new WaitForSeconds(stunCooldown);
        canUseStun = true;
    }

    IEnumerator UseNeedler()
    {
        canUseNeedler = false;

        if (needlePrefab != null && shootPoint != null)
        {
            GameObject needle = Instantiate(
                needlePrefab,
                shootPoint.position,
                shootPoint.rotation
            );

            Rigidbody rb = needle.GetComponent<Rigidbody>();

            if (rb != null)
                rb.linearVelocity = shootPoint.forward * needleSpeed;
        }
        yield return new WaitForSeconds(needleCooldown);
        canUseNeedler = true;
    }

    void OnEnable()
    {
        
        canUseUlt = true;
        canUseStun = true;
        canUseNeedler = true;

        if (beamEffect != null)
            beamEffect.SetActive(false);
    }

    void OnDrawGizmosSelected()
    {
        if (beamSpawn == null) return;

        Gizmos.color = Color.cyan;

        Vector3 center = beamSpawn.position + beamSpawn.forward * (beamRange / 2f);
        Vector3 size = new Vector3(beamWidth, beamHeight, beamRange);

        Matrix4x4 oldMatrix = Gizmos.matrix;
        Gizmos.matrix = Matrix4x4.TRS(center, beamSpawn.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, size);
        Gizmos.matrix = oldMatrix;
    }

    //iCharaterValue contract
    public string GetCharacterName() { return characterName; }
    public Abilities_SO GetAbility_1() { return chd_SO.Ability_1; }
    public Abilities_SO GetAbility_2() { return chd_SO.Ability_2; }
    public Abilities_SO GetAbility_3() { return chd_SO.Ability_3; }
    public void CharacterSelectionDataChange()
    {
        if (CharacterSwap.instance.GetSlotData_B() != null)
        {
            chd_SO = CharacterSwap.instance.GetSlotData_B();
            characterName = chd_SO.CharacterName;
            currentClip= chd_SO.MaxClip;
            maxClip = currentClip;
            GlobalEventManager.Fire_OnRangedCharacterDecorateUI(maxClip);
        }
    }

}