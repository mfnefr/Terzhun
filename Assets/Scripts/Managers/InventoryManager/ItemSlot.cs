using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Image iconDisplay;
    public Item currentItem;

    public void UpdateSlot(Item newItem)
    {
        currentItem = newItem;

        if(newItem != null)
        {
            iconDisplay.sprite = newItem.icon;
            iconDisplay.enabled = true;
        }
        else
        {
            iconDisplay.sprite = null;
            iconDisplay.enabled = false;
        }
    }

    public void ClearSlot()
    {
        UpdateSlot(null);
    }
}
