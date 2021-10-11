//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FoodBar : MonoBehaviour
{
    public Animator Controller;
    public GameObject Example;
    static private GameObject[] Bar;
    private void Start()
    {
        Bar = new GameObject[5];
        string[] names = { "Mona", "Sandwich", "Rosquilletas", "Fartons", "Bunyols" };
        for (int i = 0; i < Bar.Length; i++)
        {
            Bar[i] = Instantiate(Example);
            Bar[i].transform.position = Bar[i].transform.position + new Vector3((i + 1) * 2 / (Bar.Length * 0.15f) + 3, 1.5f, 0);
            Bar[i].gameObject.GetComponent<SpriteRenderer>().color = new Color((float)i / Bar.Length, 0, (float)Bar.Length - i / Bar.Length);
            Bar[i].gameObject.transform.GetChild(1).GetComponent<TextMeshPro>().color = new Color((float)Bar.Length - i / Bar.Length, 0.5f, (float)i / Bar.Length);
            Bar[i].gameObject.transform.GetChild(1).GetComponent<TextMeshPro>().text = names[i];
            Bar[i].transform.SetParent(gameObject.transform, false);
            
            Bar[i].SetActive(true);
        }
    }
    private void OnMouseDown()
    {
        Controller.SetTrigger("MouseTouch");
        bool dir = Controller.GetBool("Up");
        dir = !dir;
        Controller.SetBool("Up", dir);
    }
    public void ResetTrigger()
    {
        Controller.ResetTrigger("MouseTouch");
    }
    static public void WriteCommand(int index)
    {
        string Text;
        Text = Bar[index].gameObject.transform.GetChild(0).GetComponent<TextMeshPro>().text;
        int num = GetNums(Text);
        num++;
        Bar[index].gameObject.transform.GetChild(0).GetComponent<TextMeshPro>().text = "Pedidos x" + num.ToString() + "\n Tienes x0";
        //num = GetNums(Text, 15);
        //Bar[i].gameObject.transform.GetChild(0).GetComponent<TextMeshPro>().text += num.ToString();
    }
    static private int GetNums(string txt, int index = 0)
    {
        int resul = 0;
        bool isChar = false;
        while(index < txt.Length && !isChar)
        {
            if ("0123456789".IndexOf(txt[index]) != -1)
            {
                resul = resul * 10 + int.Parse("" + txt[index]);
                if ("0123456789".IndexOf(txt[index + 1]) == -1) { isChar = true; }
            }
            index++;
        }
        return resul;
    }
}
