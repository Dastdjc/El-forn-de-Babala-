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
    private float walk = 27.5f;
    private GameObject gmo;
    private bool conversando;
    public bool tochingPlayer = false;
    public bool ImSpecial = false;
    public int SpriteID;
    //static private CustomerController[] Instance = new CustomerController[4];
    //static public int MaxIndexRecipe = 5;

    //State == 0 cuando entra a la panader?a
    //State == 1 cuando pide algo y empieza a cansarse
    //State == 2 cuando se le ha acabado la paciencia o le has dado lo que quer?a y se va

    public GameObject[] sprites;
    string[] nombreRecetas = { "Mona de Pascua", "Fartons", "Farinada", "Bunyols de calabaza", "Pilotes de frare", "Flaons", "Coca de llanda", "Pasteles de boniato", "Mocador?"};
    enum Recetas
    {
        Mona = 0,
        Fartons = 1,
        Farinada = 2,
        Bunyols = 3,
        Pilotes = 4,
        Flaons = 5,
        Coca = 6,
        Pasteles = 7,
        Mocadora = 8
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


        //A medida que avanze y se desbloqueen m?s recetas el juego ,
        // el segundo n?mero del Range tendr? que ir aumentando

        //command = (Recetas)Random.Range(0, GameManager.Instance.maxIndexRecipe);
        command = (Recetas)Random.Range(0, GameManager.Instance.maxIndexRecipe); //Random.Range(0, GameManager.Instance.maxIndexRecipe);
        //Inicializa sus graficos y los vuelve invisibles
        PrintCommand();
        Talk(false);
        Mask = gameObject.transform.GetChild(1).gameObject.GetComponent<RectTransform>();
        image = Mask.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        Mask.localScale = new Vector3(timer / TimeWaiting, 0.2f, 1);

    }
    private void Update()
    {
        if (state == 1)
        {
            if (tochingPlayer && !dmcm.inConversation)
                SetThickness(0.005f);
            if (!tochingPlayer)
                SetThickness(0f);
            if (tochingPlayer && Input.GetKeyDown(KeyCode.E))
            {
                SetThickness(0f);
                Pedir();
            }
        }
        else if (state == 2) 
        {
            if (tochingPlayer && !dmcm.inConversation)
                SetThickness(0.005f);
            if (!tochingPlayer)
                SetThickness(0f);
            if (tochingPlayer && Input.GetKeyDown(KeyCode.F) && !dmcm.inConversation)
            {
                SetThickness(0f);
                if (Inventory.Instance.GetRecipe().amount > 0)
                {
                    SetSatisfaction(Inventory.Instance.GetRecipe());
                    Inventory.Instance.SubstractRecipeItem(Inventory.Instance.GetRecipe(), 1);
                }
                if (Inventory.Instance.inventoryOpened)
                    Inventory.Instance.OpenCloseInventory();
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
                //Est? en el m?todo OnMouseDown//
                //Espera a que le des su comida
                //case 1
                case 2:
                    timer += Time.deltaTime * 0.03f;
                    
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
                    DialogueManagerCM.TalkingWith = SpriteID;
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
                            GameManager.Instance.SumarSatisfacci?n(0);
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
                            GameManager.Instance.SumarSatisfacci?n(-3);
                            break;
                        case 2:
                            //pedido medio
                            //el plato correcto pero no est? en el punto del horno
                            if (conversando)
                            {
                                dmcm.index = Random.Range(0, 5);
                                dmcm.NPC = transform;
                                dmcm.conversation = medio;
                                dmcm.inConversation = true;
                                conversando = false;
                            }
                            GameManager.Instance.SumarSatisfacci?n(2);
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
                            GameManager.Instance.SumarSatisfacci?n(5);
                            break;
                    }
                    parent.GetComponent<Animator>().SetBool("waitting", false);
                    parent.transform.localScale = new Vector3(-parent.transform.localScale.x, parent.transform.localScale.y, 1f);
                    walk = 18;
                    state++;
                    break;
                case 4:
                    if (walk > 0) { parent.position -= new Vector3(0.1f, 0, 0); walk -= 0.1f; }
                    else { Destroy(parent.gameObject); GameManager.Instance.SumarSatisfacci?n(0); }
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
        if (food.type != nombreRecetas[(int)command] || (food.Coock.Peek() == 3 || food.Coock.Peek() == 0)) { satisfaction = -3; }
        else if (food.type == nombreRecetas[(int)command] && food.Coock.Peek() == 1) { satisfaction = 2; }
        else if (food.type == nombreRecetas[(int)command] && food.Coock.Peek() == 2) { satisfaction = 5; }
        GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>().touchingCustomer = false;
        conversando = true;
        food.Coock.Dequeue();
        //Calcular satisfacci?n
        state = 3;
    }

    void Pedir()
    {
        DialogueManagerCM.TalkingWith = SpriteID;
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
        Talk(false);
        tochingPlayer = false;
        GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>().touchingCustomer = false;
    }
    void SetThickness(float thick)
    {
        Material material;
        foreach (GameObject sprite in sprites)
        {
            material = sprite.GetComponent<SpriteRenderer>().material;
            material.SetFloat("Thickness", thick);
        }
        return;
    }
    private void OnEnable()
    {
        if (state != 0)
        {
            parent.GetComponent<Animator>().SetBool("waitting", true);
        }
            gmo = GameObject.FindGameObjectWithTag("dialogo");
            if (gmo != null)
            {
                dmcm = gmo.GetComponent<DialogueManagerCM>();
            }


    }
}
