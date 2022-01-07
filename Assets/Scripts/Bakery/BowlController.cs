using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlController : MonoBehaviour
{
    private Transform Content;
    static public int[][] IngPerRecipe;
    static public int selected = 0;
    static private float Upmov = 0.05f;

    private int somethingInside = -2;
    private int cookState = -1;
    public SpriteRenderer ColorBar;
    //public float relativeX;

    //public KilnController Kiln;
    void Start()
    {
        ColorBar.gameObject.SetActive(false);
        setIngrRecp();
        Content = transform.GetChild(0);
        Content.localPosition = new Vector3(0, -0.9f, 0);
        gameObject.GetComponent<FoodBar>().SetNumbers(IngPerRecipe[selected],selected);
        //relativeX = GameObject.FindGameObjectWithTag("MainCamera").transform.position.x;
    }
    private void Update()
    {
        int selec = selected;
        if (selected > 0 && Input.GetKeyDown(KeyCode.DownArrow)) { selected--; }
        else if (selected < 8 && Input.GetKeyDown(KeyCode.UpArrow)) { selected++; }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        { //Pasar del inventario a la olla
            somethingInside = DeterminateFood();
            ActivateCookBar(); 
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (somethingInside != -2)
            {
                PassToInv(somethingInside, cookState);
                somethingInside = -2;
                ColorBar.gameObject.SetActive(false);
            }
        }
        //Una vez ha pasado a la olla aqui se calcula como de hecho está
        if (cookState == -1 && selec != selected) { gameObject.GetComponent<FoodBar>().SetNumbers(IngPerRecipe[selected],selected); }
        else if(cookState > -1 && ColorBar.transform.localPosition.x < 2.25f){
            ColorBar.transform.localPosition += new Vector3(0.002f, 0, 0);
            if (ColorBar.transform.localPosition.x > 1)//Muy hecho
            {
                cookState = 2;
                ColorBar.color = new Color(0.8f, 0, 0);
            }
            else if (ColorBar.transform.localPosition.x > -0.4f)//En el punto
            {
                cookState = 1;
                ColorBar.color = new Color(0, 0.8f, 0);
            }
            //Normal
        }
    }
    private void PassToInv(int index, int state)
    {
        string[] names = { "Mona de Pascua", "Fartons", "Farinada", "Bunyols de calabaza", "Pilotes de frare", "Flaons", "Coca de llanda", "Pasteles de boniato", "Mocadorà" };
        Recipe aux = ScriptableObject.CreateInstance<Recipe>();
        aux.amount = 1;
        if (index != -1)aux.type = names[index];
        else aux.type = "Basura";
        Debug.Log(state);
        GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>().AddRecipe(aux);
    }
    public int DeterminateFood()
    {
        /*
         * 1)Mona
         * 2)Fartons
         * 3)Farinada
         * 4)Bunyols
         * 5)Pilotes de Frare
         * 6)Flaons
         * 7)Coca de llanda
         * 8)Pasteles de boniato
         * 9)Mocadorá
         */
        int[] auxArray = new int[13];
        bool oneUnless = false;
        foreach (Items itm in GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>().ingrList)
        {
            oneUnless = true;
            int index = 0;
            string[] names = { "Harina", "Levadura", "Leche", "Mantequilla", "Azúcar", "Huevos", "Aceite", "Agua", "Limón", "Requesón", "Almendra", "Boniato", "Calabaza" };
            while (names[index] != itm.type) { index++; }
            auxArray[index] = itm.amount;
        }

        foreach (Items itm in GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>().ingrList)
        {
            int index = 0;
            string[] names = { "Harina", "Levadura", "Leche", "Mantequilla", "Azúcar", "Huevos", "Aceite", "Agua", "Limón", "Requesón", "Almendra", "Boniato", "Calabaza" };
            while (names[index] != itm.type) { index++; }
            if (itm.amount > 0 && itm.amount > IngPerRecipe[selected][index])
                itm.amount -= IngPerRecipe[selected][index];
            else itm.amount = 0;
        }
        gameObject.GetComponent<FoodBar>().WhatIHaveRefresh();
        gameObject.GetComponent<FoodBar>().SetNumbers(IngPerRecipe[selected], selected);
        if (IsEnough(auxArray))return selected;
        if (oneUnless)return -1;
        return -2;
    }
    private void setIngrRecp()
    {
        IngPerRecipe = new int[9][];
        IngPerRecipe[0] = new int[] { 5, 1, 1, 2, 2, 1, 0, 0, 0, 0, 0, 0, 0 };
        IngPerRecipe[1] = new int[] { 5, 2, 0, 0, 2, 3, 2, 2, 0, 0, 0, 0, 0 };
        IngPerRecipe[2] = new int[] { 5, 1, 0, 0, 3, 3, 2, 2, 1, 1, 0, 0, 0 };
        IngPerRecipe[3] = new int[] { 5, 0, 2, 0, 2, 2, 0, 0, 1, 0, 0, 0, 1 };
        IngPerRecipe[4] = new int[] { 5, 2, 4, 0, 2, 1, 2, 0, 1, 0, 0, 0, 0 };
        IngPerRecipe[5] = new int[] { 5, 0, 0, 0, 2, 2, 4, 0, 0, 4, 2, 0, 0 };
        IngPerRecipe[6] = new int[] { 5, 0, 8, 0, 6, 5, 1, 0, 1, 0, 0, 0, 0 };
        IngPerRecipe[7] = new int[] { 0, 0, 0, 0, 3, 1, 2, 0, 0, 0, 0, 4, 0 };
        IngPerRecipe[8] = new int[] { 0, 0, 0, 0, 6, 1, 0, 2, 1, 6, 6, 0, 0 };
    }
    private bool IsEnough(int[] q) 
    {
        bool isOnbowl = true;
        int i = 0;
        while(i < q.Length && isOnbowl)
        {
            if (IngPerRecipe[selected][i] > q[i]) isOnbowl = false;
            i++;
        }
        if (isOnbowl) Resset();
        return isOnbowl; 
    }
    private void Resset() { Content.localPosition = new Vector3(0, -0.9f, 0); }
    private void ActivateCookBar()
    {
        cookState = 0;
        ColorBar.gameObject.SetActive(true);
        ColorBar.color = new Color(0, 0, 0.8f);
        ColorBar.transform.localPosition = new Vector3(-2.25f, 0, 0);
    }
    /*public void PutIngredient(Items ingr)
    {
        int index = 0;
        //Debug.Log(ingr.type);
        string[] names = { "Harina", "Levadura", "Leche", "Mantequilla", "Azúcar", "Huevos", "Aceite", "Agua", "Limón", "Requesón", "Almendra", "Boniato", "Calabaza" };
        while (names[index] != ingr.type) { index++; }
        Content.transform.position += new Vector3(0, Upmov, 0);
        //if(ingredients[0] > 2)BackIgredients();
        int j = 0;
        for (int i = 0; i < GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>().ingrImagesList.Count; i++)
        {
            if (GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>().ingrImagesList[i].type == ingr.type) { j = i; }
        }
        //FoodBar.AddItemToBar(GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>().ingrImagesList[j].itemImage, index, ingredients[index]);
    }*/
}
