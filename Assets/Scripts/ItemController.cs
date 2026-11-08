using System;
using UnityEngine;

public class ItemController : MonoBehaviour {
    public float spinSpeed = 180f;
    public float bounceHeight = 0.5f;
    public float bounceSpeed = 2f;

    private Vector3 startPos;
    
    public AudioSource CoinSource;

    [SerializeField] private Key_SO keyRef;
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
        return  keyRef;
    }
    public void DisableBoxCollider()
    {
        GetComponent<BoxCollider>().enabled = false;
    }
}

