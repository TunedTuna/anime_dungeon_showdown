using UnityEngine;
//not used atm
public interface IDamageable
{
    public void TakeDamage(float damage);
    public void TakeKnockBack(Vector3 knockBackDir, float knockBackStrength);

}
