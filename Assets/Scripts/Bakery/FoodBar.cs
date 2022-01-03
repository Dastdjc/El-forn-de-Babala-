using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FoodBar : MonoBehaviour
{
    public Canvas Example;
    public Sprite[] ingrs;
    public Sprite[] recipes;
    private int[] WhatINeed;
    private int[] WhatIHave;
    private GameObject Result;
    private GameObject[] Ingredients;
    private GameObject[] Numbers;
    private int AreActive;
    private Transform Camara;

    public void Initiate()
    {
        Ingredients = new GameObject[13];
        Numbers = new GameObject[13];
        for (int i = 0; i < ingrs.Length; i++)
        {
            Ingredients[i] = Instantiate(Example.transform.GetChild(1).gameObject);
            Ingredients[i].GetComponent<Image>().sprite = ingrs[i];
            Numbers[i] = Instantiate(Example.transform.GetChild(2).gameObject);
            Numbers[i].transform.SetParent(Ingredients[i].transform);
            Numbers[i].SetActive(true);
            Ingredients[i].SetActive(false);
        }
        Result = Instantiate(Example.transform.GetChild(0).gameObject);
        Result.GetComponent<Image>().sprite = recipes[0];
        Camara = GameObject.FindGameObjectWithTag("MainCamera").transform;
        Result.transform.position = new Vector3(3, 2, 0) + new Vector3(Camara.position.x, 0, 0);
        Result.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        Result.transform.GetChild(0).gameObject.SetActive(false);
        WhatIHaveRefresh();
        

    }
    public void WhatIHaveRefresh()
    {
        WhatIHave = new int[13];
        foreach (Items itm in GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>().ingrList)
        {
            int index = 0;
            string[] names = { "Harina", "Levadura", "Leche", "Mantequilla", "Azúcar", "Huevos", "Aceite", "Agua", "Limón", "Requesón", "Almendra", "Boniato", "Calabaza" };
            while (names[index] != itm.type) { index++; }
            WhatIHave[index] = itm.amount;
        }
    }
    public void SetNumbers(int[] paid, int res)
    {
        WhatINeed = paid;
        AreActive = 0;
        int i;
        for(i = 0; i < WhatINeed.Length; i++)
        {
            Ingredients[i].SetActive(WhatINeed[i] > 0);
            if (Ingredients[i].activeSelf)
            {
                Numbers[i].GetComponent<TextMeshPro>().text = WhatIHave[i].ToString() + "/" + WhatINeed[i].ToString();
                AreActive++;
                Ingredients[i].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                Ingredients[i].transform.position = new Vector3((AreActive - 1) * (-0.5f)*1.5f, 2, 0);
                Ingredients[i].transform.position += new Vector3(Camara.position.x, 0, 0);
            }
        }
        Result.GetComponent<SpriteRenderer>().sprite = recipes[res];
        Result.SetActive(true);
        //for (int j = i; j < Ingredients.Length; j++) { Ingredients[j].SetActive(false); }
        
    }
}
