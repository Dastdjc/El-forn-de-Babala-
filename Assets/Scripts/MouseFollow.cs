using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    private Camera cam;
    public float Distance = 10;
    private void Start()
    {
        cam = FindObjectOfType<Camera>();
    }
    void Update()
    {
        Ray r = cam.ScreenPointToRay(Input.mousePosition);
        Vector3 pos = r.GetPoint(Distance);
        transform.position = pos;
    }
}
