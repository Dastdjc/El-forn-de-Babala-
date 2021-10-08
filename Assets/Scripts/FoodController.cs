using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    private Vector3 mousePos;
    private bool isHeld;
    private GameObject other;

    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(gameObject.transform.position.y / 5, 0, gameObject.transform.position.x / 5);
    }
    private void OnMouseDown()
    {
        other = Instantiate(this.gameObject);
        isHeld = true;
    }
    private void OnMouseUp()
    {
        isHeld = false;
        Destroy(other);
    }
    private void FixedUpdate()
    {
        if (isHeld)
        {
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            other.transform.localPosition = new Vector3(mousePos.x, mousePos.y, 0);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(other);
    }
}
