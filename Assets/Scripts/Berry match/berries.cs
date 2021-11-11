using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class berries : MonoBehaviour
{
    private Vector2 mOffset;


    void OnMouseDown()
    {
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    void OnMouseDrag()
    {
        transform.position = mousePosWorld + posOff
    }

    private Vector2 GetMouseWorldPos()
    {
        Vector2 mousePoint = Input.mousePosition;
        
    }
  
}
