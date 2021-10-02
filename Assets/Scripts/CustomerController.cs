using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CustomerController : MonoBehaviour
{
    public TextMeshPro txt;
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
    void Update()
    {
        
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
