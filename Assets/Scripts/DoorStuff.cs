using UnityEngine;

public class DoorStuff : MonoBehaviour
{
    private bool playerInRange = false;
    public GameObject openDoorText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerInputController.instance.OnInteract += PlayerInputController_OnInteract;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            openDoorText.SetActive(true);
            Debug.Log("Press E to open door");
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

    private void HideText()
    {
        openDoorText.SetActive(false);
    }

    private void PlayerInputController_OnInteract(object sender, System.EventArgs e)
    {
       
        if (playerInRange)
        {
            HideText();
            playerInRange=false;
            UseTheDoor();

        }
    }

    private void UseTheDoor()
    {
        //just destroy for simplicity. animate later

        Destroy(transform.parent.gameObject);
    }
    private void OnDisable()
    {
        PlayerInputController.instance.OnInteract += PlayerInputController_OnInteract;
    }

}
