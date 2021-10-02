using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CustomerController : MonoBehaviour
{
    public TextMeshPro txt;
    public RectTransform Mask;
    public SpriteRenderer image;
    private float satisfacton = 0;
    enum Recetas
    {
        Mona = 0,
        Sandwich  = 1,
        Rosquilletas = 2
    }
    private Recetas[] command;
    // Start is called before the first frame update
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
        Mask.localScale = new Vector3(satisfacton, 0.2f, 1);
    }
    public void PrintCommand()
    {
        txt.text = "";
        for(int i = 0; i < command.Length; i++)
        {
            txt.text += command[i].ToString();
            txt.text += "\n";
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        satisfacton += 0.001f;
        if(satisfacton >= 1) { satisfacton = 0; }
        Mask.localScale = new Vector3(satisfacton, 0.2f, 1);
        image.color = new Color(satisfacton, 1 - satisfacton, 0);
        Debug.Log(satisfacton);
    }
    public void OnMouseOver()
    {
        Talk(true);
    }
    public void OnMouseExit()
    {
        Talk(false);
    }
    public void Talk(bool appear)
    {
        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(appear);
        }
    }
}
