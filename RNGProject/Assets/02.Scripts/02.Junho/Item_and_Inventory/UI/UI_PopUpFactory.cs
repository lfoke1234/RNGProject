using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_PopUpFactory : MonoBehaviour, IPointerClickHandler
{
    public int sequence;
    public InventoryItem item;

    [SerializeField] private UI_ItemTooltip itemTooltip;

    [Header("UI")]
    [SerializeField] private Image itemImage;

    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;

    [SerializeField] private TextMeshProUGUI itemRaraty;
    [SerializeField] private TextMeshProUGUI goldGeneration;


    public void UpdateSlot(InventoryItem _newItem)
    {
        item = _newItem;

        if (item == null)
            return;

        itemImage.color = Color.white;

        if (item != null)
        {
            itemImage.sprite = item.data.icon;
            itemName.text = item.data.name;
            itemDescription.text = item.data.itemDescription;
            itemRaraty.text = item.data.itemType.ToString();
            goldGeneration.text = "°ñµå »ý¼º·®: " + (item.data.goldGeneration /*/ SpeedUpgrade.speed*/) + "/s";
        }
    }

    public void CleanUpSlot()
    {
        item = null;

        itemImage.sprite = null;
        itemImage.color = new Color(1f, 1f, 1f, 0f);
        itemName.text = "";
        itemDescription.text = "";
        itemRaraty.text = "";
        goldGeneration.text = "";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Inventory.Instance.EquipFactory(itemTooltip.item, sequence);
        itemTooltip.CleanUpTooltip();
    }
}
