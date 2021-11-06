using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlController : MonoBehaviour
{
    private Transform Content;
    private List<string> ingredients;
    private bool isHeld = false;
    private Vector3 mousePos;
    private Vector3 initialPos;
    static private float Upmov;
    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        Content = transform.GetChild(0);
        ingredients = new List<string>();
        Upmov = 0.05f;
    }
    private void FixedUpdate()
    {
        if(Time.timeScale == 1 && isHeld)
        {
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            transform.localPosition = new Vector3(mousePos.x, mousePos.y+0.5f, 0);
        }
    }
    public void MoveContent(string name)
    {
        ingredients.Add(name);
        Content.position = Content.position + new Vector3(0, Upmov, 0);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 9 && isHeld)
        {
            for (int i = 0; i < ingredients.Count; i++)
            {
                collision.transform.GetComponent<BowlController>().MoveContent(ingredients[i]);
                Content.position = Content.position + new Vector3(0, -Upmov, 0);
            }
            ingredients = new List<string>();
        }
    }
    private void OnMouseDown()
    {
        if (Time.timeScale == 1){ isHeld = true; }
    }
    private void OnMouseUp()
    {
        isHeld = false;
        transform.position = initialPos;
    }
}
