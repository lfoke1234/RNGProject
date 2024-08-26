using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Button collectionButton;
    [SerializeField] private Button button02;
    [SerializeField] private Button inventoryButton;
    [SerializeField] private Button button03;
    [SerializeField] private Button optionButton;

    [Header("Objects")]
    [SerializeField] private GameObject collection;
    [SerializeField] private GameObject popupCollection;

    [SerializeField] private GameObject inventory;
    [SerializeField] private GameObject popupFactory;

    [SerializeField] private GameObject option;


    private void Start()
    {
        collectionButton.onClick.AddListener(() => CollectionButton());
        button02.onClick.AddListener(() => Button02());
        inventoryButton.onClick.AddListener(() => InventoryButton());
        button03.onClick.AddListener(() => Button04());
        optionButton.onClick.AddListener(() => OptionButton());
    }

    private void CollectionButton()
    {
        CloseInventory();

        if (collection.activeSelf == false)
        {
            collection.SetActive(true);
        }
        else
        {
            CloseCollection();
        }
    }


    private void Button02()
    {
        return;
    }

    private void InventoryButton()
    {
        CloseCollection();

        if (inventory.activeSelf == false)
        {
            inventory.SetActive(true);
        }
        else if (inventory.activeSelf == true)
        {
            CloseInventory();
        }
    }


    private void Button04()
    {
        return;
    }

    private void OptionButton()
    {
        if (option.activeSelf == false)
        {
            option.SetActive(true);
        }
        else if (option.activeSelf == true)
        {
            option.SetActive(false);
        }
    }

    #region Close

    private void CloseCollection()
    {
        collection.GetComponentInChildren<UI_RarlityArray>().ChangeUI(0);
        popupCollection.SetActive(false);
        collection.SetActive(false);
    }
    private void CloseInventory()
    {
        popupFactory.SetActive(false);
        inventory.SetActive(false);
    }

    #endregion
}
