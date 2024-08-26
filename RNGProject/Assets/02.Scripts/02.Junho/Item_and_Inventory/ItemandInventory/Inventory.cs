using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Inventory : MonoBehaviour, ISaveManager
{
    public static Inventory Instance;

    public int gold;
    public List<InventoryItem> item;
    public Dictionary<ItemData, InventoryItem> ItemDictionary;

    public InventoryItem[] currentFactory;
    public ItemData currentItem;

    [Header("UI")]
    [SerializeField] private Transform inventorySlotParent;
    [SerializeField] private Transform currentFactorySlotParent;
    [SerializeField] private Transform popUpFactoryUI;
    private UI_ItemSlot[] inventoryItemSlot;
    private UI_CurrentFactory[] currentFactorySlot;
    private UI_PopUpFactory[] popUpFactorySlot;


    [Header("Data base")]
    public List<ItemData> itemDataBase;
    public List<InventoryItem> loadedItems;

    [Space]
    public List<ItemData> rareItems;
    public List<ItemData> uniqueItems;
    public List<ItemData> epicItems;
    public List<ItemData> legendItems;

    [SerializeField] private ItemData test;
    [SerializeField] private GameObject slotPrefab;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        item = new List<InventoryItem>();
        ItemDictionary = new Dictionary<ItemData, InventoryItem>();
        currentFactory = new InventoryItem[5];
    }

    private void Start()
    {
        rareItems = new List<ItemData>();
        uniqueItems = new List<ItemData>();
        epicItems = new List<ItemData>();
        legendItems = new List<ItemData>();
        InitializeItemLists();

        inventoryItemSlot = inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>();
        currentFactorySlot = currentFactorySlotParent.GetComponentsInChildren<UI_CurrentFactory>();
        popUpFactorySlot = popUpFactoryUI.GetComponentsInChildren<UI_PopUpFactory>();

        StartingItem();
        UpdateSlotUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            AddItem(test);
        }
    }

    private void StartingItem()
    {
        if (loadedItems.Count > 0)
        {
            foreach (InventoryItem item in loadedItems)
            {
                for (int i = 0; i < item.stackSize; i++)
                {
                    AddItem(item.data);
                }
            }
        }
    }

    private void InitializeItemLists()
    {
#if UNITY_EDITOR
        string rarePath = "Assets/05.Data/Item/Rare";
        string uniquePath = "Assets/05.Data/Item/Unique";
        string epicPath = "Assets/05.Data/Item/Epic";
        string legendPath = "Assets/05.Data/Item/Legend";

        rareItems = LoadItemsFromPath(rarePath);
        uniqueItems = LoadItemsFromPath(uniquePath);
        epicItems = LoadItemsFromPath(epicPath);
        legendItems = LoadItemsFromPath(legendPath);
#endif
    }

    private List<ItemData> LoadItemsFromPath(string path)
    {
        List<ItemData> items = new List<ItemData>();

#if UNITY_EDITOR
        string[] assetGuids = AssetDatabase.FindAssets("t:ItemData", new[] { path });

        foreach (string guid in assetGuids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            ItemData item = AssetDatabase.LoadAssetAtPath<ItemData>(assetPath);
            if (item != null)
            {
                items.Add(item);
            }
        }
#endif

        return items;
    }



    public void AddItem(ItemData _item)
    {
        if (_item == null)
        {
            return;
        }

        if (ItemDictionary.TryGetValue(_item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(_item);
            item.Add(newItem);
            ItemDictionary.Add(_item, newItem);
        }

        UpdateSlotUI();
    }


    public void RemoveItem(ItemData _item)
    {
        if (ItemDictionary.TryGetValue(_item, out InventoryItem value))
        {
            if (value.stackSize <= 1)
            {
                item.Remove(value);
                ItemDictionary.Remove(_item);
            }
            else
            {
                value.RemoveStack();
            }
        }

        UpdateSlotUI();
    }



    public void EquipFactory(InventoryItem factoryItem, int sequence)
    {

        if (sequence >= 0 && sequence < currentFactory.Length)
        {
            UnequipFactory(sequence);

            currentFactory[sequence] = factoryItem;
            RemoveItem(factoryItem.data);

            UpdateSlotUI();
        }
        else
        {
        }
    }


    public void UnequipFactory(int index)
    {
        if (currentFactory[index] != null)
        {
            AddItem(currentFactory[index].data);
            currentFactory[index] = null;

            UpdateSlotUI();
        }
    }

    public void CleanUpPopupFactory(int index)
    {
        if (popUpFactorySlot[index] != null)
        {
            popUpFactorySlot[index].CleanUpSlot();
        }
    }


    private void UpdateSlotUI()
    {
        if (inventoryItemSlot == null || currentFactorySlot == null || popUpFactorySlot == null)
        {
            return;
        }

        for (int i = 0; i < inventoryItemSlot.Length; i++)
        {
            inventoryItemSlot[i].CleanUpSlot();
        }

        if (item.Count > inventoryItemSlot.Length)
        {

            for (int i = inventoryItemSlot.Length; i < item.Count; i++)
            {
                GameObject newSlot = Instantiate(slotPrefab, inventorySlotParent);
                UI_ItemSlot slotComponent = newSlot.GetComponent<UI_ItemSlot>();
                inventoryItemSlot = inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>();
            }
        }

        for (int i = 0; i < item.Count && i < inventoryItemSlot.Length; i++)
        {
            inventoryItemSlot[i].UpdateSlot(item[i]);
        }

        for (int i = 0; i < currentFactorySlot.Length; i++)
        {
            currentFactorySlot[i].CleanUpSlot();
        }

        for (int i = 0; i < currentFactory.Length && i < currentFactorySlot.Length; i++)
        {
            currentFactorySlot[i].UpdateSlot(currentFactory[i]);
        }

        for (int i = 0; i < popUpFactorySlot.Length; i++)
        {
            popUpFactorySlot[i].CleanUpSlot();
        }

        for (int i = 0; i < currentFactory.Length && i < popUpFactorySlot.Length; i++)
        {
            popUpFactorySlot[i].UpdateSlot(currentFactory[i]);
        }
    }



    #region Gacha
    public void RandomRare()
    {
        int random = Random.Range(0, rareItems.Count);
        currentItem = rareItems[random];
        AddItem(currentItem);
        Collection.Instance.MarkAsCollected(currentItem);
    }

    public void RandomUnique()
    {
        int random = Random.Range(0, uniqueItems.Count);
        currentItem = uniqueItems[random];
        AddItem(currentItem);
        Collection.Instance.MarkAsCollected(currentItem);
    }

    public void RandomEpic()
    {
        int random = Random.Range(0, epicItems.Count);
        currentItem = epicItems[random];
        AddItem(currentItem);
        Collection.Instance.MarkAsCollected(currentItem);
    }

    public void RandomLegend()
    {
        int random = Random.Range(0, legendItems.Count);
        currentItem = legendItems[random];
        AddItem(currentItem);
        Collection.Instance.MarkAsCollected(currentItem);
    }
    #endregion


    #region SaveandLoad
    public void LoadData(GameData _data)
    {
        gold = _data.gold;

        foreach (KeyValuePair<string, int> pair in _data.inventory)
        {
            foreach (var item in itemDataBase)
            {
                if (item != null && item.itemID == pair.Key)
                {
                    InventoryItem itemToLoad = new InventoryItem(item);
                    itemToLoad.stackSize = pair.Value;

                    loadedItems.Add(itemToLoad);
                }
            }
        }

        for (int i = 0; i < _data.currentFactory.Length; i++)
        {
            string itemID = _data.currentFactory[i];
            if (itemID != "None")
            {
                ItemData matchedItem = itemDataBase.FirstOrDefault(item => item != null && item.itemID == itemID);
                if (matchedItem != null)
                {
                    InventoryItem itemToEquip = new InventoryItem(matchedItem);
                    EquipFactory(itemToEquip, i);
                }
            }
            else
            {
            }
        }
    }



    public void SaveData(ref GameData _data)
    {
        _data.gold = gold;

        _data.inventory.Clear();

        foreach (KeyValuePair<ItemData, InventoryItem> pair in ItemDictionary)
        {
            _data.inventory.Add(pair.Key.itemID, pair.Value.stackSize);
        }

        for (int i = 0; i < currentFactory.Length; i++)
        {
            if (currentFactory[i] != null)
            {
                _data.currentFactory[i] = currentFactory[i].data.itemID;
            }
            else
            {
                _data.currentFactory[i] = "None";
            }
        }
    }


#if UNITY_EDITOR
    [ContextMenu("Fill up item data base")]
    private void FillUpItemDataBase() => itemDataBase = new List<ItemData>(GetItemDataBase());

    private List<ItemData> GetItemDataBase()
    {
        List<ItemData> itemDataBase = new List<ItemData>();
        string[] assetNames = AssetDatabase.FindAssets("", new[] { "Assets/05.Data/Item" });

        foreach (string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var itemData = AssetDatabase.LoadAssetAtPath<ItemData>(SOpath);

            if (itemData == null)
                continue;

            itemDataBase.Add(itemData);
        }

        return itemDataBase;
    }
#endif

    #endregion
}
