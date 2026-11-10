using UnityEngine;
using System.Collections.Generic;
/// <summary>
/// makes bullets based on size of clip
/// when a shot is fired, disable the image
/// hold a list of images and go down
/// </summary>
public class RangedCharacterAmmoUI : MonoBehaviour
{
    [SerializeField] private GameObject bulletObjectUI;
    [SerializeField] private GameObject bulletHolderUI;
    [SerializeField] private List<BulletUI> bullets = new List<BulletUI>();
    [SerializeField] private bool hasBeenSet = false;
    private void OnEnable()
    {
        //listen to fire from ch_b (removes bullet) (host cannot fire if clip is empty)
        //listen to reload from ch_b (cannot reload if no reserve)
        //
        GlobalEventManager.OnRangedCharacterFired += GlobalEventManager_OnRangedCharacterFired;
        GlobalEventManager.OnRangedCharacterReload += GlobalEventManager_OnRangedCharacterReload;
        

    }



    private void OnDisable()
    {
        GlobalEventManager.OnRangedCharacterFired -= GlobalEventManager_OnRangedCharacterFired;
        GlobalEventManager.OnRangedCharacterReload -= GlobalEventManager_OnRangedCharacterReload;
    }

    private void GlobalEventManager_OnRangedCharacterReload()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            bullets[i].ShowBullet();
        }
    }

    private void GlobalEventManager_OnRangedCharacterFired(int currentClip)
    {
        bullets[currentClip].HideBullet();
    }
    

    public void SetRangedInfoUI(int MaxClipSize)
    {
        if (hasBeenSet)
        {
            foreach (BulletUI bullet in bullets)
            {
                Destroy(bullet.gameObject);
            }
            bullets.Clear();
        }
        
        for (int i = 0; i<MaxClipSize; i++)
        {
            hasBeenSet = true;
            GameObject tempBullet= Instantiate(bulletObjectUI);
            tempBullet.transform.SetParent(bulletHolderUI.transform);
            bullets.Add(tempBullet.GetComponent<BulletUI>());

        }
    }


}
