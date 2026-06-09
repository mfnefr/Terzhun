using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Image iconDisplay;
    public Button button;
    public TextMeshProUGUI text;
    public Item currentItem;

    public void UpdateSlot(Item newItem)
    {
        currentItem = newItem;

        if(newItem != null)
        {
            iconDisplay.sprite = newItem.icon;
            iconDisplay.enabled = true;
            button.interactable = true;
            text.text = newItem.isStackable ? newItem.itemCount.ToString() : string.Empty;
        }
        else
        {
            iconDisplay.sprite = null;
            iconDisplay.enabled = false;
            button.interactable = false;
            text.text = string.Empty;
        }
    }

    public void ClearSlot()
    {
        UpdateSlot(null);
    }
}
