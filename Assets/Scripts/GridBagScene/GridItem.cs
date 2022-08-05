using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GridItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    [SerializeField] Image image;
    private int x;
    private int y;
    private UnityAction<int, int> onPointerEnter;
    private UnityAction<int, int> onPointerExit;
    private UnityAction<int, int> onDrop;

    public void SetColor(Color color)
    {
        image.color = color;
    }

    public void SetPosition(int _x, int _y)
    {
        x = _x;
        y = _y;
        name = x + "_" + y;
    }

    public void AddListener(UnityAction<int, int> _onPointerEnter, UnityAction<int, int> _onPointerExit, UnityAction<int, int> _onDrop)
    {
        onPointerEnter = _onPointerEnter;
        onPointerExit = _onPointerExit;
        onDrop = _onDrop;
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        onPointerEnter?.Invoke(x, y);
        //Debug.Log("gridItem  pointer enter");
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        onPointerExit?.Invoke(x, y);
        //Debug.Log("gridItem  pointer exit");
    }

    void IDropHandler.OnDrop(PointerEventData eventData)
    {
        onDrop?.Invoke(x, y);
        //Debug.Log("gridItem  pointer ondrop");
    }
}
