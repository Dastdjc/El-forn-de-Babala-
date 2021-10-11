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
        other = Instantiate(gameObject);
        isHeld = true;
        /*
        //----------------------------
        //Se puede cambiar instanciando a otro objeto pero es un lío entonces con el script
        //----------------------------
        for (int i = 0; i < other.transform.childCount; i++)
        {
            Destroy(other.transform.GetChild(i));
        }
        
        if(other == null)
        {
            int num = FoodBar.GetNums(gameObject.GetComponent<TextMeshPro>().text);
            gameObject.GetComponent<TextMeshPro>().text = "Pedidos x" + num.ToString() + "\n Tienes x";
            num = FoodBar.GetNums(gameObject.GetComponent<TextMeshPro>().text, 15);
            num--;
            gameObject.GetComponent<TextMeshPro>().text += num.ToString();
            Debug.Log(other == null);
        }*/
    }
    private void OnMouseUp()
    {
        isHeld = false;
        Destroy(other);
        /*if (other == null)
        {
            int num = FoodBar.GetNums(gameObject.GetComponent<TextMeshPro>().text);
            gameObject.GetComponent<TextMeshPro>().text = "Pedidos x" + num.ToString() + "\n Tienes x";
            num = FoodBar.GetNums(gameObject.GetComponent<TextMeshPro>().text, 15);
            num++;
            gameObject.GetComponent<TextMeshPro>().text += num.ToString();
        }*/
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
