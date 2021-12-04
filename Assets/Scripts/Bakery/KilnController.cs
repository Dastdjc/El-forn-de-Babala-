using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KilnController : MonoBehaviour
{
    private bool open = false;
    private GameObject[] objectEntering = new GameObject[6];
    public GameObject FoodToCook;
    public Sprite[] FoodVisuals;
    public int[] foodIndex = new int[6];
    private SpriteRenderer[] ColorBar = new SpriteRenderer[6];
    private Transform[] Mask = new Transform[6];
    static public int electricity = 2;
    private float timer;
    static public int[] isCoock = new int[6];
    static private Color secondColor = new Color(1, 0.3f, 0);
    public GameObject Inventory;
    private void Start() 
    {
        if(open) gameObject.GetComponent<SpriteRenderer>().color = secondColor;
        else gameObject.GetComponent<SpriteRenderer>().color = new Color(0.25f, 0, 1);
    }
    private void OnMouseDown()
    {
        if (open)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0.25f, 0, 1);
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 4;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = secondColor;
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
        open = !open;
    }
    private void Update()
    {
        if(Time.timeScale == 1 && electricity > 0)
        {
            for (int i = 0; i < objectEntering.Length; i++)
            {
                if (objectEntering[i] != null && Mask[i] != null)
                {
                    float position = Mask[i].transform.localPosition.x;
                    if (position > 0.30f && position < 0.70f) { ColorBar[i].color += new Color(0, 0.0002f, -0.0001f); isCoock[i] = 1; }
                    else if (position > 0.70f) { ColorBar[i].color += new Color(0.0001f, -0.0004f, 0); isCoock[i] = 2; }
                    Mask[i].transform.position += new Vector3(0.00002f * electricity, 0, 0);
                    if (position > 1) { Destroy(Mask[i].gameObject); }
                }
                else if (objectEntering[i] == null)
                {
                    //send isCoock[i] to inventory
                    //send foodIndex[i] to inventory
                }
            }
            if (electricity >= 2)
            {
                timer += 0.01f;
                if (timer > 100 && Random.Range(0, 1000) == 1)
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
        int i = 0;
        while (i < objectEntering.Length && objectEntering[i] != null) { i++; }
        if (i < objectEntering.Length)
        {
            objectEntering[i] = Instantiate(FoodToCook);
            foodIndex[i] = selectSprite;
            objectEntering[i].gameObject.layer = 7;
            objectEntering[i].transform.localScale = new Vector3(0.5f, 0.5f, 1);
            if(selectSprite != -1)objectEntering[i].GetComponent<SpriteRenderer>().sprite = FoodVisuals[selectSprite];

            if (i > 2)
                objectEntering[i].transform.position = new Vector3(i - 3 * 1 + 28.125f, -2.2f, 0);
            else
                objectEntering[i].transform.position = new Vector3(i * 1 + 28.125f, -1.2f, 0);


            objectEntering[i].GetComponent<SpriteRenderer>().sortingOrder = 3;
            ColorBar[i] = objectEntering[i].transform.GetChild(1).GetComponent<SpriteRenderer>();
            Mask[i] = objectEntering[i].transform.GetChild(0);
            isCoock[i] = 0;
            secondColor = new Color(1, 0.3f, 0);
            ColorBar[i].color = secondColor;
            string[] names = { "Mona", "Fartons", "Farinada", "Bunyols de calabaza", "Pilotes de frare", "Flaons", "Coca de llanda", "Pasteles de boniato", "Mocadorà" };
            Recipe aux = ScriptableObject.CreateInstance<Recipe>();
            aux.amount = 1;
            aux.type = names[selectSprite];
            objectEntering[i].GetComponent<IWantToDie>().my = aux;
            objectEntering[i].GetComponent<IWantToDie>().Inventory = Inventory;
        }
    }
    public bool ImOpen() { return open; }
    static public void ReturnElec()
    {
        electricity = 2;
        secondColor = new Color(1, 0.3f, 0);
    }
}
