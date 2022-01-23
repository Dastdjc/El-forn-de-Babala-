using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{
    //dontdestroy
    public static Inventory Instance;

    //variables tocando mesa y tocando cliente
    public bool touchingTable;
    public bool touchingCustomer;

    //para poder quitar objetos del inventario
    //Usa la variable estatica TableController.isOnColision

    //para poder abrir y cerrar el inventario
    public GameObject inventory;
    public bool inventoryOpened;

    //0 para el de ingredientes y 1 para el de recetas
    private int inventoryType;

    //objeto que tomamos del inventario para usarlo en la cocina
    private Items givenItem;
    //comida que tomamos del comidentario para darselo a los mundanos
    private Recipe givenRecipe;

    //selector de slot
    public GameObject selector;
    public GameObject selector2;

    //variable usada para saber cuántos slots ocupados hay, slots != null
    private int c = 0;

    //ID se usará para saber en que slot del inventario estamos y posicionar el selector encima de éste
    private int ingrID;
    private int recetID;
    private int comiID;

    //esta lista, dada en el Unity, solo se usa para "robar" la imagen del objeto correcto
    public List<Items> ingrImagesList;

    //esta lista, dada en el Unity, solo se usa para "robar" la imagen de la receta correcta
    public List<Recipe> recipeImagesList;

    //esta se usa para determinar si ya poseemos un item del tipo que vamos a añadir
    public List<Items> ingrList = new List<Items>();

    //ésta para que se sepa que tipo de item está en cada slot
    private List<Items> itemBySlotList = new List<Items>();

    //ésta para que se sepa que tipo de recta está en cada slot
    private List<Recipe> recipeBySlotList = new List<Recipe>();

    //esta para saber en el inventario de recetas, que receta ocupa que sitio
    private List<Recipe> recipeItemBySlotList = new List<Recipe>();

    //esta se usa para determinar si ya poseemos una receta del tipo que vamos a añadir y cuántos platos de esa receta poseemos
    public List<Recipe> recipeList = new List<Recipe>();

    
    private Items harina;

    private Recipe fartons;
    private Recipe basura;
    private Recipe bunyols;
    private Recipe coca;
    private Recipe farinada;
    private Recipe flaons;
    private Recipe mocadora;
    private Recipe mona;
    private Recipe pasteles;
    private Recipe pilotes;

    // Variables para control de poder abrir el inventario
    private GameObject player;
    private DialogueManager dm;
    public bool inMinigame;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        for (int i = 0; i < 21; i++)
        {
            //establecemos a null toda la lista
            itemBySlotList.Add(null);
        }

        for (int i = 0; i < 10; i++)
        {
            //establecemos a null toda la lista
            recipeBySlotList.Add(null);
        }

        for (int i = 0; i < 10; i++)
        {
            //establecemos a null toda la lista
            recipeItemBySlotList.Add(null);
        }
    }
   
    

    // Start is called before the first frame update
    void Start()
    {

        touchingTable = false;
        inventoryOpened = false;
        inventoryType = 0;
        ingrID = 0;
        recetID = 0;

        harina = ScriptableObject.CreateInstance<Items>();
        harina.amount = 5;
        harina.type = "Harina";
        AddIngrItem(harina);

       
        fartons = ScriptableObject.CreateInstance<Recipe>();
        fartons.amount = 0;
        fartons.type = "Fartons";
        fartons.Coock = new Queue<int>();
        for (int i = 0; i < 50; i++)
        {
            fartons.Coock.Enqueue(2);
        }
        AddRecipe(fartons);

        basura = ScriptableObject.CreateInstance<Recipe>();
        basura.amount = 0;
        basura.type = "Basura";
        basura.Coock = new Queue<int>();
        basura.Coock.Enqueue(2);
        AddRecipe(basura);

        bunyols = ScriptableObject.CreateInstance<Recipe>();
        bunyols.amount = 0;
        bunyols.type = "Bunyols de calabaza";
        bunyols.Coock = new Queue<int>();
        bunyols.Coock.Enqueue(2);
        AddRecipe(bunyols);

        coca = ScriptableObject.CreateInstance<Recipe>();
        coca.amount = 0;
        coca.type = "Coca de llanda";
        coca.Coock = new Queue<int>();
        coca.Coock.Enqueue(2);
        AddRecipe(coca);

        farinada = ScriptableObject.CreateInstance<Recipe>();
        farinada.amount = 0;
        farinada.type = "Farinada";
        farinada.Coock = new Queue<int>();
        farinada.Coock.Enqueue(2);
        AddRecipe(farinada);

        flaons = ScriptableObject.CreateInstance<Recipe>();
        flaons.amount = 0;
        flaons.type = "Flaons";
        flaons.Coock = new Queue<int>();
        flaons.Coock.Enqueue(2);
        AddRecipe(flaons);

        mocadora = ScriptableObject.CreateInstance<Recipe>();
        mocadora.amount = 0;
        mocadora.type = "Mocadorà";
        mocadora.Coock = new Queue<int>();
        mocadora.Coock.Enqueue(2);
        AddRecipe(mocadora);

        /*mona = ScriptableObject.CreateInstance<Recipe>();
        mona.amount = 0;
        mona.type = "Mona de Pascua";
        mona.Coock = new Queue<int>();
        mona.Coock.Enqueue(2);
        AddRecipe(mona);*/

        mona = ScriptableObject.CreateInstance<Recipe>();
        mona.amount = 3;
        mona.type = "Mona de Pascua";
        mona.Coock = new Queue<int>();
        mona.Coock.Enqueue(2);
        AddRecipe(mona);

        pasteles = ScriptableObject.CreateInstance<Recipe>();
        pasteles.amount = 0;
        pasteles.type = "Pasteles de boniato";
        pasteles.Coock = new Queue<int>();
        pasteles.Coock.Enqueue(2);
        AddRecipe(pasteles);

        pilotes = ScriptableObject.CreateInstance<Recipe>();
        pilotes.amount = 0;
        pilotes.type = "Pilotes de frare";
        pilotes.Coock = new Queue<int>();
        pilotes.Coock.Enqueue(2);
        AddRecipe(pilotes);

        player = GameObject.Find("Dore_player");
        dm = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (dm == null) // Comprobar si se ha destruido el objeto para volver a asignarlo
                dm = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
            if (!dm.inConversation && !inMinigame)
            {
                //sonido abrir inventario
                if (!inventory.GetComponent<AudioSource>().enabled)
                {
                    inventory.GetComponent<AudioSource>().enabled = true;
                }
                inventory.GetComponent<AudioSource>().Play();

                OpenCloseInventory();
            }
        }

        //si el inventario está abierto y hay mínimo un item
        if (inventoryOpened)
        {
            //si estamos viendo el ingredentario
            if (inventoryType == 0 && ingrList.Count > 0)
            {
                MoveInIngredentario();

                //también puedes seleccionar un objeto del inventario para restarle la cantidad necesaria a un ingrediente
                if (Input.GetKeyDown(KeyCode.F) && touchingTable)
                {
                    givenItem = TakeItemBySelector();
                    //GameObject.FindGameObjectWithTag("Bol").GetComponent<BowlController>().PutIngredient(givenItem);
                    SubstractIngrItem(givenItem, 1);
                }
            }
            //si estamos viendo el recetario
            else if (inventoryType == 1 && recipeList.Count > 0)
            {
                MoveInRecetario();

                //también puedes seleccionar un objeto del inventario para restarle la cantidad necesaria a un ingrediente
                /*if (Input.GetKeyDown(KeyCode.F) && touchingCustomer && SpawnCustomers.WhichToching() < 4)
                {
                    Debug.Log("Intentando dar receta a cliente");
                    //quitaremos uno a la cantidad de platos que tengamos de una receta, siempre que tengamos mínimo uno
                    if (!SpawnCustomers.specialSpawned)
                        SpawnCustomers.Customers[SpawnCustomers.WhichToching()].transform.GetChild(SpawnCustomers.Customers[SpawnCustomers.WhichToching()].transform.childCount - 1).GetComponent<CustomerController>().SetSatisfaction(recipeBySlotList[recetID]);
                    else
                        SpawnCustomers.Customers[SpawnCustomers.WhichToching()].transform.GetChild(SpawnCustomers.Customers[SpawnCustomers.WhichToching()].transform.childCount - 1).GetComponent<SpecialCustomerController>().SetSatisfaction(recipeBySlotList[recetID]);
                    recipeBySlotList[recetID].amount--;
                    OpenCloseInventory();
                }*/
               // OpenCloseInventory();
            }
            //si estamos viendo el recetory
            else if (inventoryType == 2 && recipeList.Count > 0)
            {
                MoveInRecetory();

                if (Input.GetKeyDown(KeyCode.F) && touchingCustomer && recipeItemBySlotList[comiID].amount > 0)
                {
                    givenRecipe = TakeRecipeItemBySelector();
                    SubstractRecipeItem(givenRecipe, 1);
                }
            }

            //si el inventario está abierto puedes cambiar entre pestañas
            ChangeInventory();
        }
    }

    private void ChangeInventory()
    {
        if (inventoryType == 0 && Input.GetKeyDown(KeyCode.E))
        {
            inventoryType = 1;
            inventory.transform.GetChild(2).GetComponent<AudioSource>().enabled = false;
            inventory.transform.GetChild(1).GetComponent<AudioSource>().enabled = false;
            inventory.transform.GetChild(1).gameObject.SetActive(false);
            inventory.transform.GetChild(8).gameObject.SetActive(false);
            inventory.transform.GetChild(0).gameObject.SetActive(true);
            inventory.transform.GetChild(3).transform.GetChild(0).GetComponent<TMP_Text>().text = "RECETARIO";
            inventory.transform.GetChild(5).gameObject.SetActive(false);
            inventory.transform.GetChild(6).gameObject.SetActive(true);
            inventory.transform.GetChild(4).transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);

            if (recipeList.Count == 0)
            {
                //también habría que poner el selector en el primer slot del recetario
                inventory.transform.GetChild(2).gameObject.SetActive(false);
                inventory.transform.GetChild(4).gameObject.SetActive(false);
            }
            else
            {
                //activamos el selector, el nombre de la receta y los ingredientes por receta si conocemos alguna receta
                selector.transform.position = inventory.transform.GetChild(0).transform.GetChild(0).transform.GetChild(recetID).transform.position;
                inventory.transform.GetChild(2).gameObject.SetActive(true);
                inventory.transform.GetChild(4).gameObject.SetActive(true);
                inventory.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
            }

            //sonido del cambio de página
            if (!inventory.transform.GetChild(3).GetComponent<AudioSource>().enabled)
            {
                inventory.transform.GetChild(3).GetComponent<AudioSource>().enabled = true;
            }

            inventory.transform.GetChild(3).GetComponent<AudioSource>().Play();
        }
        else if (inventoryType == 1 && Input.GetKeyDown(KeyCode.Q))
        {
            inventoryType = 0;
            inventory.transform.GetChild(1).gameObject.SetActive(true);
            inventory.transform.GetChild(0).gameObject.SetActive(false);
            inventory.transform.GetChild(3).transform.GetChild(0).GetComponent<TMP_Text>().text = "INGREDENTARIO";
            inventory.transform.GetChild(6).gameObject.SetActive(false);
            inventory.transform.GetChild(5).gameObject.SetActive(true);
            inventory.transform.GetChild(8).gameObject.SetActive(true);

            if (ingrList.Count == 0)
            {
                inventory.transform.GetChild(2).gameObject.SetActive(false);
                inventory.transform.GetChild(4).gameObject.SetActive(false);
            }
            else
            {
                inventory.transform.GetChild(2).gameObject.SetActive(true);
                inventory.transform.GetChild(4).gameObject.SetActive(true);
            }

            //sonido cambio página
            if (!inventory.transform.GetChild(3).GetComponent<AudioSource>().enabled)
            {
                inventory.transform.GetChild(3).GetComponent<AudioSource>().enabled = true;
            }

            inventory.transform.GetChild(3).GetComponent<AudioSource>().Play();
        }
        else if (inventoryType == 0 && Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryType = 2;
            inventory.transform.GetChild(7).gameObject.SetActive(true);
            inventory.transform.GetChild(1).gameObject.SetActive(false);
            inventory.transform.GetChild(3).gameObject.SetActive(false);
            inventory.transform.GetChild(9).gameObject.SetActive(true);
            inventory.transform.GetChild(5).gameObject.SetActive(false);
            inventory.transform.GetChild(8).gameObject.SetActive(false);
            inventory.transform.GetChild(10).gameObject.SetActive(true);
            inventory.transform.GetChild(2).gameObject.SetActive(false);

            if (recipeList.Count > 0)
            {
                inventory.transform.GetChild(12).gameObject.SetActive(true);
                inventory.transform.GetChild(11).gameObject.SetActive(true);
            }
            else
            {
                inventory.transform.GetChild(12).gameObject.SetActive(false);
                inventory.transform.GetChild(11).gameObject.SetActive(false);
            }

            //sonido cambio página
            if (!inventory.transform.GetChild(9).GetComponent<AudioSource>().enabled)
            {
                inventory.transform.GetChild(9).GetComponent<AudioSource>().enabled = true;
            }

            inventory.transform.GetChild(9).GetComponent<AudioSource>().Play();
        }
        else if (inventoryType == 2 && Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryType = 0;
            inventory.transform.GetChild(1).gameObject.SetActive(true);
            inventory.transform.GetChild(7).gameObject.SetActive(false);
            inventory.transform.GetChild(3).gameObject.SetActive(true);
            inventory.transform.GetChild(8).gameObject.SetActive(true);
            inventory.transform.GetChild(5).gameObject.SetActive(true);
            inventory.transform.GetChild(9).GetComponent<AudioSource>().enabled = false;
            inventory.transform.GetChild(12).GetComponent<AudioSource>().enabled = false;
            inventory.transform.GetChild(9).gameObject.SetActive(false);
            inventory.transform.GetChild(11).gameObject.SetActive(false);
            inventory.transform.GetChild(10).gameObject.SetActive(false);
            inventory.transform.GetChild(12).gameObject.SetActive(false);

            if (ingrList.Count == 0)
            {
                inventory.transform.GetChild(2).gameObject.SetActive(false);
                inventory.transform.GetChild(4).gameObject.SetActive(false);
            }
            else
            {
                inventory.transform.GetChild(2).gameObject.SetActive(true);
                inventory.transform.GetChild(4).gameObject.SetActive(true);
            }


            //sonido cambio página
            if (!inventory.transform.GetChild(3).GetComponent<AudioSource>().enabled)
            {
                inventory.transform.GetChild(3).GetComponent<AudioSource>().enabled = true;
            }

            inventory.transform.GetChild(3).GetComponent<AudioSource>().Play();
        }
    }

    //abrir y cerrar inventario
    public void OpenCloseInventory()
    {
        if (!inventoryOpened)
        {
            inventoryOpened = true;

            //último inventario abierto el de ingredientes
            if (inventoryType == 0)
            {
                //activamos los slots ocupados, el selector, el texto Inventario y el nombre del objeto seleccionado
                inventory.transform.GetChild(1).gameObject.SetActive(true);
                inventory.transform.GetChild(3).gameObject.SetActive(true);
                inventory.transform.GetChild(5).gameObject.SetActive(true);
                inventory.transform.GetChild(8).gameObject.SetActive(true);

                if (ingrList.Count > 0)
                {
                    inventory.transform.GetChild(2).gameObject.SetActive(true);
                    inventory.transform.GetChild(4).gameObject.SetActive(true);
                }

            //último inventario abierto el de recetas
            }
            else if (inventoryType == 1)
            {
                inventory.transform.GetChild(0).gameObject.SetActive(true);
                inventory.transform.GetChild(3).gameObject.SetActive(true);
                inventory.transform.GetChild(6).gameObject.SetActive(true);

                if (recipeList.Count > 0)
                {
                    inventory.transform.GetChild(2).gameObject.SetActive(true);
                    inventory.transform.GetChild(4).gameObject.SetActive(true);
                    inventory.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
                }
            }
            //último inventario abierto el de comida
            else
            {
                inventory.transform.GetChild(7).gameObject.SetActive(true);
                inventory.transform.GetChild(9).gameObject.SetActive(true);
                inventory.transform.GetChild(10).gameObject.SetActive(true);

                if (recipeList.Count > 0)
                {
                    inventory.transform.GetChild(11).gameObject.SetActive(true);
                    inventory.transform.GetChild(12).gameObject.SetActive(true);
                }
            }
        }
        else
        {
            inventoryOpened = false;
            inventory.transform.GetChild(3).GetComponent<AudioSource>().enabled = false;
            inventory.transform.GetChild(2).GetComponent<AudioSource>().enabled = false;
            inventory.transform.GetChild(9).GetComponent<AudioSource>().enabled = false;
            inventory.transform.GetChild(1).GetComponent<AudioSource>().enabled = false;
            inventory.transform.GetChild(12).GetComponent<AudioSource>().enabled = false;
            inventory.transform.GetChild(3).gameObject.SetActive(false);
            inventory.transform.GetChild(9).gameObject.SetActive(false);
            inventory.transform.GetChild(10).gameObject.SetActive(false);

            if (ingrList.Count > 0 || recipeList.Count > 0)
            {
                inventory.transform.GetChild(2).gameObject.SetActive(false);
                inventory.transform.GetChild(4).gameObject.SetActive(false);
                inventory.transform.GetChild(11).gameObject.SetActive(false);
                inventory.transform.GetChild(12).gameObject.SetActive(false);
            }

            if (inventoryType == 0)
            {
                inventory.transform.GetChild(1).gameObject.SetActive(false);
                inventory.transform.GetChild(5).gameObject.SetActive(false);
                inventory.transform.GetChild(8).gameObject.SetActive(false);
            }
            else if (inventoryType == 1)
            {
                inventory.transform.GetChild(0).gameObject.SetActive(false);
                inventory.transform.GetChild(6).gameObject.SetActive(false);
            }
            else
            {
                inventory.transform.GetChild(7).gameObject.SetActive(false);
                inventory.transform.GetChild(5).gameObject.SetActive(false);
            }
        }
        // Activar/Desactivar jugador
        if (player == null)
            player = GameObject.Find("Dore_player");
        player.SetActive(!inventoryOpened);
    }

    //mover por el recetory (comidentario)
    private void MoveInRecetory()
    {
        if (Input.GetKeyDown(KeyCode.D) && comiID < recipeList.Count - 1)
        {
            comiID++;

            if (!inventory.transform.GetChild(12).GetComponent<AudioSource>().enabled)
            {
                inventory.transform.GetChild(12).GetComponent<AudioSource>().enabled = true;
            }

            inventory.transform.GetChild(12).GetComponent<AudioSource>().Play();
        }
        else if (Input.GetKeyDown(KeyCode.A) && comiID > 0)
        {
            comiID--;

            if (!inventory.transform.GetChild(12).GetComponent<AudioSource>().enabled)
            {
                inventory.transform.GetChild(12).GetComponent<AudioSource>().enabled = true;
            }

            inventory.transform.GetChild(12).GetComponent<AudioSource>().Play();
        }
        else if (Input.GetKeyDown(KeyCode.W) && comiID > 4)
        {
            comiID -= 5;

            if (!inventory.transform.GetChild(12).GetComponent<AudioSource>().enabled)
            {
                inventory.transform.GetChild(12).GetComponent<AudioSource>().enabled = true;
            }

            inventory.transform.GetChild(12).GetComponent<AudioSource>().Play();
        }
        else if (Input.GetKeyDown(KeyCode.S) && recipeList.Count >= (comiID + 5) && (comiID + 5) != recipeList.Count)
        {
            comiID += 5;

            if (!inventory.transform.GetChild(12).GetComponent<AudioSource>().enabled)
            {
                inventory.transform.GetChild(12).GetComponent<AudioSource>().enabled = true;
            }

            inventory.transform.GetChild(12).GetComponent<AudioSource>().Play();
        }

        //mostramos el nombre del objeto en el que está el selector
        inventory.transform.GetChild(11).transform.GetChild(0).GetComponent<TMP_Text>().text = recipeItemBySlotList[comiID].type;

        //colocamos el selector en la casilla correcta
        selector2.transform.position = inventory.transform.GetChild(7).transform.GetChild(comiID).transform.position;
    }

    //mover selector por el inventario
    private void MoveInIngredentario()
    {
        if (Input.GetKeyDown(KeyCode.D) && ingrID < ingrList.Count - 1)
        {
            ingrID++;

            if (!inventory.transform.GetChild(2).GetComponent<AudioSource>().enabled)
            {
                inventory.transform.GetChild(2).GetComponent<AudioSource>().enabled = true;
            }

            inventory.transform.GetChild(2).GetComponent<AudioSource>().Play();
        }
        else if (Input.GetKeyDown(KeyCode.A) && ingrID > 0)
        {
            ingrID--;

            if (!inventory.transform.GetChild(2).GetComponent<AudioSource>().enabled)
            {
                inventory.transform.GetChild(2).GetComponent<AudioSource>().enabled = true;
            }

            inventory.transform.GetChild(2).GetComponent<AudioSource>().Play();
        }
        else if (Input.GetKeyDown(KeyCode.W) && ingrID > 6)
        {
            ingrID -= 7;

            if (!inventory.transform.GetChild(2).GetComponent<AudioSource>().enabled)
            {
                inventory.transform.GetChild(2).GetComponent<AudioSource>().enabled = true;
            }

            inventory.transform.GetChild(2).GetComponent<AudioSource>().Play();
        }
        else if (Input.GetKeyDown(KeyCode.S) && ingrList.Count >= (ingrID + 7) && (ingrID+7) != ingrList.Count)
        {
            ingrID += 7;

            if (!inventory.transform.GetChild(2).GetComponent<AudioSource>().enabled)
            {
                inventory.transform.GetChild(2).GetComponent<AudioSource>().enabled = true;
            }

            inventory.transform.GetChild(2).GetComponent<AudioSource>().Play();
        }

        //mostramos el nombre del objeto en el que está el selector
        inventory.transform.GetChild(4).transform.GetChild(0).GetComponent<TMP_Text>().text = itemBySlotList[ingrID].type;

        //colocamos el selector en la casilla correcta
        selector.transform.position = inventory.transform.GetChild(1).transform.GetChild(ingrID).transform.position;

        /*
        //mostramos las estrellas al lado del nombre si el ingrediente es especial
        for (int i = 0; i < specialIngredientsList.Count; i++)
        {
            if (itemBySlotList[ingrID].type == specialIngredientsList[i])
            {
                inventory.transform.GetChild(4).transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
                break;
            }
            else
            {
                inventory.transform.GetChild(4).transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        */
    }

    //mover selector por el recetario
    private void MoveInRecetario()
    {
        //Debug.Log(recipeList.Count);

        if (Input.GetKeyDown(KeyCode.D) && recetID+3 < recipeList.Count)
        {
            recetID += 3;

            if (!inventory.transform.GetChild(2).GetComponent<AudioSource>().enabled)
            {
                inventory.transform.GetChild(2).GetComponent<AudioSource>().enabled = true;
            }

            inventory.transform.GetChild(2).GetComponent<AudioSource>().Play();
        }
        else if (Input.GetKeyDown(KeyCode.A) && recetID-3 >= 0)
        {
            recetID -= 3;

            if (!inventory.transform.GetChild(2).GetComponent<AudioSource>().enabled)
            {
                inventory.transform.GetChild(2).GetComponent<AudioSource>().enabled = true;
            }

            inventory.transform.GetChild(2).GetComponent<AudioSource>().Play();
        }
        else if (Input.GetKeyDown(KeyCode.W) && recetID > 0)
        {
            recetID--;

            if (!inventory.transform.GetChild(2).GetComponent<AudioSource>().enabled)
            {
                inventory.transform.GetChild(2).GetComponent<AudioSource>().enabled = true;
            }

            inventory.transform.GetChild(2).GetComponent<AudioSource>().Play();
        }
        else if (Input.GetKeyDown(KeyCode.S) && recetID+1 < recipeList.Count)
        {
            recetID++;

            if (!inventory.transform.GetChild(2).GetComponent<AudioSource>().enabled)
            {
                inventory.transform.GetChild(2).GetComponent<AudioSource>().enabled = true;
            }

            inventory.transform.GetChild(2).GetComponent<AudioSource>().Play();
        }

        
        //mostramos de la receta en el que está el selector
        inventory.transform.GetChild(4).transform.GetChild(0).GetComponent<TMP_Text>().text = recipeBySlotList[recetID].type;

        //mostramos los ingredientes de la receta correcta
        IdentifyRecipeIgredients();

        //colocamos el selector en la casilla correcta
        selector.transform.position = inventory.transform.GetChild(0).transform.GetChild(0).transform.GetChild(recetID).transform.position;

        //mostramos los ingredientes necesarios para hacer la receta
        //IngredientsPerRecipe();
    }

    //Añadir objetos al inventario
    public void AddIngrItem(Items item)
    {
        if(ingrList.Count > 0)
        {
            for (int i = 0; i < ingrList.Count; i++)
            {
                //si ya poseemos mínimo un ingrediente de los que vamos a añadir
                if(ingrList[i].type == item.type)
                {
                    //la cantidad que ya había más la cantidad pasada 
                    ingrList[i].amount += item.amount;

                    for (int p = 0; p < itemBySlotList.Count; p++)
                    {
                        if(itemBySlotList[p].type == item.type)
                        {
                            inventory.transform.GetChild(1).transform.GetChild(p).transform.GetChild(1).GetComponent<TMP_Text>().text = ingrList[i].amount.ToString();
                            return;
                        }
                    }
                }
            }
        }

        ingrList.Add(item);

        //aumentamos en uno la cantidad de slots ocupados
        c++;

        for (int i = 0; i < itemBySlotList.Count; i++)
        {
            //cogemos el primer slot vacío que encontremos y lo activamos para el nuevo item
            //la lista de slots libres y los slots van de la mano, misma indexación
            if (itemBySlotList[i] == null)
            {
                inventory.transform.GetChild(1).transform.GetChild(i).gameObject.SetActive(true);
                itemBySlotList[i] = item;

                //buscamos la imagen correspondiente al nuevo item
                for (int j = 0; j < ingrImagesList.Count; j++)
                {
                    if (ingrImagesList[j].type == item.type)
                    {
                        //activamos para el slot la imagen correcta dependiendo del ingrediente
                        inventory.transform.GetChild(1).transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().sprite = ingrImagesList[j].itemImage;
                        inventory.transform.GetChild(1).transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().preserveAspect = true;
                        //activamos su cantidad, texto
                        inventory.transform.GetChild(1).transform.GetChild(i).transform.GetChild(1).GetComponent<TMP_Text>().text = ingrList[ingrList.Count - 1].amount.ToString();
                        return;
                    }
                }
            }
        }
    }

    //public void AddRecipeItem(Recipe item)
    //{
    //    if (ingrList.Count > 0)
    //    {
    //        for (int i = 0; i < ingrList.Count; i++)
    //        {
    //            //si ya poseemos mínimo un ingrediente de los que vamos a añadir
    //            if (ingrList[i].type == item.type)
    //            {
    //                //la cantidad que ya había más la cantidad pasada 
    //                ingrList[i].amount += item.amount;

    //                for (int p = 0; p < itemBySlotList.Count; p++)
    //                {
    //                    if (itemBySlotList[p].type == item.type)
    //                    {
    //                        inventory.transform.GetChild(1).transform.GetChild(p).transform.GetChild(1).GetComponent<TMP_Text>().text = ingrList[i].amount.ToString();
    //                        return;
    //                    }
    //                }
    //            }
    //        }
    //    }

    //    ingrList.Add(item);

    //    //aumentamos en uno la cantidad de slots ocupados
    //    c++;

    //    for (int i = 0; i < itemBySlotList.Count; i++)
    //    {
    //        //cogemos el primer slot vacío que encontremos y lo activamos para el nuevo item
    //        //la lista de slots libres y los slots van de la mano, misma indexación
    //        if (itemBySlotList[i] == null)
    //        {
    //            inventory.transform.GetChild(1).transform.GetChild(i).gameObject.SetActive(true);
    //            itemBySlotList[i] = item;

    //            //buscamos la imagen correspondiente al nuevo item
    //            for (int j = 0; j < ingrImagesList.Count; j++)
    //            {
    //                if (ingrImagesList[j].type == item.type)
    //                {
    //                    //activamos para el slot la imagen correcta dependiendo del ingrediente
    //                    inventory.transform.GetChild(1).transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().sprite = ingrImagesList[j].itemImage;
    //                    //activamos su cantidad, texto
    //                    inventory.transform.GetChild(1).transform.GetChild(i).transform.GetChild(1).GetComponent<TMP_Text>().text = ingrList[ingrList.Count - 1].amount.ToString();
    //                    return;
    //                }
    //            }
    //        }
    //    }
    //}

    public void AddRecipe(Recipe recipe)
    {
        if (recipeList.Count > 0)
        {
            for (int i = 0; i < recipeList.Count; i++)
            {
                //si ya poseemos mínimo un ingrediente de los que vamos a añadir
                if (recipeList[i].type == recipe.type)
                {
                    //la cantidad que ya había más la cantidad pasada 
                    recipeList[i].amount += recipe.amount;
                    recipeList[i].Coock.Enqueue(recipe.Coock.Dequeue());

                    for (int p = 0; p < recipeItemBySlotList.Count; p++)
                    {
                        if (recipeItemBySlotList[p].type == recipe.type)
                        {
                            inventory.transform.GetChild(7).transform.GetChild(p).transform.GetChild(1).GetComponent<TMP_Text>().text = recipeList[i].amount.ToString();

                            if (recipeList[i].amount >= 1)
                            {
                                inventory.transform.GetChild(7).transform.GetChild(p).transform.GetChild(0).GetComponent<Image>().color = Color.white;
                            }

                            return;
                        }
                    }
                }
            }
        }

        recipeList.Add(recipe);

        for (int i = 0; i < recipeBySlotList.Count; i++)
        {
            if (recipeBySlotList[i] == null)
            {
                inventory.transform.GetChild(0).transform.GetChild(0).transform.GetChild(i).gameObject.SetActive(true);
                inventory.transform.GetChild(7).transform.GetChild(i).gameObject.SetActive(true);
                recipeBySlotList[i] = recipe;
                recipeItemBySlotList[i] = recipe;
                inventory.transform.GetChild(7).transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().color = Color.grey;

                //buscamos la imagen asociada a la receta
                for (int j = 0; j < recipeImagesList.Count; j++)
                {
                    if (recipeImagesList[j].type == recipe.type)
                    {
                        recipeBySlotList[i].recipeImage = recipeImagesList[j].recipeImage;
                        recipeItemBySlotList[i].recipeImage = recipeImagesList[j].recipeImage;
                        //activamos para el slot la imagen correcta dependiendo del ingrediente
                        inventory.transform.GetChild(0).transform.GetChild(0).transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().sprite = recipeImagesList[j].recipeImage;
                        inventory.transform.GetChild(0).transform.GetChild(0).transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().preserveAspect = true;
                        inventory.transform.GetChild(7).transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().sprite = recipeImagesList[j].recipeImage;
                        inventory.transform.GetChild(7).transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().preserveAspect = true;
                        //activamos su cantidad, texto
                        inventory.transform.GetChild(7).transform.GetChild(i).transform.GetChild(1).GetComponent<TMP_Text>().text = recipeList[recipeList.Count - 1].amount.ToString();
                        return;
                    }
                }
            }
        }

    }

    public void SubstractIngrItem(Items item, int cuantity)
    {
        for (int i = 0; i < ingrList.Count; i++)
        {
            //reproducimos el sonido
            if (!inventory.transform.GetChild(1).GetComponent<AudioSource>().enabled)
            {
                inventory.transform.GetChild(1).GetComponent<AudioSource>().enabled = true;
            }

            inventory.transform.GetChild(1).GetComponent<AudioSource>().Play();

            //buscamos el tipo
            if (ingrList[i].type == item.type)
            {
                ingrList[i].amount -= cuantity;

                //si aún quedan ingredientes del tipo dado
                if (ingrList[i].amount > 0)
                {
                    for (int p = 0; p < itemBySlotList.Count; p++)
                    {
                        //actualizamos el texto (amount) del slot adecuado
                        if (itemBySlotList[p].type == item.type)
                        {
                            inventory.transform.GetChild(1).transform.GetChild(p).transform.GetChild(1).GetComponent<TMP_Text>().text = ingrList[i].amount.ToString();
                            return;
                        }
                    }
                }
                //si no quedan ingredientes del tipo dado
                else
                {
                    ingrList.Remove(ingrList[i]);

                    for (int p = 0; p < itemBySlotList.Count; p++)
                    {
                        if (itemBySlotList[p].type == item.type)
                        {
                            //desactivamos el slot que ha dejado el objeto y liberamos el espacio de la lista para que pueda volver a ser usado por otro
                            inventory.transform.GetChild(1).transform.GetChild(p).gameObject.SetActive(false);
                            itemBySlotList[p] = null;

                            //se le resta uno a la cantidad de slots ocupados
                            c--;

                            ReorganizeInventory();

                            //si no quedan items, al quitar ese se desactiva toda la información relativa a ellos y el selector
                            if (ingrList.Count == 0)
                            {
                                inventory.transform.GetChild(2).gameObject.SetActive(false);
                                inventory.transform.GetChild(4).gameObject.SetActive(false);
                            }
                            //si solo queda uno, el selector se podrá poner únicamente en un slot, el 0
                            else if (ingrList.Count == 1)
                            {
                                ingrID = 0;
                                selector.transform.position = inventory.transform.GetChild(1).transform.GetChild(ingrID).transform.position;
                            }
                            //si se elimina el último ingrediente del inventario, el selector pasará ál item anterior a este
                            //si el ID es igual a la cantidad de slots ocupada
                            else if (ingrID == c)
                            {
                                ingrID = ingrID - 1;
                                selector.transform.position = inventory.transform.GetChild(1).transform.GetChild(ingrID).transform.position;
                            }

                            return;
                        }
                    }
                }
            }
        }
    }

    //Recolocamos los slots y las posiciones de la lista asociada, este método es necesario debido
    //a la forma en la que se muestra la información, mediante el selector
    private void ReorganizeInventory()
    {
        int anterior = 0;

        for (int i = 1; i < itemBySlotList.Count; i++)
        {
            if (itemBySlotList[anterior] == null && itemBySlotList[i] != null)
            {
                itemBySlotList[anterior] = itemBySlotList[i];
                itemBySlotList[i] = null;
                inventory.transform.GetChild(1).transform.GetChild(i).gameObject.SetActive(false);

                for (int j = 0; j < ingrImagesList.Count; j++)
                {
                    //como hemos cambiado el slot recolocaremos la imagen
                    if (ingrImagesList[j].type == itemBySlotList[anterior].type)
                    {
                        inventory.transform.GetChild(1).transform.GetChild(anterior).transform.GetChild(0).GetComponent<Image>().sprite = ingrImagesList[j].itemImage;
                        inventory.transform.GetChild(1).transform.GetChild(anterior).transform.GetChild(0).GetComponent<Image>().preserveAspect = true;
                        break;
                    }
                }

                for (int t = 0; t < ingrList.Count; t++)
                {
                    //también recolocamos el texto de la cantidad
                    if (ingrList[t].type == itemBySlotList[anterior].type)
                    {
                        inventory.transform.GetChild(1).transform.GetChild(anterior).transform.GetChild(1).GetComponent<TMP_Text>().text = ingrList[t].amount.ToString();
                        break;
                    }
                }

                inventory.transform.GetChild(1).transform.GetChild(anterior).gameObject.SetActive(true);

            }

            anterior = i;
        }
    }

    private void IdentifyRecipeIgredients()
    {
        //si no es ninguna receta es basura
        int i = 0;

        if (recipeBySlotList[recetID].type == "Fartons")
        {
            i = 2;
        }
        else if(recipeBySlotList[recetID].type == "Bunyols de calabaza")
        {
            i = 4;
        }
        else if (recipeBySlotList[recetID].type == "Coca de llanda")
        {
            i = 7;
        }
        else if (recipeBySlotList[recetID].type == "Farinada")
        {
            i = 3;
        }
        else if (recipeBySlotList[recetID].type == "Pilotes de frare")
        {
            i = 5;
        }
        else if (recipeBySlotList[recetID].type == "Flaons")
        {
            i = 6;
        }
        else if (recipeBySlotList[recetID].type == "Mocadorà")
        {
            i = 9;
        }
        else if (recipeBySlotList[recetID].type == "Mona de Pascua")
        {
            i = 1;
        }
        else if (recipeBySlotList[recetID].type == "Pasteles de boniato")
        {
            i = 8;
        }

        for (int j = 1; j < 10; j++)
        {
            if (j != i)
            {
                inventory.transform.GetChild(0).transform.GetChild(j).gameObject.SetActive(false);
            }
        }

        inventory.transform.GetChild(0).transform.GetChild(i).gameObject.SetActive(true);
    }

    //quitar receta del inventario
    public void SubstractRecipeItem(Recipe recipe, int cuantity)
    {
        for (int i = 0; i < recipeList.Count; i++)
        {
            //reproducimos el sonido


            //buscamos el tipo
            if (recipeList[i].type == recipe.type)
            {
                recipeList[i].amount -= cuantity;

                //si aún quedan comidas del tipo dado
                if (recipeList[i].amount > 0)
                {
                    for (int p = 0; p < recipeItemBySlotList.Count; p++)
                    {
                        //actualizamos el texto
                        if (recipeItemBySlotList[p].type == recipe.type)
                        {
                            inventory.transform.GetChild(7).transform.GetChild(p).transform.GetChild(1).GetComponent<TMP_Text>().text = recipeList[i].amount.ToString();
                            return;
                        }
                    }
                }
                else
                {
                    for (int p = 0; p < recipeItemBySlotList.Count; p++)
                    {
                        //actualizamos el texto
                        if (recipeItemBySlotList[p].type == recipe.type)
                        {
                            inventory.transform.GetChild(7).transform.GetChild(p).transform.GetChild(1).GetComponent<TMP_Text>().text = recipeList[i].amount.ToString();
                            inventory.transform.GetChild(7).transform.GetChild(p).transform.GetChild(0).GetComponent<Image>().color = Color.grey;
                            return;
                        }
                    }
                }
            }
        }
    }

    private Items TakeItemBySelector()
    {
        Items item;

        item = itemBySlotList[ingrID];

        return item;
    }

    private Recipe TakeRecipeItemBySelector()
    {
        Recipe recipe;

        recipe = recipeItemBySlotList[comiID];

        return recipe;
    }

    public Recipe GetRecipe() 
    {
        return recipeBySlotList[comiID];
    }
}
