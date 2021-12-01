using System.Collections;
using Cinemachine;
using UnityEngine;
public class MouseZoom : MonoBehaviour
{
    float minOrthScale = 16;
    float maxOrthScale = 23;

    float orthScale = 18;

    public float sensitivity = 10f;
    public CinemachineVirtualCamera camZoomed;


    // Update is called once per frame
    void Update()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            camZoomed.Priority = 6;
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            camZoomed.Priority = 11;
        }
    }

    
}

