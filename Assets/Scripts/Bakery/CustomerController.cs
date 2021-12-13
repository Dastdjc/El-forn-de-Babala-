using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CustomerController : MonoBehaviour
{
    public float TimeWaiting = 0.8f;
    public Transform parent;
    private RectTransform Mask;
    private SpriteRenderer image;
    public Conversation conversation;
    public Conversation enfadado;
    public Conversation bueno;
    public Conversation medio;
    public Conversation malo;
    public DialogueManagerCM dmcm;
    private float timer = 0;
    private int satisfaction = 0;
    private int state;
    private float walk = 15;
    private GameObject gmo;
    private bool conversando;
    public bool tochingPlayer = false;
    public bool ImSpecial = false;
    //static private CustomerController[] Instance = new CustomerController[4];
    static public int MaxIndexRecipe;

    //State == 0 cuando entra a la panadería
    //State == 1 cuando pide algo y empieza a cansarse
    //State == 2 cuando se le ha acabado la paciencia o le has dado lo que quería y se va

    public SpriteRenderer sr;
    private Material outline;
    enum Recetas
    {
        Mona = 0,
        Flaons  = 1,
        Farinada = 2,
        Fartons = 3,
        Bunyols = 4
    }
    private Recetas command;

    private void Awake()
    {
        /*int i = 0;
        int j = 0;
        while(Instance[i] != null)
        {
            i++;
            if (Instance[i] == this) j = i;
        }
        if (i < 4)
        {
            Instance[i] = this;
            while(parent != null) { }
            DontDestroyOnLoad(parent);
            
        else if (Instance[j] != this)
        {
            Destroy(gameObject);
        }*/
        gmo = GameObject.FindGameObjectWithTag("dialogo");
        if (gmo != null)
        {
            dmcm = gmo.GetComponent<DialogueManagerCM>();
        }
    }

    void Start()
    {
        //Se asigna la layer CustomerIn que no colisionan con CustomerIn
        gameObject.layer = 6;

        
        //A medida que avanze y se desbloqueen más recetas el juego ,
        // el segundo número del Range tendrá que ir aumentando
        command = (Recetas)Random.Range(0, 5);
        //Inicializa sus graficos y los vuelve invisibles
        PrintCommand();
        Talk(false);
        Mask = gameObject.transform.GetChild(1).gameObject.GetComponent<RectTransform>();
        image = Mask.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        Mask.localScale = new Vector3(timer / TimeWaiting, 0.2f, 1);

        parent = transform.parent;
        outline = sr.material;
    }
    private void Update()
    {
        if (state == 1) 
        {
            if (tochingPlayer && Input.GetKeyDown(KeyCode.E))
            {
                Pedir();
            }
        }
    }
    void FixedUpdate()
    {
        if(Time.timeScale == 1)
        {
            //if(Instance[0] == this)Debug.Log(state);
            switch (state)
            {
                //Anda a su sitio
                case 0:
                    if (walk > 0) { parent.position += new Vector3(0.1f, 0, 0); walk -= 0.1f; }
                    else { state++;parent.GetComponent<Animator>().SetBool("waitting",true); }
                    break;
                //Tiene que pedir y hablar
                //Está en el método OnMouseDown//
                //Espera a que le des su comida
                //case 1
                case 2:
                    timer += 0.001f;
                    
                    Mask.localScale = new Vector3(timer / TimeWaiting, 0.2f, 1);
                    image.color = new Color(timer / TimeWaiting, 1 - timer / TimeWaiting, 0);
                    if (timer >= TimeWaiting)
                    {
                        state = 3;
                        Talk(false);
                        conversando = true;
                    }
                    break;
                
                case 3:
                    switch (satisfaction)
                    {
                        case 0:
                            //Se va a su casa enfadado
                            //se va por tiempo
                            if (conversando)
                            {
                                dmcm.index = Random.Range(0, 6);
                                dmcm.NPC = transform;
                                dmcm.conversation = enfadado;
                                dmcm.inConversation = true;
                                conversando = false;
                            }
                            break;
                        case -3:
                            //pedido malo
                            //te has equivocado de ingredientes o es otro plato
                            if (conversando)
                            {
                                dmcm.index = Random.Range(0, 5);
                                dmcm.NPC = transform;
                                dmcm.conversation = malo;
                                dmcm.inConversation = true;
                                conversando = false;
                            }
                            break;
                        case 2:
                            //pedido medio
                            //el plato correcto pero no está en el punto del horno
                            if (conversando)
                            {
                                dmcm.index = Random.Range(0, 5);
                                dmcm.NPC = transform;
                                dmcm.conversation = medio;
                                dmcm.inConversation = true;
                                conversando = false;
                            }
                            break;
                        case 5:
                            //pedido bueno
                            //todo perfecto
                            if (conversando)
                            {
                                dmcm.index = Random.Range(0, 6);
                                dmcm.NPC = transform;
                                dmcm.conversation = bueno;
                                dmcm.inConversation = true;
                                conversando = false;
                            }
                            break;
                    }
                    walk = 18;
                    state++;
                    break;
                case 4:
                    if(walk > 0) { parent.position -= new Vector3(0.1f, 0, 0); walk -= 0.1f; }
                    else { Destroy(parent.gameObject); }
                    break;
            }
        }
    }
    public void PrintCommand()
    {
        gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>().text = command.ToString();
    }
    public void Talk(bool appear)
    {
        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(appear);
        }
    }
    public void SetSatisfaction(Recipe food)// De momento solo puede estar perfecto o mal
    {
        if(food.type != command.ToString() /*|| options[1] == 2*/) { satisfaction = -3; }
        //else if(options[1] == 0) { satisfaction = 2; }
        else { satisfaction = 5; }
        Debug.Log(satisfaction);
        GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>().touchingCustomer = false;
        conversando = true;
        //Calcular satisfacción
        state = 3;
    }

    void Pedir() {
        
            switch (command)
            {
                case Recetas.Mona:
                    dmcm.index = Random.Range(0, 2);
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
        state = 2;
        OnTriggerEnter2D(GameObject.Find("Dore_player").GetComponent<Collider2D>());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        outline.SetFloat("Thickness",0.03f);
        /*if (state == 1)
        {
            switch (command)
            {
                case Recetas.Mona:
                    dmcm.index = Random.Range(0, 2);
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
            state = 2;
        }*/
        if (state == 2)
        {
            Talk(true);
            tochingPlayer = true;
            GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>().touchingCustomer = true;
        }
        tochingPlayer = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        outline.SetFloat("Thickness", 0);
        Talk(false);
        tochingPlayer = false;
        GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>().touchingCustomer = false;
    }
}
