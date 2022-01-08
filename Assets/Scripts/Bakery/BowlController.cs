using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BowlController : MonoBehaviour
{
    static public int[][] IngPerRecipe;
    static public int selected = 0;
    public GameObject Texto;
    private bool touchingPlayer;
    private int somethingInside = -2;
    private int coockState = -1;
    private float timer;
    void Start()
    {
        setIngrRecp();
        gameObject.GetComponent<FoodBar>().Initiate();
        gameObject.GetComponent<FoodBar>().SetNumbers(IngPerRecipe[selected],selected);
        Texto.GetComponent<TextMeshPro>().text = "Pulsa E para poner ingredientes";
        Texto.SetActive(false);
        //gameObject.GetComponent<FoodBar>().Example.gameObject.SetActive(true);
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
    private void Update()
    {
        /*
         * <==Funcionamiento de la barra==>>
         * A comprobar:
         *      Si el jugador está tocando el bol
         *      Si ha pulsado E
         *      Si ha pulsado ESC
         *  Pasa:
         *      Si está tocando del bol y pulsa E:
         *          Se activa la barra y el booleano del inventario
         *      Si lo anterior ha ocurrido y pulsa ESC:
         *          Se desactiva la barra y el booleano
         */
        if (touchingPlayer && Input.GetKeyDown(KeyCode.E))
        {
            if (!Texto.activeSelf) 
            {
                gameObject.GetComponent<FoodBar>().ActivateAnimation(true);
            }
            Texto.SetActive(false);
            //Time.timeScale = 0;
        }
        if (touchingPlayer && Input.GetKeyDown(KeyCode.Escape) && !Texto.activeSelf)
        {
            if (!gameObject.GetComponent<FoodBar>().ReturnBarState())
            {
                Texto.SetActive(true);
                //Time.timeScale = 1;
            }
            else
            {
                gameObject.GetComponent<FoodBar>().ActivateAnimation(false);
            }
        }
        if (!Texto.activeSelf)
        {
            int selec = selected;
            if (selected > 0 && Input.GetKeyDown(KeyCode.DownArrow)) { selected--; }
            else if (selected < 8 && Input.GetKeyDown(KeyCode.UpArrow)) { selected++; }
            else if (Input.GetKeyDown(KeyCode.Q))//Pasar del inventario a la olla
            {
                somethingInside = DeterminateFood();
                gameObject.GetComponent<Animator>().SetTrigger("Change");//Pasa a Amarillo
                coockState = 0;
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                if (somethingInside != -2)
                {
                    gameObject.GetComponent<Animator>().SetTrigger("ToIdle");
                    PassToInv(somethingInside);
                    somethingInside = -2;
                    coockState = -1;
                }
            }
            //Una vez ha pasado a la olla aqui se calcula como de hecho está
            if (coockState == -1 && selec != selected) { gameObject.GetComponent<FoodBar>().SetNumbers(IngPerRecipe[selected], selected); }
            else if (somethingInside != -2 && coockState > -1 && coockState < 3)
            {
                timer += Time.deltaTime * (coockState + 1) / 5;
                if (timer > 2)
                {
                    timer = 0;
                    gameObject.GetComponent<Animator>().SetTrigger("Change");//Pasa al siguiente color
                    coockState++;
                }
            }

        }
        
    }
    private void PassToInv(int index)
    {
        string[] names = { "Mona de Pascua", "Fartons", "Farinada", "Bunyols de calabaza", "Pilotes de frare", "Flaons", "Coca de llanda", "Pasteles de boniato", "Mocadorà" };
        Recipe aux = ScriptableObject.CreateInstance<Recipe>();
        aux.amount = 1;
        if (index != -1)aux.type = names[index];
        else aux.type = "Basura";
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
    
    private bool IsEnough(int[] q) 
    {
        bool isOnbowl = true;
        int i = 0;
        while(i < q.Length && isOnbowl)
        {
            if (IngPerRecipe[selected][i] > q[i]) isOnbowl = false;
            i++;
        }
        return isOnbowl; 
    }
    private void OnTriggerEnter2D(Collider2D collision) { touchingPlayer = true; Texto.SetActive(true); }
    private void OnTriggerExit2D(Collider2D collision) { touchingPlayer = false; Texto.SetActive(false); }
}
