using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FoodBar : MonoBehaviour
{
    static private GameObject Example;
    [HideInInspector]static private GameObject[] Bar;
    static private int AreActive = 0;
    private void Start()
    {
        Bar = new GameObject[13];
        Example = gameObject.transform.GetChild(2).gameObject;
        for(int i = 0; i < Bar.Length; i++) { Bar[i] = Instantiate(Example); Bar[i].SetActive(false); }
        Destroy(Example);
    }

    static public void AddItemToBar(Sprite sp, int index, int q)
    {
        if (!Bar[index].activeSelf)
        {
            Bar[index].SetActive(true);
            AreActive++;
        }
        Bar[index].GetComponent<SpriteRenderer>().sprite = sp;
        float offset = (AreActive-1) * (-0.5f);
        for(int i = 0; i < Bar.Length; i++)
        {
            if (Bar[i].activeSelf) 
            {
                Bar[i].transform.position = new Vector3(offset, 2, 0);
                offset += 1;
                Bar[index].transform.GetChild(0).GetComponent<TextMeshPro>().text = q.ToString();
            }
            
            
        }
    }
    static public void DestroyBar() { Bar = null; }

    /*
    //Hay que pasar los ingredientes por cada barra? Por código o algún fichero?
    public Animator Controller;
    public GameObject Example;
    public bool LargeLabel;
    public int Offset;
    public string[] names = { "Mona", "Flaons", "Farinada", "Fartons", "Bunyols" };
    private GameObject[] Bar;
    private void Start()
    {
        // Hay hasta 9 comidas
        Bar = new GameObject[names.Length];
        // Crea las comidas
        for (int i = 0; i < Bar.Length; i++)
        {
            Bar[i] = Instantiate(Example);
            //Posición de la comida, si hay más estarán más apretados
            Bar[i].transform.position =
                Bar[i].transform.position + new Vector3(Offset + 2 + (i + 1) * 2 / (Bar.Length * 0.15f), 1.5f, 0);
            Bar[i].gameObject.GetComponent<FoodController>().FoodName = names[i];
            Bar[i].gameObject.GetComponent<FoodController>().LargeLabel = LargeLabel;
            Bar[i].gameObject.GetComponent<FoodController>().Iniciate();
            Bar[i].transform.SetParent(gameObject.transform, false);
            Bar[i].SetActive(true);
        }
    }
    //Baja o sube la barra dependiendo de la posición
    private void /OnMouseDown()
    {
        if(Time.timeScale == 1 && Controller != null)
        {
            Controller.SetTrigger("MouseTouch");
            bool dir = Controller.GetBool("Up");
            dir = !dir;
            Controller.SetBool("Up", dir);
        }
        
    }
    //Las animaciones una vez acabadas activan esta función
    public void ResetTrigger() {
        if(Controller!= null)
            Controller.ResetTrigger("MouseTouch");
    }
    //Al clicar a un cliente, este envía su pedido si es que no lo ha hecho aún
    //Se llama a esta función desde OnmouseDown() de customerController.cs
    public void WriteCommand(int index)
    {
        Bar[index].transform.GetComponent<FoodController>().SumOrder(1);
        Bar[index].transform.GetComponent<FoodController>().PrintNumbers();
    }
    public void AddFood(int index)
    {
        Bar[index].transform.GetComponent<FoodController>().SumQuantity(1);
        Bar[index].transform.GetComponent<FoodController>().PrintNumbers();
    }*/
}
