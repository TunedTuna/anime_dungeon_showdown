using UnityEngine;

public class BulletUI : MonoBehaviour
{
    [SerializeField] private GameObject bulletImage;
    public void HideBullet()
    {
        bulletImage.SetActive(false);
    }
    public void ShowBullet()
    {
        bulletImage.SetActive(true);
    }
}
