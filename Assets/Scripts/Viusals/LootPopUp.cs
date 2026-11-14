using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LootPopUp : MonoBehaviour
{
    [SerializeField] private GameObject imageHolder;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Vector3 startPos= new Vector3(0,1,0);
    [SerializeField] private float currentTime;
    [SerializeField] private float maxTime = 1f;
    private void OnEnable()
    {
        GlobalEventManager.OnItemPickedUp += GlobalEventManager_OnItemPickedUp;
        HideImageHolder();
    }

    private void GlobalEventManager_OnItemPickedUp(ItemSO obj)
    {
        PrototypeAnimation(obj.Icon);
    }
    private void PrototypeAnimation(Sprite sprite)
    {
        ShowImageHolder();
        currentTime=maxTime;
        spriteRenderer.sprite= sprite;
        StartCoroutine(MoveImageHolder());


    }
    private void HideImageHolder()
    {

        imageHolder.SetActive(false);
    }
    private void ShowImageHolder()
    {
        imageHolder.SetActive(true);
        imageHolder.transform.localPosition = startPos;  
    }

    private IEnumerator MoveImageHolder()
    {
        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            imageHolder.transform.position = new Vector3 (imageHolder.transform.position.x, imageHolder.transform.position.y+0.01f, imageHolder.transform.position.z);
            yield return null;
        }
        spriteRenderer.sprite= null;
        HideImageHolder();
    }
    private void OnDisable()
    {
        GlobalEventManager.OnItemPickedUp -= GlobalEventManager_OnItemPickedUp;
        HideImageHolder();
    }
}
