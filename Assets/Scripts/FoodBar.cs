//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FoodBar : MonoBehaviour
{
    public Animator Controller;
    public GameObject Example;
    static private GameObject[] Bar;
    private void Start()
    {
        Bar = new GameObject[5];
        string[] names = { "Mona", "Sandwich", "Rosquilletas", "Fartons", "Bunyols" };
        // Crea las comidas
        for (int i = 0; i < Bar.Length; i++)
        {
            Bar[i] = Instantiate(Example);
            //Posici�n de la comida, si hay m�s estar�n m�s apretados
            Bar[i].transform.position = Bar[i].transform.position + new Vector3((i + 1) * 2 / (Bar.Length * 0.15f) + 3, 1.5f, 0);
            //Configuraci�n del sprite. He puesto que sea de un color distinto pero aqu� habr� que poner la im�gen que le toque
            Bar[i].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color((float)i / Bar.Length, 0, (float)Bar.Length - i / Bar.Length);
            //Nombre de la comida, en cuanto est� la im�gen esto se puede comentar /*
            Bar[i].gameObject.transform.GetChild(1).GetComponent<TextMeshPro>().color = new Color((float)Bar.Length - i / Bar.Length, 0.5f, (float)i / Bar.Length);
            Bar[i].gameObject.transform.GetChild(1).GetComponent<TextMeshPro>().text = names[i];
            Bar[i].gameObject.GetComponent<FoodController>().FoodName = names[i];
            Bar[i].transform.SetParent(gameObject.transform, false);
            
            Bar[i].SetActive(true);
        }
    }
    //Baja o sube la barra dependiendo de la posici�n
    private void OnMouseDown()
    {
        
        Controller.SetTrigger("MouseTouch");
        bool dir = Controller.GetBool("Up");
        dir = !dir;
        Controller.SetBool("Up", dir);
    }
    //Las animaciones una vez acabadas activan esta funci�n
    public void ResetTrigger() {Controller.ResetTrigger("MouseTouch");}
    //Al clicar a un cliente, este env�a su pedido si es que no lo ha hecho a�n
    //Se llama a esta funci�n desde OnmouseDown() de customerController.cs
    static public void WriteCommand(int index)
    {
        //Text = Bar[index].gameObject.transform.GetChild(2).GetComponent<TextMeshPro>().text;
        int num = Bar[index].transform.GetComponent<FoodController>().ordered;//GetNums(Text);
        num++;
        Bar[index].gameObject.transform.GetChild(2).GetComponent<TextMeshPro>().text = "Pedidos x" + num.ToString() + "\n Tienes x";
        num = Bar[index].transform.GetComponent<FoodController>().quantity;
        Bar[index].gameObject.transform.GetChild(2).GetComponent<TextMeshPro>().text += num.ToString();
    }
}
