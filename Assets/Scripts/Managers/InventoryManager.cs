using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance {get; private set;}

    [Header("UI Links")]
    public GameObject inventoryPanel;

    private List<UISlotHandler> inventorySlots = new List<UISlotHandler>();
    public bool isInventoryOpen;


    void Awake()
    {
        Instance = this;

        inventoryPanel.GetComponentsInChildren(true, inventorySlots);
        inventoryPanel.SetActive(isInventoryOpen);
    }
    
    void Update()
    {
        if (Keyboard.current.iKey.wasPressedThisFrame)
        {
            isInventoryOpen = !isInventoryOpen;
            inventoryPanel.SetActive(isInventoryOpen);
        }
    }
    public void StackInInventory(UISlotHandler currentSlot, Item item)
    {
        currentSlot.item.itemCount += item.itemCount;
        currentSlot.itemCountText.text = currentSlot.item.isStackable ? currentSlot.item.itemCount.ToString() : string.Empty;
    }

    public void PlaceInInventory(UISlotHandler currentSlot, Item item)
    {
        currentSlot.item = item;
        currentSlot.icon.sprite = item.icon;
        currentSlot.itemCountText.text = item.isStackable ? item.itemCount.ToString() : string.Empty;
        currentSlot.icon.gameObject.SetActive(true);
    }

    public void ClearItemSlot(UISlotHandler currentSlot)
    {
        currentSlot.item = null;
        currentSlot.icon.sprite = null;
        currentSlot.itemCountText.text = string.Empty;
        currentSlot.icon.gameObject.SetActive(false);
    }

    public List<UISlotHandler> GetInventoryContents()
    {
        return inventorySlots;
    }

    public void AddItem(string resourceName)
    {
        Item item = Resources.Load<Item>("items/" + resourceName);

        if (item == null)
        {
            Debug.LogError($"Předmět s názvem {resourceName} nebyl ve složce Resources/items/ nalezen!");
            return;
        }

        Item itemToAdd = item.Clone();

        if (itemToAdd.isStackable)
        {
            foreach (UISlotHandler slot in inventorySlots)
            {
                if (slot.item != null && slot.item.itemID == itemToAdd.itemID)
                {
                    StackInInventory(slot, itemToAdd);
                    return;
                }
            }
        }

        foreach (UISlotHandler slot in inventorySlots)
        {
            if (slot.item == null)
            {
                PlaceInInventory(slot, itemToAdd);
                return;
            }
        }

        Debug.LogWarning("Inventář je plný! Předmět se nepodařilo přidat.");
    }

    public void RemoveItem(string itemName)
    {
        foreach (UISlotHandler slot in inventorySlots)
        {
            if (slot.item.itemName == itemName)
            {
                slot.item = null;
            }
        }
    }
}

