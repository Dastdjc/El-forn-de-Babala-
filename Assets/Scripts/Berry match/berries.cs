using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class berries : MonoBehaviour
{
    private Vector3 dragOffset;
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main
    }

    private void OnMouseDown()
    {
        dragOffset = transform.position - getPosMouse();   
    }

    private void OnMouseDrag()
    {
        transform.position = getPosMouse() + dragOffset;
    }

    private Vector3 getPosMouse()
    {
        var mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        mousePos.z = 0;

        return mousePos;
    }
}
