using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BagItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    private UnityAction<int> beginDrag;
    private UnityAction endDrag;
    private UnityAction<PointerEventData> onDrag;
    private UnityAction<int> onDrop;

    private int index;
    [SerializeField] private Image image;

    public void SetIndex(int _index)
    {
        index = _index;
    }

    public void SetSprite(Sprite _sprite)
    {
        if (_sprite == null)
        {
            image.gameObject.SetActive(false);
            image.sprite = null;
        }
        else
        {
            image.sprite = _sprite;
            image.gameObject.SetActive(true);
        }
    }

    public void SetEvent(UnityAction<int> _beginDrag, UnityAction _endDrag, UnityAction<PointerEventData> _onDrag, UnityAction<int> _onDrop)
    {
        beginDrag = _beginDrag;
        endDrag = _endDrag;
        onDrag = _onDrag;
        onDrop = _onDrop;
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag name:" + name);
        beginDrag?.Invoke(index);
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag name:" + name);
        endDrag?.Invoke();
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag name:" + name);
        onDrag?.Invoke(eventData);
    }

    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop name:" + name);
        onDrop?.Invoke(index);
    }
}
