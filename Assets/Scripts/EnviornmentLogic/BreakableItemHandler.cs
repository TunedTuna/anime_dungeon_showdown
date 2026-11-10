using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// drops basic resources, unless...
/// </summary>
public class BreakableItemHandler : MonoBehaviour, IDamageable
{
    [SerializeField] private float currentHp;
    [SerializeField] private GameObject coin;
    [SerializeField] private GameObject hpObject;
    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            DropSomeStuff();
            gameObject.SetActive(false);
        }
    }
    public void TakeKnockBack(Vector3 knockBackDir, float knockBackStrength)
    {
        //static item, so doesn't have to do anything...
    }
    private void DropSomeStuff()
    {
        int fiftyFifty = UnityEngine.Random.Range(0, 2);
        if (fiftyFifty == 0)
        {
            int randNum = UnityEngine.Random.Range(1, 5);
            for (int i = 0; i < randNum; i++)
            {
                GameObject temp =
                Instantiate(coin, transform);
                temp.transform.parent = null;
               
            }
        }
        else
        {
            GameObject temp =
               Instantiate(hpObject, transform);
            temp.transform.parent = null;
            
        }

    }
}
