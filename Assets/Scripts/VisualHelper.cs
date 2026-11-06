using UnityEngine;
using UnityEngine.UI;

public class VisualHelper : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    public void SpriteChangeFromCharacterChange(Sprite temp)
    {
        spriteRenderer.sprite=temp;
    }
}
