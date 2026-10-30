using UnityEngine;
using System.Collections;

public class KamehameBeam_Logic : IAbilities
{
    [Header("Kamehameha Beam")]
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
            EnemyAI enemy = hit.GetComponent<EnemyAI>();

            if (enemy != null)
            {
                enemy.TakeDamage(beamDamage * Time.deltaTime);
            }
        }
    }
    public void ActivateAbility()
    {
        
    }
}
