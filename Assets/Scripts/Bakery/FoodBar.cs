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
    private GameObject[] Result;
    private GameObject[] Ingredients;
    private GameObject[] Numbers;
    private int AreActive;
    private string[] IngrNames;
    private string[] RecipeNames;

    public void Initiate()
    {
        //Debug.Log(recipes.Length);
        IngrNames = new string[] { "Harina", "Levadura", "Leche", "Mantequilla", "Azúcar", "Huevos", "Aceite", "Agua", "Limón", "Requesón", "Almendras", "Boniatos", "Calabaza" };
        RecipeNames = new string[] { "Mona de Pascua", "Fartons", "Farinada", "Bunyols de calabaza", "Pilotes de frare", "Flaons", "Coca de llanda", "Pasteles de boniato", "Mocadorà" };
        if (Ingredients == null && Numbers == null)
        {
            Ingredients = new GameObject[13];
            Result = new GameObject[3];
            Numbers = new GameObject[14];
            for(int i = 0; i < Result.Length; i++)
            {
                Result[i] = Instantiate(Example.transform.GetChild(1).gameObject);
                Result[i].SetActive(true);
                if (i != 0) Result[i].GetComponent<Image>().sprite = recipes[i - 1];
                else { 
                    Result[i].GetComponent<Image>().sprite = null;
                    Result[i].GetComponent<Image>().color = new Color(0, 0, 0, 0);
                }
                if(i != 1)
                {
                    Result[i].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                }
                Result[i].transform.SetParent(Example.transform.GetChild(0));
                Result[i].transform.localPosition = new Vector3(-450, (i - 1) * 100, 0);
            }
            
            for (int i = 0; i < ingrs.Length + 1; i++)
            {
                Numbers[i] = Instantiate(Example.transform.GetChild(2).gameObject);
                Numbers[i].SetActive(true);
                if (i < Ingredients.Length)
                {
                    Ingredients[i] = Instantiate(Example.transform.GetChild(1).gameObject);
                    Ingredients[i].GetComponent<Image>().sprite = ingrs[i];
                    Ingredients[i].transform.SetParent(Example.transform.GetChild(0));
                    Ingredients[i].transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                    Ingredients[i].SetActive(true);
                    Numbers[i].transform.SetParent(Ingredients[i].transform);
                }
                else 
                {
                    Numbers[13].transform.SetParent(Result[1].transform);
                    Numbers[13].transform.localPosition = new Vector3(150, 0, 0);
                    Numbers[13].SetActive(true);
                }
            }
        }
        WhatIHaveRefresh();
    }
    public void WhatIHaveRefresh()
    {
        WhatIHave = new int[13];
        foreach (Items itm in GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>().ingrList)
        {
            int index = 0;
            while (IngrNames[index] != itm.type) { index++; }
            WhatIHave[index] = itm.amount;
        }
    }
    public void SetNumbers(int[] paid, int res)
    {
        WhatINeed = paid;
        AreActive = 0;
        for(int i = 0; i < WhatINeed.Length; i++)
        {
            Ingredients[i].SetActive(WhatINeed[i] > 0);
            if (Ingredients[i].activeSelf)
            {
                Numbers[i].GetComponent<TextMeshProUGUI>().text = IngrNames[i] + '\n' + WhatIHave[i].ToString() + "/" + WhatINeed[i].ToString();
                AreActive++;
                //Ingredients[i].transform.localPosition = new Vector3((AreActive - 1) * (-0.5f)*1.5f, 2, 0);
                Ingredients[i].transform.localPosition = new Vector3((AreActive - 1) * 85 - 150, 2, 0);
                //Ingredients[i].transform.position += new Vector3(Camara.position.x, 0, 0);
            }
        }
        for (int j = -1; j < 2; j++)
        {
            if (!((res == 0 && j == -1) || (res == 8 && j == 1)))
            {
                Result[j + 1].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                Result[j + 1].GetComponent<Image>().sprite = recipes[res + j];
            }
            else
            {
                Result[j + 1].GetComponent<Image>().color = new Color(0, 0, 0, 0);
            }
        }
        
        Numbers[13].GetComponent<TextMeshProUGUI>().text = RecipeNames[res];
    }
}
