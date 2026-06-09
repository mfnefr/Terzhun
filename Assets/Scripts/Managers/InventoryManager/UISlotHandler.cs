using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UISlotHandler : MonoBehaviour, IPointerClickHandler
{
    public Item item;
    public Image icon;
    public TextMeshProUGUI itemCountText;

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            if(item == null) return;

            MouseManager.instance.PickUpFromStack(this);
            return;
        }

        MouseManager.instance.UpdateHeldItem(this);
    }

    void Start()
    {
        if(item != null)
        {
            item = item.Clone();
            icon.sprite = item.icon;
            itemCountText.text = item.isStackable ? item.itemCount.ToString() : string.Empty;
        }
        else
        {
            icon.gameObject.SetActive(false);
            itemCountText.text = string.Empty;
        }
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        itemCountText.text = string.Empty;    
    }
}
