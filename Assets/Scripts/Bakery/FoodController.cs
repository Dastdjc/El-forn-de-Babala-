using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FoodController : MonoBehaviour
{
    /*private Vector3 mousePos;
    private bool isHeld = false;
    private GameObject other;
    private int ordered = 0;
    public int quantity = 0;
    public bool LargeLabel;
    public string FoodName;
    public int index = 0;

    private void /OnMouseDown()
    {
        if(quantity > 0 && Time.timeScale == 1)
        {
            other = Instantiate(gameObject.transform.GetChild(0).gameObject);
            //other.GetComponent<IWantToDie>().Parent = gameObject;
            isHeld = true;
            quantity--;
            PrintNumbers();
        }
    }
    private void OnMouseUp()
    {
        if(other != null)
        {
            Destroy(other);
            quantity++;
            PrintNumbers();
        }
        isHeld = false;
    }
    public void SumQuantity(int q) { this.quantity += q; }
    public void SumOrder(int q) { this.ordered += q; }
    private void Start(){ Iniciate(); }
    public void Iniciate()
    {
        PrintNumbers();
        PrintVisual(null);
        PrintName();
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
    public void PrintNumbers()
    {
        gameObject.transform.GetChild(2).GetComponent<TextMeshPro>().text =
            "Tienes x" + quantity.ToString();
        if (LargeLabel)
            gameObject.transform.GetChild(2).GetComponent<TextMeshPro>().text +=
            "\nPedido x" + ordered.ToString();
    }
    public void PrintName()
    {
        gameObject.transform.GetChild(1).GetComponent<TextMeshPro>().text =
        FoodName;
        gameObject.transform.GetChild(1).GetComponent<TextMeshPro>().color =
            new Color(1, 0, 0);
    }
    public void PrintVisual(Sprite Visual)
    {
        if (Visual == null)
        {
            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color =
                new Color(transform.position.x / 10, transform.position.x / 5, 0.2f);
        }
        else { gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Visual; }
    }*/
}
