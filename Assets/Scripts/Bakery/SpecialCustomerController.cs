using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpecialCustomerController : MonoBehaviour
{
    public float TimeWaiting = 0.8f;
    public Transform parent;
    public Animator animator;
    private RectTransform Mask;
    private SpriteRenderer image;
    public DialogueManager dm;
    private float timer = 0;
    private int satisfaction = 0;
    private int state;
    private float walk = 27.5f;
    private GameObject gmo;
    private bool leaving = false;
    public bool tochingPlayer = false;
    public bool ImSpecial = false;
    public CharacterDialogueManager cdm;

    public DialogueAudio characterAudio;
    private SpawnCustomers spawner;

    public GameObject[] sprites;
    //static private CustomerController[] Instance = new CustomerController[4];
    //static public int MaxIndexRecipe = 5;

    //State == 0 cuando entra a la panadería
    //State == 1 cuando pide algo y empieza a cansarse
    //State == 2 cuando se le ha acabado la paciencia o le has dado lo que quería y se va
    string[] nombreRecetas = { "Mona de Pascua", "Fartons", "Farinada", "Bunyols de calabaza", "Pilotes de frare", "Flaons", "Coca", "Pasteles de boniato", "Mocadorà" };
    enum Recetas
    {
        Mona,
        Fartons,
        Farinada,
        Bunyols,
        Pilotes,
        Flaons,
        Coca,
        Pasteles,
        Mocadora
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
            dm = gmo.GetComponent<DialogueManager>();
        }
        characterAudio.animatedText = GameObject.Find("TMP_Animated (1)").GetComponent<TMP_Animated>();
    }

    void Start()
    {
        //Se asigna la layer CustomerIn que no colisionan con CustomerIn
        gameObject.layer = 6;


        //A medida que avanze y se desbloqueen más recetas el juego ,
        // el segundo número del Range tendrá que ir aumentando
        cdm = GetComponent<CharacterDialogueManager>();
        command = (Recetas)Random.Range(0, GameManager.Instance.maxIndexRecipe);//(Recetas)cdm.recipeNumber;//Random.Range(0, GameManager.Instance.maxIndexRecipe); (Recetas)1
        //Inicializa sus graficos y los vuelve invisibles
        PrintCommand();
        Talk(false);
        Mask = gameObject.transform.GetChild(1).gameObject.GetComponent<RectTransform>();
        image = Mask.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        Mask.localScale = new Vector3(timer / TimeWaiting, 0.2f, 1);


        animator.SetBool("Moving", true);

        spawner = GameObject.Find("Spawner").GetComponent<SpawnCustomers>();
    }
    private void Update()
    {
        if (state == 1)
        {
            if (tochingPlayer && !dm.inConversation)
                SetThickness(0.005f);
            else if (!tochingPlayer)
                SetThickness(0f);
            if (tochingPlayer && Input.GetKeyDown(KeyCode.E))
            {
                SetThickness(0f);
                Pedir();
            }
        }
        else if (state == 2)
        {
            if (tochingPlayer && !dm.inConversation)
            {
                SetThickness(0.005f);
                Talk(true);
            }
            
            if (!tochingPlayer)
                SetThickness(0f);
            if (tochingPlayer && Input.GetKeyDown(KeyCode.F) && !dm.inConversation)
            {
                Talk(false);
                SetThickness(0f);
                SetSatisfaction(Inventory.Instance.GetRecipe());
                if (Inventory.Instance.inventoryOpened)
                    Inventory.Instance.OpenCloseInventory();
            }
        }
        else if (state == 4) 
        {
            if (tochingPlayer && !dm.inConversation)
                SetThickness(0.005f);
            if (!tochingPlayer)
                SetThickness(0f);
            if (tochingPlayer && Input.GetKeyDown(KeyCode.E))
            {
                SetThickness(0f);
                dm.conversation = cdm.final;
                dm.NPC = transform;
                dm.inConversation = true;
                state++;
                
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
                    if (walk > 0) { parent.transform.position += new Vector3(0.1f, 0, 0); walk -= 0.1f; }
                    else { state++; animator.SetBool("Moving", false);
                        //parent.GetComponent<Animator>().SetBool("waitting",true); 
                    }
                    break;
                //Tiene que pedir y hablar
                //Está en el método OnMouseDown//
                //Espera a que le des su comida
                //case 1
                case 2:
                    if (!dm.inConversation)
                    {
                        timer += Time.deltaTime * 0.03f;

                        Mask.localScale = new Vector3(timer / TimeWaiting, 0.2f, 1);
                        image.color = new Color(timer / TimeWaiting, 1 - timer / TimeWaiting, 0);
                        if (timer >= TimeWaiting)
                        {
                            state = 3;
                            Talk(false);
                        }
                    }
                    break;
                
                case 3:
                    Conversation conversationInstace = ScriptableObject.CreateInstance("Conversation") as Conversation;
                    switch (satisfaction) 
                    {
                        case 0:
                            conversationInstace.lines.Add(cdm.reacciones.lines[0]);
                            break;
                        case -3:
                            conversationInstace.lines.Add(cdm.reacciones.lines[1]);
                            break;
                        case 2:
                            conversationInstace.lines.Add(cdm.reacciones.lines[2]);
                            break;
                        case 5:
                            conversationInstace.lines.Add(cdm.reacciones.lines[3]);
                            break;
                    }
                    
                    dm.conversation = conversationInstace;
                    dm.NPC = transform;
                    dm.inConversation = true;
                    walk = 18;
                    state++;
                    break;
                case 4:
                    if (satisfaction == 0 || satisfaction == -3) 
                        // Se va triste
                    {
                        if (!leaving)
                        {
                            Talk(false);
                            leaving = true;
                            animator.SetBool("Moving", true);
                            parent.transform.localScale = new Vector3(-parent.transform.localScale.x, parent.transform.localScale.y, 1f);
                        }
                        if (walk > 0) { parent.transform.position -= new Vector3(0.1f, 0, 0); walk -= 0.1f; animator.SetBool("Moving", true);}
                        else { spawner.SpawnigSpecial = false; 
                            GameManager.Instance.SumarSatisfacción(0); Destroy(parent.transform.gameObject); }
                    }
                    break;
                case 5:
                    if (!dm.inConversation)
                    {
                        // Se va con satisfacción
                        if (!leaving)
                        {
                            leaving = true;
                            animator.SetBool("Moving", true);
                            parent.transform.localScale = new Vector3(-parent.transform.localScale.x, parent.transform.localScale.y, 1f);
                        }
                        if (walk > 0) { parent.transform.position -= new Vector3(0.1f, 0, 0); walk -= 0.1f; }
                        else { 
                            GameManager.Instance.SumarSatisfacción(0);
                            if (satisfaction > 0)
                            {
                                spawner.CustomersNumber = 0;
                                GameManager.Instance.specialCharacterIndex++;
                            }
                            
                            Destroy(parent.transform.gameObject);
                        } 
                        GameManager.Instance.dia = false;
                    }
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
        Debug.Log(satisfaction);
        GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>().touchingCustomer = false;
        //Calcular satisfacción
        state = 3;
    }

    void Pedir() {
        //Conversation conversationInstace = ScriptableObject.CreateInstance("Conversation") as Conversation;
        //conversationInstace.lines.Add(cdm.reacciones.lines[satisfaction]);
        dm.conversation = cdm.CrearConversacion((int)command);
        dm.NPC = transform;
        dm.inConversation = true;
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
        if (state == 2 && !dm.inConversation)
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
}
