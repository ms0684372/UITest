using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DrawLineMain : MonoBehaviour
{
    [SerializeField] private DragItem applyRange;
    [SerializeField] private DrawLineMouseFollower mouseFollower;

    private void Start()
    {
        mouseFollower.AddOnTriggerListener(OnMouseFollowerTriggerEnter);
        applyRange.AddOnDragListener(OnApplyRangeDrag);
    }

    private void OnApplyRangeDrag(PointerEventData eventData)
    {
        mouseFollower.transform.position = eventData.position;
        Debug.Log("Raycast name:"+eventData.pointerCurrentRaycast.gameObject);
    }

    private void OnMouseFollowerTriggerEnter(Collider2D collision)
    {
        Debug.Log("onTrigger:" + collision.name);
    }
}
