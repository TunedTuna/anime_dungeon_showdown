using UnityEngine;

public class KnockBackHandler : MonoBehaviour
{
    
    [SerializeField] private Vector3 velocity;
    [SerializeField] private bool knockBackActive = false;
    [Header("in coming force")]
    [SerializeField] private Vector3 dir;
    [SerializeField] private float knockBackStrength;
    [Header("player check")]
    [SerializeField] private bool isPlayer = false;
    [SerializeField] private CharacterController characterController;

    [Header("Clock")]
    [SerializeField] private float knockBackDUration = 0.2f;
    [SerializeField] private float currentKnockBacktime = 0.2f;
    private void Start()
    {
        if(GetComponent<CharacterController>() != null)
        {
            isPlayer = true;
            characterController=GetComponent<CharacterController>();
        }
    }
    private void Update()
    {
        if (knockBackActive)
        {
            DoTheKnockBack();
        }
    }
    public void TakeKnockBack(Vector3 inny, float knockBackStrength) //IDamageable contract
    {
        //temp ref of incoming force, prob change later
        dir = inny;
        this.knockBackStrength = knockBackStrength;
        knockBackActive = true;
        velocity.x = inny.x * knockBackStrength;
        velocity.z = inny.z * knockBackStrength;
        if (isPlayer)
        {
            characterController.enabled = false;
        }

    }
    private void DoTheKnockBack()
    {
        currentKnockBacktime -= Time.deltaTime;
        if (currentKnockBacktime <= 0f)
        {
            knockBackActive = false;
            currentKnockBacktime = knockBackDUration;
            if (isPlayer)
            {
                characterController.enabled = true;
                Debug.Log( $"retruning controller objectL, status: {characterController.enabled}");
                return;
            }
            
        }

        transform.position += dir * knockBackStrength * Time.deltaTime;
    }
    public bool IsKnockBackActive()
    {
        return knockBackActive;
    }
}
