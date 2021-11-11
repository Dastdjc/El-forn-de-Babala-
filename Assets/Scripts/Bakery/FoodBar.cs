//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FoodBar : MonoBehaviour
{
    //Hay que pasar los ingredientes por cada barra? Por c�digo o alg�n fichero?
    public Animator Controller;
    public GameObject Example;
    public bool LargeLabel;
    public int Offset;
    public string[] names = { "Mona", "Sandwich", "Rosquilletas", "Fartons", "Bunyols" };
    private GameObject[] Bar;
    private void Start()
    {
        // Hay hasta 9 comidas
        Bar = new GameObject[names.Length];
        // Crea las comidas
        for (int i = 0; i < Bar.Length; i++)
        {
            Bar[i] = Instantiate(Example);
            //Posici�n de la comida, si hay m�s estar�n m�s apretados
            Bar[i].transform.position =
                Bar[i].transform.position + new Vector3(Offset + 2 + (i + 1) * 2 / (Bar.Length * 0.15f), 1.5f, 0);
            Bar[i].gameObject.GetComponent<FoodController>().FoodName = names[i];
            Bar[i].gameObject.GetComponent<FoodController>().LargeLabel = LargeLabel;
            Bar[i].gameObject.GetComponent<FoodController>().Iniciate();
            Bar[i].transform.SetParent(gameObject.transform, false);
            Bar[i].SetActive(true);
        }
    }
    //Baja o sube la barra dependiendo de la posici�n
    private void OnMouseDown()
    {
        if(Time.timeScale == 1 && Controller != null)
        {
            Controller.SetTrigger("MouseTouch");
            bool dir = Controller.GetBool("Up");
            dir = !dir;
            Controller.SetBool("Up", dir);
        }
        
    }
    //Las animaciones una vez acabadas activan esta funci�n
    public void ResetTrigger() {
        if(Controller!= null)
            Controller.ResetTrigger("MouseTouch");
    }
    //Al clicar a un cliente, este env�a su pedido si es que no lo ha hecho a�n
    //Se llama a esta funci�n desde OnmouseDown() de customerController.cs
    public void WriteCommand(int index)
    {
        Bar[index].transform.GetComponent<FoodController>().SumOrder(1);
        Bar[index].transform.GetComponent<FoodController>().PrintNumbers();
    }
    public void AddFood(int index)
    {
        Bar[index].transform.GetComponent<FoodController>().SumQuantity(1);
        Bar[index].transform.GetComponent<FoodController>().PrintNumbers();
    }
}
