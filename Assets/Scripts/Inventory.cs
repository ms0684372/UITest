using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Inventory : MonoBehaviour
{
    private static Inventory instance;
    public static Inventory GetInstance()
    {
        if(instance == null)
        {
            GameObject go = new GameObject("Inventory");
            instance = go.AddComponent<Inventory>();
        }
        return instance;
    }

    private void Awake()
    {
        if (instance !=null && instance != this)
            Destroy(gameObject);
    }

    private Dictionary<int, ItemData> itemMap;

    public void Init()
    {
        itemMap = new Dictionary<int, ItemData>();
        for (int i = 0; i < 5; i++)
            itemMap.Add(i, null);
    }

    public void AddItem(ItemData itemData)
    {
        for (int i = 0; i < itemMap.Count; i++)
        {
            if (itemMap[i] == null)
            {
                itemMap[i] = itemData;
                return;
            }
            Debug.Log("AddItem failed, bag is filled.");
        }
    }

    public Dictionary<int,ItemData> GetItemMap()
    {
        return itemMap;
    }

    public void SwapItem(int from,int to)
    {
        if(itemMap[from]==null)
        {
            Debug.Log("交換物件順序失敗, from是空的");
            return;
        }

        ItemData toData = itemMap[to];
        itemMap[to] = itemMap[from];
        itemMap[from] = toData;
    }
}
