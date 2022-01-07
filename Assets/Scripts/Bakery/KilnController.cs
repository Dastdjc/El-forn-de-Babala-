using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KilnController : MonoBehaviour
{

    /*private bool open = false;
    private GameObject objectEntering;
    public GameObject FoodToCook;//Es un objeto con sprite renderer y lo uso para instanciarlo y un hijo con un sprite rectangular y otro hijo mascara
    
    private SpriteRenderer ColorBar;
    private Transform Mask;
    private float timer;
    */static public int electricity = 2;/*
    private KilnController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        Inventory = GameObject.FindGameObjectWithTag("Inventory");
    }

    private void Start() 
    {
        if(open) gameObject.GetComponent<SpriteRenderer>().color = secondColor;
        else gameObject.GetComponent<SpriteRenderer>().color = new Color(0.25f, 0, 1);
    }
    private void Update()
    {
        if(Time.timeScale == 1 && electricity > 0)
        {
            if(objectEntering != null && Mask != null)
            {
                float position = Mask.transform.localPosition.x;
                if (position > 0.30f && position < 0.70f) //En el punto
                {
                    ColorBar.color += new Color(0, 0.0002f, -0.0001f);
                }
                else if (position > 0.70f) //Pasado, quemado, se convierte en basura
                { 
                    ColorBar.color += new Color(0.0001f, -0.0004f, 0);
                    Recipe aux = ScriptableObject.CreateInstance<Recipe>();
                    aux.amount = 1;
                    aux.type = "Basura";
                }
                Mask.transform.position += new Vector3(0.00002f * electricity, 0, 0);
                if (position > 1)
                {
                    Destroy(Mask.gameObject);
                }
            }
            if (electricity >= 2)
            {
                timer += 0.01f;
                if (timer > 1000 && Random.Range(0, 1000) == 1)
                {
                    electricity = 0;
                    secondColor = new Color(0.35f, 0.26f, 0.16f);
                    if(open)gameObject.GetComponent<SpriteRenderer>().color = secondColor;
                    timer = 0;
                }
            }
        }
    }
    public void GetToCook(int selectSprite)
    {
        if (objectEntering == null)
        {
            objectEntering = Instantiate(FoodToCook);
            foodIndex = selectSprite;
            objectEntering.gameObject.layer = 7;
            objectEntering.transform.localScale = new Vector3(1, 1, 1);
            objectEntering.transform.position = new Vector3(29.3f, -1.6f, 0);


            objectEntering.GetComponent<SpriteRenderer>().sortingOrder = 3;
            objectEntering.GetComponent<SpriteRenderer>().color = new Color(0.88f, 0.58f, 0.27f);
            ColorBar = objectEntering.transform.GetChild(1).GetComponent<SpriteRenderer>();
            Mask = objectEntering.transform.GetChild(0);
            secondColor = new Color(1, 0.3f, 0);
            ColorBar.color = new Color(0, 0, 0.5f);
        }
    }
    public bool ImBusy() { return objectEntering == null; }
    public bool ImOpen() { return open; }*/
    static public void ReturnElec()
    {
        electricity = 2;
    }
    
}
