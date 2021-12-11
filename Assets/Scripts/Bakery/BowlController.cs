using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlController : MonoBehaviour
{
    private Transform Content;
    private int[] ingredients = new int[13];
    static private float Upmov = 0.05f;
    //public KilnController Kiln;
    void Start()
    {
        Content = transform.GetChild(0);
        Content.localPosition = new Vector3(0, -0.9f, 0);
        ingredients = new int[13];
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && GameObject.FindGameObjectWithTag("Horno").GetComponent<KilnController>().ImBusy())
        {
            GameObject.FindGameObjectWithTag("Horno").GetComponent<KilnController>().GetToCook(DeterminateFood());
            FoodBar.BarVisibility();
        }
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
        int[] recipe = { 5, 1, 1, 2, 2, 1 };
        if (IsEnough(recipe)) { return 0; }
        recipe = new int[]{ 5, 2, 0, 0, 2, 3, 2, 2 };
        if (IsEnough(recipe)) { return 1; }
        recipe = new int[] { 5, 1, 0, 0, 3, 3, 2, 2, 1, 1 };
        if (IsEnough(recipe)) { return 2; }
        recipe = new int[] { 5, 0, 2, 0, 2, 2, 0, 0, 1, 0, 0, 0, 1 };
        if (IsEnough(recipe)) { return 3; }
        recipe = new int[] { 5, 2, 4, 0, 2, 1, 2, 0, 1 };
        if (IsEnough(recipe)) { return 4; }
        recipe = new int[] { 5, 0, 0, 0, 2, 2, 4, 0, 0, 4, 2 };
        if (IsEnough(recipe)) { return 5; }
        recipe = new int[] { 5, 0, 8, 0, 6, 5, 1, 0, 1 };
        if (IsEnough(recipe)) { return 6; }
        recipe = new int[] { 0, 0, 0, 0, 3, 1, 2, 0, 0, 0, 0, 4 };
        if (IsEnough(recipe)) { return 7; }
        recipe = new int[] { 0, 0, 0, 0, 6, 1, 0, 2, 1, 6, 6 };
        if (IsEnough(recipe)) { return 8; }
        Resset();
        return -1;
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
        FoodBar.AddItemToBar(GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>().ingrImagesList[j].itemImage, index, ingredients[index]);
    }
}
