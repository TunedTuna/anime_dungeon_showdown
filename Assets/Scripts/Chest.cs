using System;
using TMPro;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private bool playerInRange = false;
    private bool hasBeenOpened = false;
    public AudioSource ChestSource;
    public GameObject pressChestText;
    [SerializeField] private TextMeshProUGUI panelText;
    private string notificationText = "Press E to open Chest";
    [SerializeField] private GameObject chestLid;//rotate to -61

    [SerializeField] private ScriptableObject tempLoot;
    private void Start()
    {
        PlayerInputController.OnInteract += PlayerInputController_OnInteract;
        NotificationPanelUI temp = ParentUI.Instance.PassRefDataNotificationUI();
        pressChestText = temp.GetPanelGameObject();
        panelText = temp.GetTextMeshProUGUIObject();
    }

    private void PlayerInputController_OnInteract()
    {
        if (playerInRange && !hasBeenOpened)
        {
            OpenChest();
        }
    }



    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasBeenOpened)
        {
            playerInRange = true;
            pressChestText.SetActive(true);
            //Debug.Log("Press E to open chest");
            panelText.text = notificationText;
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



    void OpenChest()
    {
        playChestSound();
        if (chestLid != null)
        {
            chestLid.transform.localEulerAngles = new Vector3(-60f, 0f, 0f);
        }

        //play animation call
        //play sound call to sound manager?
        HideText();
        hasBeenOpened = true;

        Debug.Log("Chest opened!");
    }

    public void playChestSound()
    {
        Debug.Log("PLAY SOUND");
        ChestSource.Play();
    }
    private void HideText()
    {
        pressChestText.SetActive(false);
    }
    public void SetLoot(ScriptableObject temp)
    {
        tempLoot= temp;
    }
    private void OnDisable()
    {
        PlayerInputController.OnInteract -= PlayerInputController_OnInteract;
    }
}