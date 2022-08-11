using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class PuzzleOperate : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] Image baseGridItem;
    [SerializeField] Transform operateItem;
    private List<Image> gridList;
    private int enableCount;
    private int spacing = 60;
    private Vector3 originalPosition;
    private UnityAction onBeginDrag;
    private UnityAction<PointerEventData> onDrag;
    private UnityAction onEndDrag;

    public void AddListener(UnityAction _onBeginDrag, UnityAction<PointerEventData> _onDrag, UnityAction _onEndDrag)
    {
        onBeginDrag = _onBeginDrag;
        onDrag = _onDrag;
        onEndDrag = _onEndDrag;
    }

    public void SetStyle(GridPoint[] points)
    {
        if (gridList == null)
            gridList = new List<Image>();

        if (gridList.Count < points.Length)
            CreateGrid(points.Length - gridList.Count);

        DisableGrid();

        for (int i = 0; i < points.Length; i++)
        {
            Image grid = gridList[i];
            GridPoint point = points[i];
            grid.gameObject.SetActive(true);
            grid.rectTransform.localPosition = new Vector3(point.x * 60, point.y * 60);
        }

        enableCount = points.Length;
    }

    public void SetRaycastTarget(bool state)
    {
        for (int i = 0; i < enableCount; i++)
            gridList[i].raycastTarget = state;
    }

    public void SetColor(in Color color)
    {
        for (int i = 0; i < gridList.Count; i++)
            gridList[i].color = color;
    }

    public void SetPosition(Vector2 value)
    {
        operateItem.transform.position = value;
    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        onBeginDrag?.Invoke();
        //Debug.Log("operate begindrag");
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        onDrag?.Invoke(eventData);
        //Debug.Log("operate ondrag");
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        onEndDrag?.Invoke();
        //Debug.Log("operate enddrag");
    }

    private void CreateGrid(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Image newGrid = Instantiate(baseGridItem, operateItem.transform);
            gridList.Add(newGrid);
        }
    }

    private void DisableGrid()
    {
        for (int i = 0; i < gridList.Count; i++)
            gridList[i].gameObject.SetActive(false);
    }
}
