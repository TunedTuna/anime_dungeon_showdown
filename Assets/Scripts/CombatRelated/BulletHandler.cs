using UnityEngine;

public class BulletHandler : MonoBehaviour
{
    [SerializeField] private Vector3 bulletFaceDirection;
    [SerializeField] private float bulletAirTime=5f;
    [SerializeField] private float bulletSpeed = 1.5f;
    [SerializeField] private float currentTime;

    [SerializeField] private float temp_x;
    [SerializeField] private float temp_y;
    [SerializeField] private float temp_z;
    [SerializeField] private float damageStuff;
    private void Start()
    {
        bulletFaceDirection = transform.forward;
        temp_x = transform.forward.x;
        temp_y = transform.forward.y;
        temp_z = transform.forward.z;
        currentTime= bulletAirTime;
    }
    private void Update()
    {
        Mah();
    }
    private void Mah()
    {
        currentTime-= Time.deltaTime;
        MoveBullet();
        if (currentTime < 0)
        {
            Debug.Log("times up!!");
            Destroy(gameObject);
        }
        //needs a direction
        //go that way
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IDamageable>()!=null)
        {
            other.gameObject.GetComponent<IDamageable>().TakeDamage(damageStuff);
        }
        Debug.Log("hit something, bye!");
        Destroy(gameObject);
    }
    private void MoveBullet()
    {
        transform.position= new Vector3(transform.position.x + (temp_x * bulletSpeed), 
            transform.position.y +(temp_y * bulletSpeed), 
            transform.position.z +(temp_z * bulletSpeed));
    }
    public void SetDamage(float damage)
    {
        damageStuff += damage;
    }
}
