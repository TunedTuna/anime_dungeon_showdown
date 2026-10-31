using UnityEngine;
using System.Collections;

public class KamehameBeam_Logic : MonoBehaviour, IAbilities
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
        //Debug.Log("Beam activated from Ability SO!");

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
        //Debug.Log("Box is checking!");
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
            //Debug.Log("Box is checking, in da hit!");
            if (enemy != null)
            {
                //Debug.Log($"inside the Kamehame wave!: {hit.gameObject.name}");
                enemy.TakeDamage(beamDamage * Time.deltaTime);
            }
        }
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
    public void ActivateAbility()
    {
        StartCoroutine(UseKamehameha());
    }
}
