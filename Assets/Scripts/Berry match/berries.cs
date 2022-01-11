using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class berries : MonoBehaviour
{
    private Vector3 dragOffset;
    private Camera cam;
    [SerializeField] private float speed = 10;

    private float startTime;

    private spawnerBerries spawn;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Start()
    {
        startTime = Time.time;
        GameObject spawner = GameObject.Find("Spawner");
        spawn = spawner.GetComponent<spawnerBerries>();
    }

    /*private void OnMouseDown()
    {
        Debug.Log("clicando");
        dragOffset = transform.position - getPosMouse();
        //transform.localScale = transform.localScale * 1.1f;
    }*/
    private void OnMouseUp()
    {
        //transform.localScale = transform.localScale * 0.9f;
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

    private void Update()
    {
        float t = Time.time - startTime;
        
        if (t > 3.5)
        {
            Destroy(gameObject);
            t = 0;
            spawn.cantGO -= 1;
        }
    }
}
