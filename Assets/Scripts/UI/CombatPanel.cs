using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CombatPanel : MonoBehaviour
{
    private List<UISlotHandler> inventorySlots;
    private List<ItemSlot> combatSlots = new List<ItemSlot>();

    void Start()
    {
        inventorySlots = InventoryManager.Instance.GetInventoryContents();
        GetComponentsInChildren(true, combatSlots);
        
        LoadCombatSlots();
    }

    void OnEnable()
    {
        if (InventoryManager.Instance != null)
        {
            inventorySlots = InventoryManager.Instance.GetInventoryContents();
            
            LoadCombatSlots();
        }
    }

    private void LoadCombatSlots()
    {
        foreach (ItemSlot cSlot in combatSlots)
        {
            cSlot.ClearSlot();
        }

        List<UISlotHandler> usableItems = new List<UISlotHandler>();

        if(inventorySlots != null){
            foreach (UISlotHandler invSlot in inventorySlots)
            {
                if (invSlot.item != null && invSlot.item.isUsableInCombat)
                {
                    usableItems.Add(invSlot);
                }
            }
        }

        usableItems = usableItems.OrderBy(invSlot => invSlot.item.itemID).ToList();

        for (int i = 0; i < usableItems.Count; i++)
        {
            if (i >= combatSlots.Count) break; 

            combatSlots[i].UpdateSlot(usableItems[i].item);
        }
    }
}
