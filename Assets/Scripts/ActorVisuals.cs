using System.Collections;
using UnityEngine;

public class ActorVisuals : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    public void OnTakeDamage()
    {
        StartCoroutine(HurtCorutine(2));
    }
    private IEnumerator HurtCorutine(int sec)
    {
        //this is so dumb
        bool altCOlor = false;
        for (int i = sec; i > 0; i++)
        {
            altCOlor = !altCOlor;
            if (altCOlor)
            {
                sr.color = Color.red;
            }
            else
            {
                sr.color = Color.white;
            }
            yield return new WaitForSeconds(0.2f);
            sr.color = Color.white;
        }
    }


}
