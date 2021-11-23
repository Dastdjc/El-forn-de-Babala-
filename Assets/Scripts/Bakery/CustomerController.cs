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
    public Conversation conversation;
    public Conversation enfadado;
    public DialogueManagerCM dmcm;
    private float satisfacton = 0;
    private float walk = 15;
    private int state;
    private GameObject gmo;
    private bool conversando;
    //State == 0 cuando entra a la panadería
    //State == 1 cuando pide algo y empieza a cansarse
    //State == 2 cuando se le ha acabado la paciencia o le has dado lo que quería y se va
    enum Recetas
    {
        Served = -1,
        Mona = 0,
        Flaons  = 1,
        Farinada = 2,
        Fartons = 3,
        Bunyols = 4
    }
    private Recetas[] command;

    private void Awake()
    {
        gmo = GameObject.FindGameObjectWithTag("dialogo");

        if (gmo != null)
        {
            dmcm = gmo.GetComponent<DialogueManagerCM>();
        }
    }
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
                    switch (command[0])
                    {
                        case Recetas.Mona:
                            dmcm.index = Random.Range(0,2);
                            dmcm.NPC = transform;
                            dmcm.conversation = conversation;
                            dmcm.inConversation = true;
                            break;
                        case Recetas.Flaons:
                            dmcm.index = Random.Range(12, 14);
                            dmcm.NPC = transform;
                            dmcm.conversation = conversation;
                            dmcm.inConversation = true;
                            break;
                        case Recetas.Farinada:
                            dmcm.index = Random.Range(2, 4);
                            dmcm.NPC = transform;
                            dmcm.conversation = conversation;
                            dmcm.inConversation = true;
                            break;
                        case Recetas.Fartons:
                            dmcm.index = Random.Range(8, 10);
                            dmcm.NPC = transform;
                            dmcm.conversation = conversation;
                            dmcm.inConversation = true;
                            break;
                        case Recetas.Bunyols:
                            dmcm.index = Random.Range(4, 6);
                            dmcm.NPC = transform;
                            dmcm.conversation = conversation;
                            dmcm.inConversation = true;
                            break;
                        default:
                            break;
                    }
                    state++;
                    break;
                //Espera a que le des su comida
                case 2:
                    satisfacton += 0.001f;
                    
                    Mask.localScale = new Vector3(satisfacton / TimeWaiting, 0.2f, 1);
                    image.color = new Color(satisfacton / TimeWaiting, 1 - satisfacton / TimeWaiting, 0);
                    if (satisfacton >= TimeWaiting)
                    {
                        state = 3;
                        Talk(false);
                        walk = 15;
                        conversando = true;
                    }
                    break;
                //Se va a su casa enfadado
                case 3:
                    if (conversando)
                    {
                        dmcm.index = Random.Range(0, 6);
                        dmcm.NPC = transform;
                        dmcm.conversation = enfadado;
                        dmcm.inConversation = true;
                        conversando = false;
                    }
                    this.transform.position = this.transform.position + new Vector3(-0.1f, 0, 0);
                    walk -= 0.1f;
                    if (walk <= 0)
                    {
                        SpawnCustomers.positions[(-(int)gameObject.transform.position.x - 12) / 2] = false;
                        Destroy(gameObject);
                    }
                    break;
                //pedido bueno
                case 4:

                    break;
                //pedido medio
                case 5:

                    break;
                //pedido malo
                case 6:

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
