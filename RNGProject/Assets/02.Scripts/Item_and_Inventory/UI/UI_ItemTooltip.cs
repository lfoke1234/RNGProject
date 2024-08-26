using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemTooltip : MonoBehaviour
{
    public InventoryItem item;
    [SerializeField] private GameObject popUpTap;
    [SerializeField] private GameObject assingBtn;

    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private TextMeshProUGUI itemRarity;

    public void OnDisable()
    {
        CleanUpTooltip();
    }

    public void SetTooltip(InventoryItem _newitem)
    {
        if (_newitem == null)
            return;

        assingBtn.SetActive(true);
        item = _newitem;

        icon.sprite = _newitem.data.icon;
        itemName.text = _newitem.data.itemName;
        itemDescription.text = _newitem.data.itemDescription;
        itemRarity.text = _newitem.data.itemType.ToString();
    }

    public void CleanUpTooltip()
    {
        item = null;

        icon.sprite = null;
        itemName.text = "";
        itemDescription.text = "";
        itemRarity.text = "";

        assingBtn.SetActive(false);
        popUpTap.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
