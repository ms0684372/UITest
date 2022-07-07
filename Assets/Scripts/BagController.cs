using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BagController : MonoBehaviour
{
    [SerializeField] private BagUI bagUI;
    private int beginIndex;
    private Dictionary<int, ItemData> itemMap;
    private bool isDragging;

    public void Init()
    {
        itemMap = Inventory.GetInstance().GetItemMap();
        bagUI.Init(ItemBeginDrag, ItemEndDrag, ItemOnDrag, ItemOnDrop);
        isDragging = false;

        RefreshUI();
    }

    private void RefreshUI()
    {
        for (int i = 0; i < itemMap.Count; i++)
        {
            bagUI.SetItem(i, itemMap[i] == null ? null : itemMap[i].GetSprite());
        }
    }

    public void ItemBeginDrag(int index)
    {
        if (itemMap[index] == null)
            return;

        isDragging = true;
        beginIndex = index;
        bagUI.SetMouseFollowerSprite(itemMap[index].GetSprite());
        bagUI.SetMouseFollowerStatus(true);
    }

    public void ItemEndDrag()
    {
        if (!isDragging)
            return;

        isDragging = false;
        bagUI.SetMouseFollowerStatus(false);
    }

    public void ItemOnDrag(PointerEventData eventData)
    {
        if (!isDragging)
            return;

        bagUI.MoveMouseFollower(eventData);
    }

    public void ItemOnDrop(int index)
    {
        if (!isDragging || beginIndex == index)
            return;

        Inventory.GetInstance().SwapItem(beginIndex, index);
        RefreshUI();
    }
}
