using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using static Unity.Collections.AllocatorManager;

public class UI_ItemSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private UI_ItemTooltip itemTooltip;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemText;

    public InventoryItem item;

    private void Start()
    {
        itemTooltip = GameManager.Instance.tootips;
    }

    public void UpdateSlot(InventoryItem _newItem)
    {
        item = _newItem;

        itemImage.color = Color.white;

        if (item != null)
        {
            itemImage.sprite = item.data.icon;

            if (item.stackSize > 1)
            {
                itemText.text = item.stackSize.ToString();
            }
            else
            {
                itemText.text = "";
            }
        }
    }

    public void CleanUpSlot()
    {
        item = null;

        itemImage.sprite = null;
        itemImage.color = new Color(1,1,1,0);
        itemText.text = "";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.tootips.gameObject.SetActive(true);
        itemTooltip.SetTooltip(item);
    }
}
