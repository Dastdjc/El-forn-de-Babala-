using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FoodController : MonoBehaviour
{
    private Vector3 mousePos;
    private bool isHeld;
    private GameObject other;
    public int quantity = 0;
    public int ordered = 0;
    public string FoodName = "";

    private void OnMouseDown()
    {
        if(quantity > 0 && Time.timeScale == 1)
        {
            other = Instantiate(gameObject.transform.GetChild(0).gameObject);
            other.GetComponent<IWantToDie>().FoodName = FoodName;
            isHeld = true;
            quantity--;
            PrintNumbers();
        }
    }
    private void OnMouseUp()
    {
        if(other != null)
        {
            isHeld = false;
            Destroy(other);
            quantity++;
            PrintNumbers();
        }
    }
    private void Start(){
        //if(gameObject.transform.childCount > 1)
            gameObject.transform.GetChild(2).GetComponent<TextMeshPro>().text =
            "Pedido x" + ordered.ToString() + "\nTienes x" + quantity.ToString();
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
    private void PrintNumbers()
    {
        if(other != null)
            gameObject.transform.GetChild(2).GetComponent<TextMeshPro>().text =
            "Pedido x" + ordered.ToString() + "\nTienes x" + quantity.ToString();
    }
}
