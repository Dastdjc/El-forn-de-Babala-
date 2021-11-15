using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class berrieNegra : MonoBehaviour
{
    private Vector3 dragOffset;
    private Camera cam;
    [SerializeField] private float speed = 10;


    private void Awake()
    {
        cam = Camera.main;
    }

    private void OnMouseDown()
    {
        dragOffset = transform.position - getPosMouse();
    }

    private void OnMouseDrag()
    {
        transform.position = Vector3.MoveTowards(transform.position, getPosMouse() + dragOffset, speed * Time.deltaTime);
    }

    private Vector3 getPosMouse()
    {
        var mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        mousePos.z = 0;

        return mousePos;
    }
}
