using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CustomerController : MonoBehaviour
{
    public GameObject me;

    private RectTransform Mask;
    private SpriteRenderer image;
    private float satisfacton = 0;
    private int state;
    //State == 0 cuando entra a la panadería
    //State == 1 cuando pide algo y empieza a cansarse
    //State == 2 cuando se le ha acabado la paciencia o le has dado lo que quería y se va
    enum Recetas
    {
        Mona = 0,
        Sandwich  = 1,
        Rosquilletas = 2
    }
    private Recetas[] command;



    void Start()
    {
        command = new Recetas[Random.Range(1, 4)];
        for(int i = 0; i < command.Length; i++)
        {
            command[i] = new Recetas();
            command[i] = (Recetas)Random.Range(0, 3);
        }
        PrintCommand();
        Talk(false);
        Mask = me.transform.GetChild(1).gameObject.GetComponent<RectTransform>();
        image = Mask.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        Mask.localScale = new Vector3(satisfacton, 0.2f, 1);
    }
    void FixedUpdate()
    {
        if(state == 0)
        {
            this.transform.position = this.transform.position + new Vector3(0.1f, 0, 0);
            if (this.transform.position.x >= 1) { state++; }
        }
        else if(state == 1)
        {
            satisfacton += 0.001f;
            Mask.localScale = new Vector3(satisfacton, 0.2f, 1);
            image.color = new Color(satisfacton, 1 - satisfacton, 0);
            if (satisfacton >= 1) { state++; Talk(false); }
        }
        else
        {
            this.transform.position = this.transform.position + new Vector3(-0.1f, 0, 0);
            if (this.transform.position.x <= -10) { Destroy(me); }
        }
        
    }
    public void OnMouseOver()
    {
        if(state == 1)
            Talk(true);
    }
    public void OnMouseExit()
    {
        if (state == 1)
            Talk(false);
    }

    public void PrintCommand()
    {
        TextMeshPro txt = me.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>();
        txt.text = "";
        for (int i = 0; i < command.Length; i++)
        {
            txt.text += command[i].ToString();
            txt.text += "\n";
        }
    }
    public void Talk(bool appear)
    {
        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(appear);
        }
    }
}
