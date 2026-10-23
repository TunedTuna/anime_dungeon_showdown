using UnityEngine;

public class DamageCUbeHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private float damageValue = 10f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<IDamageable>().TakeDamage(damageValue);
        }
    }
}
