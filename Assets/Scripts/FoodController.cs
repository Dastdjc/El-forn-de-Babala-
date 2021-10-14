using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FoodController : MonoBehaviour
{
    private Vector3 mousePos;
    private bool isHeld;
    private GameObject other;
    private void OnMouseDown()
    {
        other = Instantiate(gameObject.transform.GetChild(0).gameObject);
        isHeld = true;
    }
    private void OnMouseUp()
    {
        isHeld = false;
        Destroy(other);
    }
    private void FixedUpdate()
    {
        if (isHeld && other != null)
        {
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            other.transform.localPosition = new Vector3(mousePos.x, mousePos.y, 0);
        }
    }
}
