using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlController : MonoBehaviour
{
    private Transform Content;
    private int[] ingredients = new int[13];
    static public int[][] IngPerRecipe;
    public int selected = 0;
    static private float Upmov = 0.05f;
    //public KilnController Kiln;
    private void Awake()
    {
        setIngrRecp();
    }
    void Start()
    {
        setIngrRecp();
        Content = transform.GetChild(0);
        Content.localPosition = new Vector3(0, -0.9f, 0);
        //FoodBar.SetNumbers(IngPerRecipe[selected]);
        //ingredients = new int[13];
    }
    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.C) && GameObject.FindGameObjectWithTag("Horno").GetComponent<KilnController>().ImBusy())
        {
            GameObject.FindGameObjectWithTag("Horno").GetComponent<KilnController>().GetToCook(DeterminateFood());
            FoodBar.BarVisibility();
        }*/
        int selec = selected;
        if (selected > 0 && Input.GetKeyDown(KeyCode.DownArrow)) { selected--; }
        else if (selected < 8 && Input.GetKeyDown(KeyCode.UpArrow)) { selected++; }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) { }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) { IsEnough(IngPerRecipe[selected]); }

        if(selec != selected) { FoodBar.SetNumbers(IngPerRecipe[selected],selected); }
    }
    private int DeterminateFood()
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
        /*int i = 0;
        while (i < IngPerRecipe.Length && !IsEnough(IngPerRecipe[i]))
        {
            i++;
        }

        if (i < IngPerRecipe.Length) { return i; }*/
        return -1;
    }
    private void setIngrRecp()
    {
        IngPerRecipe = new int[9][];
        IngPerRecipe[0] = new int[] { 5, 1, 1, 2, 2, 1 };
        IngPerRecipe[1] = new int[] { 5, 2, 0, 0, 2, 3, 2, 2 };
        IngPerRecipe[2] = new int[] { 5, 1, 0, 0, 3, 3, 2, 2, 1, 1 };
        IngPerRecipe[3] = new int[] { 5, 0, 2, 0, 2, 2, 0, 0, 1, 0, 0, 0, 1 };
        IngPerRecipe[4] = new int[] { 5, 2, 4, 0, 2, 1, 2, 0, 1 };
        IngPerRecipe[5] = new int[] { 5, 0, 0, 0, 2, 2, 4, 0, 0, 4, 2 };
        IngPerRecipe[6] = new int[] { 5, 0, 8, 0, 6, 5, 1, 0, 1 };
        IngPerRecipe[7] = new int[] { 0, 0, 0, 0, 3, 1, 2, 0, 0, 0, 0, 4 };
        IngPerRecipe[8] = new int[] { 0, 0, 0, 0, 6, 1, 0, 2, 1, 6, 6 };
    }
    private bool IsEnough(int[] q) 
    {
        bool isOnbowl = true;
        int i = 0;
        while(i < q.Length && isOnbowl)
        {
            if (ingredients[i] < q[i]) isOnbowl = false;
            i++;
        }
        if (isOnbowl) Resset();
        return isOnbowl; 
    }
    private void Resset() 
    {
        Content.localPosition = new Vector3(0, -0.9f, 0);
        ingredients = new int[13];
    }
    public void BackIgredients()
    {
        string[] names = { "Harina", "Levadura", "Leche", "Mantequilla", "Azúcar", "Huevos", "Aceite", "Agua", "Limón", "Requesón", "Almendra", "Boniato", "Calabaza" };
        Inventory aux = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        for (int i = 0; i < 13; i++)
        {
            if(ingredients[i] > 0)
            {
                Items aux2 = ScriptableObject.CreateInstance<Items>();
                aux2.amount = ingredients[i];
                aux2.type = names[i];
                aux.AddIngrItem(aux2);
                for (int j = 0; j < ingredients[i]; j++)
                {
                    Content.position -= new Vector3(0, Upmov, 0);
                }
            }
        }
        Resset();
    }
    public void PutIngredient(Items ingr)
    {
        int index = 0;
        //Debug.Log(ingr.type);
        string[] names = { "Harina", "Levadura", "Leche", "Mantequilla", "Azúcar", "Huevos", "Aceite", "Agua", "Limón", "Requesón", "Almendra", "Boniato", "Calabaza" };
        while (names[index] != ingr.type) { index++; }
        ingredients[index] += 1;
        Content.transform.position += new Vector3(0, Upmov, 0);
        //if(ingredients[0] > 2)BackIgredients();
        int j = 0;
        for (int i = 0; i < GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>().ingrImagesList.Count; i++)
        {
            if (GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>().ingrImagesList[i].type == ingr.type) { j = i; }
        }
        //FoodBar.AddItemToBar(GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>().ingrImagesList[j].itemImage, index, ingredients[index]);
    }
}
