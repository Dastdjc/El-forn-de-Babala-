using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTracker : MonoBehaviour
{
    private Vector3 mousePos;
    private Vector3 initialPos;
    public bool isHeld { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        isHeld = false;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Time.timeScale == 1 && isHeld)
        {
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            transform.localPosition = new Vector3(mousePos.x, mousePos.y, 0);
        }
    }
    private void OnMouseDown()
    {
        if (Time.timeScale == 1) { isHeld = true; }
    }
    private void OnMouseUp()
    {
        isHeld = false;
        transform.position = initialPos;
    }
}