using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class berries : MonoBehaviour, IPointerDownHandler/*, IBeginDragHandler, IEndDragHandler, IDragHandler*/
{
    /*public void OnBeginDrag(PointerEventData pointerEventData)
    {
        Debug.Log("Begin drag");
    }

    public void OnEndDrag(PointerEventData pointerEventData)
    {
        Debug.Log("End drag");
    }
    public void OnDrag(PointerEventData pointerEventData)
    {
        Debug.Log("On drag");
    }*/
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        Debug.Log("Down");
    }
}
