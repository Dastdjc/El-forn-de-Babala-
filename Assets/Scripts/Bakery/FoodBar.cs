using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FoodBar : MonoBehaviour
{
    public GameObject Example;
    public Sprite[] ingrs;
    public Sprite[] recipes;
    static private Sprite[] recipesStat;
    static private GameObject[] CurrentVisuals;
    static private GameObject Result;
    static private int[] WhatINeed;
    static private int[] WhatIHave;
    static private int AreActive;
    static private Transform Camara;

    public void Initiate()
    {
        recipesStat = new Sprite[recipes.Length];
        for(int i = 0; i < recipes.Length; i++) { recipesStat[i] = recipes[i]; }
        Example.SetActive(false);
        CurrentVisuals = new GameObject[13];
        Result = Instantiate(Example);
        
        for (int i = 0; i < 13; i++)
        {
            CurrentVisuals[i] = Instantiate(Example);
            CurrentVisuals[i].GetComponent<SpriteRenderer>().sprite = ingrs[i];
        }
        WhatIHave = new int[13];
        foreach (Items itm in GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>().ingrList)
        {
            int index = 0;
            string[] names = { "Harina", "Levadura", "Leche", "Mantequilla", "Az�car", "Huevos", "Aceite", "Agua", "Lim�n", "Reques�n", "Almendra", "Boniato", "Calabaza" };
            while (names[index] != itm.type) { index++; }
            WhatIHave[index] = itm.amount;
        }
        Camara = GameObject.FindGameObjectWithTag("MainCamera").transform;
        Result.transform.position = new Vector3(3, 2, 0) + new Vector3(Camara.position.x, 0, 0);
        Result.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        Result.transform.GetChild(0).gameObject.SetActive(false);
    }
    static public void SetNumbers(int[] paid, int res)
    {
        WhatINeed = paid;
        AreActive = 0;
        int i;
        for(i = 0; i < WhatINeed.Length; i++)
        {
            CurrentVisuals[i].SetActive(WhatINeed[i] > 0);
            if (CurrentVisuals[i].activeSelf)
            {
                CurrentVisuals[i].transform.GetChild(0).GetComponent<TextMeshPro>().text = WhatIHave[i].ToString() + "/" + WhatINeed[i].ToString();
                AreActive++;
                CurrentVisuals[i].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                CurrentVisuals[i].transform.position = new Vector3((AreActive - 1) * (-0.5f)*1.5f, 2, 0);
                CurrentVisuals[i].transform.position += new Vector3(Camara.position.x, 0, 0);
            }
        }
        Result.GetComponent<SpriteRenderer>().sprite = recipesStat[res];
        Result.SetActive(true);
        for (int j = i; j < CurrentVisuals.Length; j++) { CurrentVisuals[j].SetActive(false); }
    }
    /*static private GameObject Example;
    [HideInInspector]static private GameObject[] Bar;
    static private int AreActive = 0;
    static public Transform Camara;
    private void Start()
    {
        Bar = new GameObject[13];
        Example = gameObject.transform.GetChild(2).gameObject;
        Example.SetActive(false);
        for(int i = 0; i < Bar.Length; i++) { Bar[i] = Instantiate(Example); }
        Camara = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    static public void AddItemToBar(Sprite sp, int index, int q)
    {
        if (!Bar[index].activeSelf)
        {
            Bar[index].SetActive(true);
            AreActive++;
        }
        Bar[index].GetComponent<SpriteRenderer>().sprite = sp;
        Bar[index].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        float offset = (AreActive-1) * (-0.5f);
        for(int i = 0; i < Bar.Length; i++)
        {
            if (Bar[i].activeSelf) 
            {
                Bar[i].transform.position = new Vector3(offset, 3, 0);
                Bar[i].transform.position += new Vector3(Camara.position.x, 0, 0);
                offset += 1;
                Bar[index].transform.GetChild(0).GetComponent<TextMeshPro>().text = q.ToString();
            }
            
            
        }
    }
    static public void BarVisibility()
    {
        for(int i = 0; i < Bar.Length; i++)
        {
            Bar[i].SetActive(false);
            Bar[i].transform.GetChild(0).GetComponent<TextMeshPro>().text = "";
        }
        AreActive = 0;
    }

    /*
    //Hay que pasar los ingredientes por cada barra? Por c�digo o alg�n fichero?
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
    }*/
}
