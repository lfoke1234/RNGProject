using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_CurrentFactory : MonoBehaviour
{
    public InventoryItem item;
    [SerializeField] private int sequence;

    [Header("UI")]
    [SerializeField] private Image itemImage;

    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;

    [SerializeField] private TextMeshProUGUI itemRaraty;
    [SerializeField] private TextMeshProUGUI goldGeneration;

    [SerializeField] private Button removeButton;

    private void Start()
    {
        removeButton.onClick.AddListener(() => RemoveSlot());
    }

    public void UpdateSlot(InventoryItem _newItem)
    {
        item = _newItem;

        if (item.IsUnityNull() || item.data.IsUnityNull())
            return;

        itemImage.color = Color.white;

        if (item.data != null)
        {
            removeButton.gameObject.SetActive(true);
            itemImage.sprite = item.data.icon;
            itemName.text = item.data.itemName;
            itemDescription.text = item.data.itemDescription;
            itemRaraty.text = item.data.itemType.ToString();
            goldGeneration.text = "°ñµå »ý¼º·®: " + (item.data.goldGeneration /*/ SpeedUpgrade.speed*/) + "/s";
        }
    }

    public void CleanUpSlot()
    {
        item = null;
        removeButton.gameObject.SetActive(false);
        itemImage.sprite = null;
        itemImage.color = new Color(1f, 1f, 1f, 0f);
        itemName.text = "";
        itemDescription.text = "";
        itemRaraty.text = "";
        goldGeneration.text = "";

        Inventory.Instance.CleanUpPopupFactory(sequence);
    }

    public void RemoveSlot()
    {
        Inventory.Instance.UnequipFactory(sequence);
        CleanUpSlot();
        
    }
}
