using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    public bool isInventoryOpen;
    public GameObject inventoryPanel;

    void Start()
    {
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
}
