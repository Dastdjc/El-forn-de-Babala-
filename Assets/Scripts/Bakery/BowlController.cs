using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BowlController : MonoBehaviour
{
    static public int[][] IngPerRecipe;
    static public int selected = 0;
    public GameObject Texto;
    public AudioClip[] audioResults;
    private bool touchingPlayer;
    private int HUDState;
    private int somethingInside = -2;
    private int coockState = -1;
    private float timer;
    public GameObject player;
    public AudioSource bowlSound;
    public AudioSource sourceResults;
    public AudioSource BGMusic;
    private bool startedCooking = false;
    void Start()
    {
        setIngrRecp();
        gameObject.GetComponent<FoodBar>().Initiate();
        gameObject.GetComponent<FoodBar>().SetNumbers(IngPerRecipe[selected], selected);
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
        if (!GameObject.FindGameObjectWithTag("Inventory").transform.GetChild(0).gameObject.activeSelf)
        {
            ManageHUD();
            int selec = selected;
            if (HUDState == 3 && selected > 0 && (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))) { selected--; }
            else if (HUDState == 3 && selected < 8 && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))) { selected++; }
            else if (HUDState == 3 && Input.GetKeyDown(KeyCode.E))//Pasar del inventario a la olla
            {
                somethingInside = DeterminateFood();
                if (somethingInside != -2)
                {
                    gameObject.GetComponent<Animator>().SetTrigger("Change");//Pasa a Amarillo
                    coockState = 0;
                    HUDState++;
                }
            }
            else if (HUDState == 1 && Input.GetKeyDown(KeyCode.E))//Pasa de la olla al inventario
            {
                if (somethingInside != -2)
                {
                    startedCooking = false;
                    // Sonido
                    bowlSound.Stop();
                    sourceResults.clip = audioResults[coockState];
                    sourceResults.Play();
                    if (!SpawnCustomers.specialSpawned)
                    StartCoroutine(AudioFadeOut.FadeIn(BGMusic, 2f));

                    gameObject.GetComponent<Animator>().SetTrigger("ToIdle");
                    PassToInv(somethingInside, coockState);
                    somethingInside = -2;
                    coockState = -1;
                }
            }

            //Una vez ha pasado a la olla aqui se calcula como de hecho est?
            if (coockState == -1 && selec != selected) { gameObject.GetComponent<FoodBar>().SetNumbers(IngPerRecipe[selected], selected); }
            //-------------------//
            // Animaci?n caldero//
            //-----------------//
            else if (somethingInside != -2 && coockState > -1 && coockState < 3)
            {
                if (!startedCooking) 
                {
                    startedCooking = true;
                    StartCoroutine(AudioFadeOut.FadeOut(BGMusic, 1f));
                    bowlSound.PlayDelayed(1f);
                }
                timer += Time.deltaTime * (coockState + 1) / 5;
                if (timer > 0.7f)
                {
                    timer = 0;
                    gameObject.GetComponent<Animator>().SetTrigger("Change");//Pasa al siguiente color
                    coockState++;
                }
            }
            else if(coockState >= 3) { Texto.SetActive(false); }
        }
    }
    private void ManageHUD()
    {
        if (touchingPlayer && Input.GetKeyDown(KeyCode.E))
        {
            if (HUDState == 0)
            {
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
                player.GetComponent<Animator>().SetFloat("speed", 0f);
                player.transform.GetChild(0).gameObject.SetActive(true);
                player.GetComponent<PlayerMovement>().enabled = false;
                //player.GetComponent<PlayerMovement>().dashSpeed = 0;

                Texto.SetActive(false);
                HUDState++;
            }
        }
        if (HUDState == 1 && somethingInside == -2) { HUDState++; }
        if (HUDState == 2 && gameObject.GetComponent<FoodBar>().Example.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition.y > 360)
        {
            gameObject.GetComponent<FoodBar>().Example.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition -= new Vector2(0, 500 * Time.deltaTime);
        }
        else if (HUDState == 2 && gameObject.GetComponent<FoodBar>().Example.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition.y <= 400) 
        {
            HUDState++;
            gameObject.GetComponent<FoodBar>().SetNumbers(IngPerRecipe[selected], selected);
        }

        if (touchingPlayer && Input.GetKeyDown(KeyCode.Escape))
        {
            if (HUDState == 3) 
            {
                HUDState++;
                player.GetComponent<PlayerMovement>().enabled = true;
            }
            /*if(HUDState == 0) 
            {
                if (somethingInside == -2) Texto.SetActive(true);
                player.GetComponent<PlayerMovement>().enabled = true; 
            }*/
        }
        if (HUDState == 4 && gameObject.GetComponent<FoodBar>().Example.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition.y < 750)
        {
            gameObject.GetComponent<FoodBar>().Example.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition += new Vector2(0, 500 * Time.deltaTime);
        }
        else if (HUDState == 4 && gameObject.GetComponent<FoodBar>().Example.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition.y >= 750) 
        {
            HUDState = 0;
        }
    }
    private void PassToInv(int index, int state)
    {
        string[] names = { "Mona de Pascua", "Fartons", "Farinada", "Bunyols de calabaza", "Pilotes de frare", "Flaons", "Coca de llanda", "Pasteles de boniato", "Mocador?" };
        Recipe aux = ScriptableObject.CreateInstance<Recipe>();
        aux.amount = 1;
        aux.Coock = new Queue<int>();
        aux.Coock.Enqueue(state);
        if (index != -1 && state != 3 && state != 0)aux.type = names[index];
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
         * 9)Mocador?
         */
        int[] auxArray = new int[13];
        bool oneUnless = false;
        //Solo determina la posicion en mi array auxiliar
        foreach (Items itm in GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>().ingrList)
        {
            if(itm.amount > 0)oneUnless = true;
            int index = 0;
            string[] names = { "Harina", "Levadura", "Leche", "Mantequilla", "Az?car", "Huevos", "Aceite", "Agua", "Lim?n", "Reques?n", "Almendra", "Boniato", "Calabaza" };
            while (names[index] != itm.type) { index++; }
            auxArray[index] = itm.amount;
        }
        for (int i = 0; i < GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>().ingrList.Count; i++)
        {
            Items itm = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>().ingrList[i];
            int index = 0;
            string[] names = { "Harina", "Levadura", "Leche", "Mantequilla", "Az?car", "Huevos", "Aceite", "Agua", "Lim?n", "Reques?n", "Almendra", "Boniato", "Calabaza" };
            while (names[index] != itm.type) { index++; }
            if (itm.amount > 0 && itm.amount >= IngPerRecipe[selected][index])
            {
                Inventory.Instance.SubstractIngrItem(itm, IngPerRecipe[selected][index]);
            }
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
    private void OnTriggerEnter2D(Collider2D collision) { touchingPlayer = true; if(somethingInside == -2)Texto.SetActive(true); }
    private void OnTriggerExit2D(Collider2D collision) { touchingPlayer = false; Texto.SetActive(false); }
}
