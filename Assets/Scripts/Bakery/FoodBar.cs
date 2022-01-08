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
    private Animator myController;

    public void Initiate()
    {
        Example.gameObject.SetActive(true);
        myController = Example.transform.GetChild(0).gameObject.GetComponent<Animator>();
        myController.enabled = true;
        //myController.enabled = false;
        IngrNames = new string[] { "Harina", "Levadura", "Leche", "Mantequilla", "Azúcar", "Huevos", "Aceite", "Agua", "Limón", "Requesón", "Almendras", "Boniatos", "Calabaza" };
        RecipeNames = new string[] { "Mona de Pascua", "Fartons", "Farinada", "Bunyols de calabaza", "Pilotes de frare", "Flaons", "Coca de llanda", "Pasteles de boniato", "Mocadorà" };
        Ingredients = new GameObject[ingrs.Length];
        Result = new GameObject[3];
        Numbers = new GameObject[ingrs.Length + 1];
        for (int i = 0; i < Result.Length; i++)
        {
            Result[i] = Instantiate(Example.transform.GetChild(1).gameObject);
            Result[i].SetActive(false);
            if (i != 0) Result[i].GetComponent<Image>().sprite = recipes[i - 1];
            else
            {
                Result[i].GetComponent<Image>().sprite = null;
                Result[i].GetComponent<Image>().color = new Color(0, 0, 0, 0);
            }
            if (i != 1)
            {
                Result[i].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
            Result[i].transform.SetParent(Example.transform.GetChild(0));
            Result[i].transform.localPosition = new Vector3(-450, (i - 1) * 100, 0);
            Result[i].transform.localScale = new Vector3(1, 1, 1);
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
                Ingredients[i].SetActive(false);
                Numbers[i].transform.SetParent(Ingredients[i].transform);
                Numbers[i].transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                Numbers[Ingredients.Length].transform.SetParent(Result[1].transform);
                Numbers[Ingredients.Length].transform.localPosition = new Vector3(150, 0, 0);
                Numbers[Ingredients.Length].SetActive(false);
                Numbers[i].transform.localScale = new Vector3(1, 1, 1);
            }

        }
        WhatIHaveRefresh();
        SetBarVisibility(true);
    }
    public void SetBarVisibility(bool state)
    {
        for (int i = 0; i < Ingredients.Length; i++) { Ingredients[i].SetActive(state); }
        for(int i = 0; i < Result.Length; i++) { Result[i].SetActive(state); }
        Numbers[Ingredients.Length].SetActive(state);

    }
    public void WhatIHaveRefresh()
    {
        WhatIHave = new int[13];
        foreach (Items itm in GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>().ingrList)
        {
            int index = 0;
            while (index < IngrNames.Length && IngrNames[index] != itm.type) { index++; }
            WhatIHave[index] = itm.amount;
        }
    }
    public void SetNumbers(int[] paid, int res)
    {
        WhatINeed = paid;
        AreActive = 0;
        for(int i = 0; i < ingrs.Length; i++)
        {
            Ingredients[i].SetActive(WhatINeed[i] > 0);
            if (Ingredients[i].activeSelf)
            {
                Numbers[i].GetComponent<TextMeshProUGUI>().text = IngrNames[i] + '\n' + WhatIHave[i].ToString() + "/" + WhatINeed[i].ToString();
                AreActive++;
                Ingredients[i].transform.localPosition = new Vector3((AreActive - 1) * 85 - 150, 2, 0);
            }
        }
        for (int j = -1; j < 2; j++)
        {
            if (!((res == 0 && j == -1) || (res == recipes.Length - 1 && j == 1)))
            {
                Result[j + 1].GetComponent<Image>().color = new Color(1, 1, 1, 1);
                Result[j + 1].GetComponent<Image>().sprite = recipes[res + j];
            }
            else
            {
                Result[j + 1].GetComponent<Image>().color = new Color(0, 0, 0, 0);
            }
        }
        
        Numbers[Ingredients.Length].GetComponent<TextMeshProUGUI>().text = RecipeNames[res];
    }
    public void ActivateAnimation(bool direc)
    {
        myController.enabled = true;
        myController.SetBool("Direction", direc);
        myController.SetTrigger("ActivateAnim");
    }
    public bool ReturnBarState() 
    {
        myController.enabled = true;
        bool resul = myController.GetBool("Direction");
        myController.enabled = false;
        return resul;
    }
}
