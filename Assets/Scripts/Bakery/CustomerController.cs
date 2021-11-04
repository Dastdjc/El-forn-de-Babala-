using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CustomerController : MonoBehaviour
{
    public int TimeWaiting = 2;
    public GameObject TargetBar;
    private RectTransform Mask;
    private SpriteRenderer image;
    private float satisfacton = 0;
    private float walk = 15;
    private int state;
    //State == 0 cuando entra a la panadería
    //State == 1 cuando pide algo y empieza a cansarse
    //State == 2 cuando se le ha acabado la paciencia o le has dado lo que quería y se va
    enum Recetas
    {
        Served = -1,
        Mona = 0,
        Sandwich  = 1,
        Rosquilletas = 2,
        Fartons = 3,
        Bunyols = 4
    }
    private Recetas[] command;

    void Start()
    {
        //Se asigna la layer CustomerIn que no colisionan con CustomerIn
        gameObject.layer = 3;

        //A medida que avanze y se desbloqueen más recetas el juego ,
        // el segundo número del Range tendrá que ir aumentando
        command = new Recetas[Random.Range(1, 4)];
        for(int i = 0; i < command.Length; i++)
        {
            command[i] = new Recetas();
            command[i] = (Recetas)Random.Range(0, 5);
        }
        //Inicializa sus graficos y los vuelve invisibles
        PrintCommand();
        Talk(false);
        Mask = gameObject.transform.GetChild(1).gameObject.GetComponent<RectTransform>();
        image = Mask.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        Mask.localScale = new Vector3(satisfacton / TimeWaiting, 0.2f, 1);
    }
    void FixedUpdate()
    {
        if(Time.timeScale == 1)
        {
            switch (state)
            {
                //Anda a su sitio
                case 0:
                    this.transform.position = this.transform.position + new Vector3(0.1f, 0, 0);
                    walk -= 0.1f;
                    if (walk <= 0) { state++; }
                    break;
                //Tiene que pedir y hablar
                case 1:
                    //Aquí van los diálogos del cliente
                    state++;
                    break;
                //Espera a que le des su comida
                case 2:
                    satisfacton += 0.001f;
                    Mask.localScale = new Vector3(satisfacton / TimeWaiting, 0.2f, 1);
                    image.color = new Color(satisfacton / TimeWaiting, 1 - satisfacton / TimeWaiting, 0);
                    if (satisfacton >= TimeWaiting)
                    {
                        state++;
                        Talk(false);
                        walk = 15;
                    }
                    break;
                //Se va a su casa
                case 3:
                    this.transform.position = this.transform.position + new Vector3(-0.1f, 0, 0);
                    walk -= 0.1f;
                    if (walk <= 0)
                    {
                        SpawnCustomers.positions[(-(int)gameObject.transform.position.x - 12) / 2] = false;
                        Destroy(gameObject);
                    }
                    break;
            }
        }
    }
    public void OnMouseOver()
    {
        if(state == 2 && Time.timeScale ==1)
            Talk(true);
    }
    public void OnMouseExit()
    {
        if (state == 2 && Time.timeScale == 1)
            Talk(false);
    }
    private void OnMouseDown()
    {
        if (state == 2 && Time.timeScale == 1)
        {
            for (int i = 0; i < command.Length; i++)
            {
                TargetBar.transform.GetComponent<FoodBar>().WriteCommand((int)command[i]);
            }
        }
    }
    public void PrintCommand()
    {
        TextMeshPro txt = gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>();
        txt.text = "";
        int j = 0;
        for (int i = 0; i < command.Length; i++)
        {
            if(command[i] != Recetas.Served)
            {
                txt.text += command[i].ToString();
                txt.text += "\n";
            }
            else { j++; }
        }
        if( j == command.Length) { satisfacton = TimeWaiting; }
    }
    public void Talk(bool appear)
    {
        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(appear);
        }
    }
    public bool DeleteOnCommand(string foodName)
    {
        int i = 0;
        while(i < command.Length && command[i].ToString() != foodName) { i++; }
        if(i < command.Length) { command[i] = Recetas.Served; }
        else { return false; }
        PrintCommand();
        return true;
    }
}
