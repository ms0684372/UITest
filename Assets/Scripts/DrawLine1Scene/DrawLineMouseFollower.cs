using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DrawLineMouseFollower : MonoBehaviour
{
    private UnityAction<Collider2D> onTriggerEnter;

    public void AddOnTriggerListener(UnityAction<Collider2D> _onTriggerEnter)
    {
        onTriggerEnter = _onTriggerEnter;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        onTriggerEnter?.Invoke(collision);
    }
}
