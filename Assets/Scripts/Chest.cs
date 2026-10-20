using System;
using TMPro;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private bool playerInRange = false;
    private bool hasBeenOpened = false;
    public AudioSource ChestSource;
    public GameObject pressChestText;
    private void Start()
    {
        PlayerInputController.instance.OnInteract += PlayerInputController_OnInteract;
    }

    private void PlayerInputController_OnInteract(object sender, EventArgs e)
    {
        if (playerInRange  && !hasBeenOpened)
        {
            OpenChest();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&&!hasBeenOpened)
        {
            playerInRange = true;
            pressChestText.SetActive(true);
            Debug.Log("Press E to open chest");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HideText();
            playerInRange = false;
        }
    }

  

    void OpenChest() {
        playChestSound();
        //play animation call
        //play sound call to sound manager?
        HideText();
        hasBeenOpened = true;

        Debug.Log("Chest opened!");
    }
    
    public void playChestSound() {
        Debug.Log("PLAY SOUND");
        ChestSource.Play();
    }
    private void HideText()
    {
        pressChestText.SetActive(false);
    }
    private void OnDisable()
    {
        PlayerInputController.instance.OnInteract += PlayerInputController_OnInteract;
    }
}