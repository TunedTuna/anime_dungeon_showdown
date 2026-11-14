using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using System.Collections.Generic;


/* Senen Bagos
 * this class will handle the interaction of picking up and placing down objects and loot
 * 
 */
public class Inventory : MonoBehaviour {
    
    [SerializeField] private GameObject[] inventory = new GameObject[5]; 
    //inventory size of 5, can be changed in future
    [SerializeField] private int top = -1; 
    // stack structure IN INSPECTOR YOU CAN EDIT IT AND THATS STRICTLY FOR TESTING
    public Transform raycastOrigin;
    public Transform dropPosition;
    public static int Coins = 0;
    
    public event Action OnInventoryChanged;
    public TextMeshProUGUI coinText; //tuna//this ref being on player causes problem, ecpecially if chani
    [SerializeField] private bool isNewUIConnectionBeingUsed = false; //tuna- connects via instance for scene change

    [SerializeField] private List<Key_SO> keyItems = new();
    private const string KEY_TAG = "Key";
    

    private void Start()
    {
        if (isNewUIConnectionBeingUsed)
        {
            coinText = CoinUI.Instance.GetCoinTextObject();
            coinText.text = $"{Coins}";
            //prob same idea for inventory
        }
    }

    // if collides with an object that is PickUp-able will add it to inventory 
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("PickUp")) {
            if (!inventoryHasRoom()) {
                Debug.Log("Inventory full!");
                return;
            }
            
            GameObject pickUp = other.gameObject;
            addToInventory(pickUp);
            pickUp.SetActive(false);
            //Hides the pick up item in the scene
            Debug.Log(pickUp + " added to inventory");
        }
        if (other.gameObject.CompareTag("Coin")) {
            
            GameObject pickUp = other.gameObject;
            ItemController itemController = pickUp.GetComponent<ItemController>();
            itemController.playCoinSound();
            Destroy(pickUp, 0.4f);
            //Hides the pick up item in the scene
            Debug.Log(pickUp + " added to balance");
            Coins++;
            if(isNewUIConnectionBeingUsed)
            {
                coinText.text = $"{Coins}";
            }
            else
            {
                coinText.text = "Coins: " + Coins;

            }
        }
        if (other.gameObject.CompareTag(KEY_TAG))
        {
            KeyItem_AddKey(other.gameObject.GetComponent<ItemController>().GetKeyRef());
            
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("HealthRestore"))
        {
            other.gameObject.GetComponent<ItemController>().CallHealing();
            Destroy(other.gameObject);
        }
    }
    
    // this is for if we wanna drop items later
    // private void DropItem() {
    //     // sends a raycast to see if it hits nothing
    //     if (!Physics.Raycast(raycastOrigin.transform.position, raycastOrigin.transform.forward, 2f)) {
    //         if (inventoryIsEmpty()) {
    //             Debug.Log("Inventory is empty");
    //             return;
    //             // if nothing is in your inventory does nothing
    //         }
    //         
    //         getMostRecentItem().transform.position = dropPosition.position;
    //         getMostRecentItem().SetActive(true);
    //         removeFromInventory();
    //         // this drops your most recent item at your feet then removes it from your inventory
    //     }
    //     Debug.Log("Something is in the way");
    // }
    
    void OnDrawGizmos()
    {
        if (raycastOrigin == null) return;
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(raycastOrigin.transform.position, raycastOrigin.transform.forward*2);
    }
    
    // should be called in pair with removeFromInventory
    public GameObject getMostRecentItem() {
        if(inventoryIsEmpty()){
            Debug.Log("Empty Inventory");
            return null;
        } 
        return inventory[top];
    }
    
    //adds item to inventory
    public void addToInventory(GameObject item) {
        if (top == inventory.Length - 1) {
            Debug.Log("Inventory is full");
        } else {
            inventory[++top] = item;
            OnInventoryChanged?.Invoke();
        }
    }
    
    // should be called in pair with getMostRecentItem
    public void removeFromInventory() {
        if (inventoryIsEmpty()) {
            Debug.Log("Nothing in your inventory");
        } else {
            inventory[top] = null;
            top--;
            OnInventoryChanged?.Invoke();
        }
    }

    public bool inventoryHasRoom() {
        return top != 4;
    }
    
    public bool inventoryIsEmpty() {
        return top == -1;
    }
    
    public GameObject[] getInventoryArray() {
        return inventory;
    }
    public List<Key_SO> GetKeys()
    {
        return keyItems;
    }
    private void KeyItem_AddKey(Key_SO temp)
    {
        keyItems.Add(temp);
    }

}
