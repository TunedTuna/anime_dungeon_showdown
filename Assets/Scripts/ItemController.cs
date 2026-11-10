using System;
using UnityEngine;

public class ItemController : MonoBehaviour {
    public float spinSpeed = 180f;
    public float bounceHeight = 0.5f;
    public float bounceSpeed = 2f;

    private Vector3 startPos;
    [Header("Coin")]
    public AudioSource CoinSource;

    [Header("KeyStuff")]
    [SerializeField] private Key_SO keyRef;

    [Header("HealthStuff")]
    [SerializeField] private float hpRestore = 50;
    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Spin
        transform.Rotate(0, spinSpeed * Time.deltaTime, 0);

        // Bounce
        float newY = startPos.y + Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }

    public void playCoinSound() {
        Debug.Log("PLAY SOUND");
        CoinSource.Play();
    }
    public Key_SO GetKeyRef()
    {
        DisableBoxCollider();
        GlobalEventManager.Fire_OnKeyPickedUp();
        return keyRef;
    }
    public void DisableBoxCollider()
    {
        GetComponent<BoxCollider>().enabled = false;
    }
    public void CallHealing()
    {
        DisableBoxCollider() ;
        GlobalEventManager.Fire_OnHealActiveCharacter(hpRestore);
    }
}

