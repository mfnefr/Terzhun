using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public static MouseManager instance;
    public Item currentlyHeldItem;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateHeldItem(UISlotHandler currentSlot)
    {
        Item currentActiveItem = currentSlot.item;

        if(currentActiveItem != null && currentlyHeldItem != null && currentlyHeldItem.itemID == currentActiveItem.itemID && currentActiveItem.isStackable)
        {
            currentSlot.inventoryManager.StackInInventory(currentSlot, currentlyHeldItem);
            currentlyHeldItem = null;
            return;
        }

        if(currentSlot.item != null)
        {
            currentSlot.inventoryManager.ClearItemSlot(currentSlot);
        }

        if(currentlyHeldItem != null)
        {
            currentSlot.inventoryManager.PlaceInInventory(currentSlot, currentlyHeldItem);
        }

        currentlyHeldItem = currentActiveItem;
    }

    public void PickUpFromStack(UISlotHandler currentSlot)
    {
        if(currentlyHeldItem != null && currentlyHeldItem.itemID != currentSlot.item.itemID) return;

        if(currentlyHeldItem == null)
        {
            currentlyHeldItem = currentSlot.item.Clone();
            currentlyHeldItem.itemCount = 0;
        }

        currentlyHeldItem.itemCount++;
        currentSlot.item.itemCount--;
        currentSlot.itemCountText.text = currentSlot.item.itemCount.ToString();

        if(currentSlot.item.itemCount <= 0)
        {
            currentSlot.inventoryManager.ClearItemSlot(currentSlot);
        }
    }
}
