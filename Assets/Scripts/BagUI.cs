using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class BagUI : MonoBehaviour
{
    [SerializeField] private GameObject itemBase;
    [SerializeField] private Transform itemParent;
    [SerializeField] private MouseFollower mouseFollower;
    private List<BagItem> itemList;
    private UnityAction<int> itemBeginDrag;
    private UnityAction itemEndDrag;
    private UnityAction<PointerEventData> itemOnDrag;
    private UnityAction<int> itemOnDrop;

    public void Init(UnityAction<int> _beginDrag, UnityAction _endDrag, UnityAction<PointerEventData> _onDrag, UnityAction<int> _onDrop)
    {
        itemList = new List<BagItem>();

        itemBeginDrag = _beginDrag;
        itemEndDrag = _endDrag;
        itemOnDrag = _onDrag;
        itemOnDrop = _onDrop;

        CreateItem();
    }

    public void ClearEvent()
    {
        itemBeginDrag = null;
        itemEndDrag = null;
        itemOnDrag = null;
        itemOnDrop = null;
    }

    private void CreateItem()
    {
        int count = 5;
        for (int i = 0; i < count; i++)
        {
            GameObject go = Instantiate(itemBase, itemParent);
            BagItem item = go.GetComponent<BagItem>();
            item.SetEvent(itemBeginDrag, itemEndDrag, itemOnDrag, itemOnDrop);
            item.SetIndex(i);
            itemList.Add(item);
        }
    }

    public void SetItem(int index, Sprite sprite)
    {
        itemList[index].SetSprite(sprite);
    }

    public void SetMouseFollowerStatus(bool status)
    {
        mouseFollower.gameObject.SetActive(status);
    }

    public void SetMouseFollowerSprite(Sprite sprite)
    {
        mouseFollower.SetSprite(sprite);
    }

    public void MoveMouseFollower(PointerEventData eventData)
    {
        //Debug.Log($"eventData:{eventData.position} pos:{mouseFollower.transform.position} local:{mouseFollower.transform.localPosition}");
        mouseFollower.transform.position = eventData.position;
    }
}
