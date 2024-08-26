using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnlockCollection : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private bool isUnlock;

    [SerializeField] private Sprite lockImg;
    [SerializeField] private Image unlockImg;

    [SerializeField] private Collection collection;
    [SerializeField] private ItemData item;

    [Header("Popup Info")]
    [SerializeField] private GameObject popup;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI rarlity;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI define;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isUnlock == false)
            return;

        popup.SetActive(true);

        itemIcon.sprite = item.icon;
        rarlity.text = item.itemType.ToString();
        itemName.text = item.itemName;
        define.text = item.itemDefine;
    }

    private void OnEnable()
    {
        if (collection.collectedItems.TryGetValue(item.itemID, out bool value) && value)
        {
            isUnlock = true;
            unlockImg.color = Color.white;
            unlockImg.sprite = item.icon;
        }
        else
        {
            isUnlock = false;
            unlockImg.color = Color.white;
            unlockImg.sprite = lockImg;
        }
    }

    private void OnValidate()
    {
        gameObject.name = "Rare - " + item.itemName;
    }
}
