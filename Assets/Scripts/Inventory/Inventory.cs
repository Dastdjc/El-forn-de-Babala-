using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    //para poder quitar objetos del inventario
    //Usa la variable estatica TableController.isOnColision

    //para poder abrir y cerrar el inventario
    public GameObject inventory;
    private bool inventoryOpened;

    //0 para el de ingredientes y 1 para el de recetas
    private int inventoryType;

    //objeto que tomamos del inventario para usarlo en la cocina
    private Items givenItem;

    //selector de slot
    public GameObject selector;

    //variable usada para saber cu�ntos slots ocupados hay, slots != null
    private int c = 0;

    //ID se usar� para saber en que slot del inventario estamos y posicionar el selector encima de �ste
    private int ingrID;
    private int recetID;

    //esta lista, dada en el Unity, solo se usa para "robar" la imagen del objeto correcto
    public List<Items> ingrImagesList;

    //esta lista, dada en el Unity, solo se usa para "robar" la imagen de la receta correcta
    public List<Recipe> recipeImagesList;

    public GameObject Spawner;

    //esta se usa para determinar si ya poseemos un item del tipo que vamos a a�adir
    private List<Items> ingrList = new List<Items>();

    //�sta para que se sepa que tipo de item est� en cada slot
    private List<Items> itemBySlotList = new List<Items>();

    //�sta para que se sepa que tipo de recta est� en cada slot
    private List<Recipe> recipeBySlotList = new List<Recipe>();

    //esta se usa para determinar si ya poseemos una receta del tipo que vamos a a�adir y cu�ntos platos de esa receta poseemos
    private List<Recipe> recipeList = new List<Recipe>();

    //lista que recoge los distintos ingredientes especiales
    private List<string> specialIngredientsList = new List<string> { "Masigolem", "Huevos celestes", "Leche de dragona", "Azucar estelar", "Queso lunar", "Mantemimo", "O'Lantern", "Limoncio" };

    //------------------------------------------------------------
    //pruebas de ingredientes
    private Items harina;
    private Items levadura;
    private Items leche;
    private Items mantequilla;
    private Items azucar;
    private Items masigolem;
    private Items huevos;
    private Items calabaza;
    private Items aceite;
    private Items huevosCelestes;
    private Items agua;
    private Items limoncio;
    private Items requeson;

    /*private Recipe mocadora;
    private Recipe fartons;
    private Recipe coca;
    private Recipe farinada;*/

    //----------------------------------------------------------

    private void Awake()
    {
        //para que el inventario se mantenga entre escenas NO FUNCIONA
        DontDestroyOnLoad(this.gameObject);

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
    }

    // Start is called before the first frame update
    void Start()
    {

        inventoryOpened = false;
        inventoryType = 0;
        ingrID = 0;
        recetID = 0;

        //pruebas de aumentar inventario
        //-------------------------------------------------------------------------------------------
        //la creaci�n del objeto hay que hacerla as� porque un Items es un scriptable object
        
        harina = ScriptableObject.CreateInstance<Items>();
        harina.amount = 5;
        harina.type = "Harina";
        AddIngrItem(harina);

        huevos = ScriptableObject.CreateInstance<Items>();
        huevos.amount = 4;
        huevos.type = "Huevos";
        AddIngrItem(huevos);

        levadura = ScriptableObject.CreateInstance<Items>();
        levadura.amount = 4;
        levadura.type = "Levadura";
        AddIngrItem(levadura);

        leche = ScriptableObject.CreateInstance<Items>();
        leche.amount = 4;
        leche.type = "Leche";
        AddIngrItem(leche);

        mantequilla = ScriptableObject.CreateInstance<Items>();
        mantequilla.amount = 4;
        mantequilla.type = "Mantequilla";
        AddIngrItem(mantequilla);

        azucar = ScriptableObject.CreateInstance<Items>();
        azucar.amount = 4;
        azucar.type = "Az�car";
        AddIngrItem(azucar);

        /*requeson = ScriptableObject.CreateInstance<Items>();
        requeson.amount = 5;
        requeson.type = "Reques�n";
        AddIngrItem(requeson);

        limoncio = ScriptableObject.CreateInstance<Items>();
        limoncio.amount = 3;
        limoncio.type = "Limoncio";
        AddIngrItem(limoncio);*/

        /*masigolem = ScriptableObject.CreateInstance<Items>();
        masigolem.amount = 2;
        masigolem.type = "Masigolem";
        AddIngrItem(masigolem);


        //SubstractIngrItem(masigolem, 2);

        aceite = ScriptableObject.CreateInstance<Items>();
        aceite.amount = 2;
        aceite.type = "Aceite";
        AddIngrItem(aceite);

        /*huevosCelestes = ScriptableObject.CreateInstance<Items>();
        huevosCelestes.amount = 1;
        huevosCelestes.type = "Huevos celestes";
        AddIngrItem(huevosCelestes);

        agua = ScriptableObject.CreateInstance<Items>();
        agua.amount = 8;
        agua.type = "Agua";
        AddIngrItem(agua);

        //creaci�n de recetas de prueba

        mocadora = ScriptableObject.CreateInstance<Recipe>();
        mocadora.amount = 4;
        mocadora.type = "Mocador�";
        AddRecipe(mocadora);

        fartons = ScriptableObject.CreateInstance<Recipe>();
        fartons.amount = 1;
        fartons.type = "Fartons";
        AddRecipe(fartons);

        coca = ScriptableObject.CreateInstance<Recipe>();
        coca.amount = 6;
        coca.type = "Coca de llanda";
        AddRecipe(coca);

        farinada = ScriptableObject.CreateInstance<Recipe>();
        farinada.amount = 9;
        farinada.type = "Farinada";
        AddRecipe(farinada);*/

        //-------------------------------------------------------------------
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            //sonido abrir inventario
            if (!inventory.GetComponent<AudioSource>().enabled)
            {
                inventory.GetComponent<AudioSource>().enabled = true;
            }
            inventory.GetComponent<AudioSource>().Play();

            OpenCloseInventory();
        }

        //si el inventario est� abierto y hay m�nimo un item
        if (inventoryOpened)
        {
            //si estamos viendo el ingredentario
            if (inventoryType == 0 && ingrList.Count > 0)
            {
                MoveInIngredentario();

                //tambi�n puedes seleccionar un objeto del inventario para restarle la cantidad necesaria a un ingrediente
                if (Input.GetKeyDown(KeyCode.F) && TableController.isOnColision)
                {
                    givenItem = TakeItemBySelector();
                    TableController.PutIngredient(givenItem.type);
                    SubstractIngrItem(givenItem, 1);
                }
            }
            //si estamos viendo el recetario
            else if (inventoryType == 1 && recipeList.Count > 0)
            {
                MoveInRecetario();

                //tambi�n puedes seleccionar un objeto del inventario para restarle la cantidad necesaria a un ingrediente
                int i = SpawnCustomers.WhichToching();
                if (Input.GetKeyDown(KeyCode.F) && i < 5)
                {
                    //quitaremos uno a la cantidad de platos que tengamos de una receta, siempre que tengamos m�nimo uno
                    SpawnCustomers.Customers[i].GetComponent<CustomerController>().SetSatisfaction(null);
                }
            }


            //si el inventario est� abierto puedes cambiar entre pesta�as
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
            inventory.transform.GetChild(0).gameObject.SetActive(true);
            inventory.transform.GetChild(3).transform.GetChild(0).GetComponent<TMP_Text>().text = "RECETARIO";
            inventory.transform.GetChild(5).gameObject.SetActive(false);
            inventory.transform.GetChild(6).gameObject.SetActive(true);
            inventory.transform.GetChild(4).transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);

            if (recipeList.Count == 0)
            {
                //tambi�n habr�a que poner el selector en el primer slot del recetario
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

            //sonido del cambio de p�gina
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

            //sonido cambio p�gina
            if (!inventory.transform.GetChild(3).GetComponent<AudioSource>().enabled)
            {
                inventory.transform.GetChild(3).GetComponent<AudioSource>().enabled = true;
            }

            inventory.transform.GetChild(3).GetComponent<AudioSource>().Play();
        }
    }

    //abrir y cerrar inventario
    private void OpenCloseInventory()
    {
        if (!inventoryOpened)
        {
            Time.timeScale = 0;
            inventoryOpened = true;

            //�ltimo inventario abierto el de ingredientes
            if (inventoryType == 0)
            {
                //activamos los slots ocupados, el selector, el texto Inventario y el nombre del objeto seleccionado
                inventory.transform.GetChild(1).gameObject.SetActive(true);
                inventory.transform.GetChild(3).gameObject.SetActive(true);
                inventory.transform.GetChild(5).gameObject.SetActive(true);

                if (ingrList.Count > 0)
                {
                    inventory.transform.GetChild(2).gameObject.SetActive(true);
                    inventory.transform.GetChild(4).gameObject.SetActive(true);
                }

            //�ltimo inventario abierto el de recetas
            }
            else
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
        }
        else
        {
            Time.timeScale = 1;
            inventoryOpened = false;
            inventory.transform.GetChild(3).GetComponent<AudioSource>().enabled = false;
            inventory.transform.GetChild(2).GetComponent<AudioSource>().enabled = false;
            inventory.transform.GetChild(1).GetComponent<AudioSource>().enabled = false;
            inventory.transform.GetChild(3).gameObject.SetActive(false);

            if (inventoryType == 0 && ingrList.Count > 0 || inventoryType == 1 && recipeList.Count > 0)
            {
                inventory.transform.GetChild(2).gameObject.SetActive(false);
                inventory.transform.GetChild(4).gameObject.SetActive(false);
            }

            if (inventoryType == 0)
            {
                //activamos los slots ocupados, el selector, el texto Inventario y el nombre del objeto seleccionado
                inventory.transform.GetChild(1).gameObject.SetActive(false);
                inventory.transform.GetChild(5).gameObject.SetActive(false);

                //�ltimo inventario abierto el de recetas
            }
            else
            {
                inventory.transform.GetChild(0).gameObject.SetActive(false);
                inventory.transform.GetChild(6).gameObject.SetActive(false);
            }
        }
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

        //mostramos el nombre del objeto en el que est� el selector
        inventory.transform.GetChild(4).transform.GetChild(0).GetComponent<TMP_Text>().text = itemBySlotList[ingrID].type;

        //colocamos el selector en la casilla correcta
        selector.transform.position = inventory.transform.GetChild(1).transform.GetChild(ingrID).transform.position;

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
    }

    //mover selector por el recetario
    //FALTAN LOS L�MITES DEL SELECTOR
    private void MoveInRecetario()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            recetID += 3;

            if (!inventory.transform.GetChild(2).GetComponent<AudioSource>().enabled)
            {
                inventory.transform.GetChild(2).GetComponent<AudioSource>().enabled = true;
            }

            inventory.transform.GetChild(2).GetComponent<AudioSource>().Play();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            recetID -= 3;

            if (!inventory.transform.GetChild(2).GetComponent<AudioSource>().enabled)
            {
                inventory.transform.GetChild(2).GetComponent<AudioSource>().enabled = true;
            }

            inventory.transform.GetChild(2).GetComponent<AudioSource>().Play();
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            recetID--;

            if (!inventory.transform.GetChild(2).GetComponent<AudioSource>().enabled)
            {
                inventory.transform.GetChild(2).GetComponent<AudioSource>().enabled = true;
            }

            inventory.transform.GetChild(2).GetComponent<AudioSource>().Play();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            recetID++;

            if (!inventory.transform.GetChild(2).GetComponent<AudioSource>().enabled)
            {
                inventory.transform.GetChild(2).GetComponent<AudioSource>().enabled = true;
            }

            inventory.transform.GetChild(2).GetComponent<AudioSource>().Play();
        }

        
        //mostramos de la receta en el que est� el selector
        inventory.transform.GetChild(4).transform.GetChild(0).GetComponent<TMP_Text>().text = recipeBySlotList[recetID].type;
        

        //colocamos el selector en la casilla correcta
        selector.transform.position = inventory.transform.GetChild(0).transform.GetChild(0).transform.GetChild(recetID).transform.position;

        //mostramos los ingredientes necesarios para hacer la receta
        IngredientsPerRecipe();
    }

    //A�adir objetos al inventario
    public void AddIngrItem(Items item)
    {
        if(ingrList.Count > 0)
        {
            for (int i = 0; i < ingrList.Count; i++)
            {
                //si ya poseemos m�nimo un ingrediente de los que vamos a a�adir
                if(ingrList[i].type == item.type)
                {
                    //la cantidad que ya hab�a m�s la cantidad pasada 
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
            //cogemos el primer slot vac�o que encontremos y lo activamos para el nuevo item
            //la lista de slots libres y los slots van de la mano, misma indexaci�n
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
                        //activamos su cantidad, texto
                        inventory.transform.GetChild(1).transform.GetChild(i).transform.GetChild(1).GetComponent<TMP_Text>().text = ingrList[ingrList.Count - 1].amount.ToString();
                        return;
                    }
                }
            }
        }
    }

    public void AddRecipe(Recipe recipe)
    {
        if (recipeList.Count > 0)
        {
            for (int i = 0; i < recipeList.Count; i++)
            {
                //si ya poseemos m�nimo un ingrediente de los que vamos a a�adir
                if (recipeList[i].type == recipe.type)
                {
                    //la cantidad que ya hab�a m�s la cantidad pasada 
                    recipeList[i].amount += recipe.amount;

                    for (int p = 0; p < recipeBySlotList.Count; p++)
                    {
                        if (recipeBySlotList[p].type == recipe.type)
                        {
                            inventory.transform.GetChild(0).transform.GetChild(0).transform.GetChild(p).transform.GetChild(1).GetComponent<TMP_Text>().text = recipeList[i].amount.ToString();
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
                recipeBySlotList[i] = recipe;

                //buscamos la imagen asociada a la receta
                for (int j = 0; j < recipeImagesList.Count; j++)
                {
                    if (recipeImagesList[j].type == recipe.type)
                    {
                        //activamos para el slot la imagen correcta dependiendo del ingrediente
                        inventory.transform.GetChild(0).transform.GetChild(0).transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().sprite = recipeImagesList[j].recipeImage;
                        //activamos su cantidad, texto
                        inventory.transform.GetChild(0).transform.GetChild(0).transform.GetChild(i).transform.GetChild(1).GetComponent<TMP_Text>().text = recipeList[recipeList.Count - 1].amount.ToString();
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

                //si a�n quedan ingredientes del tipo dado
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

                            //si no quedan items, al quitar ese se desactiva toda la informaci�n relativa a ellos y el selector
                            if (ingrList.Count == 0)
                            {
                                inventory.transform.GetChild(2).gameObject.SetActive(false);
                                inventory.transform.GetChild(4).gameObject.SetActive(false);
                            }
                            //si solo queda uno, el selector se podr� poner �nicamente en un slot, el 0
                            else if (ingrList.Count == 1)
                            {
                                ingrID = 0;
                                selector.transform.position = inventory.transform.GetChild(1).transform.GetChild(ingrID).transform.position;
                            }
                            //si se elimina el �ltimo ingrediente del inventario, el selector pasar� �l item anterior a este
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

    //Recolocamos los slots y las posiciones de la lista asociada, este m�todo es necesario debido
    //a la forma en la que se muestra la informaci�n, mediante el selector
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
                        break;
                    }
                }

                for (int t = 0; t < ingrList.Count; t++)
                {
                    //tambi�n recolocamos el texto de la cantidad
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

    private Items TakeItemBySelector()
    {
        Items item;

        item = itemBySlotList[ingrID];

        return item;
    }

    private void IngredientsPerRecipe()
    {
        /*
        foreach (Items ingredient in ingredientsPerSlotList)
        {
            //inventory.transform.GetChild(0).transform.GetChild(1).transform.GetChild(i).transform.GetChild(0).GetComponent<TMP_Text>().text = 
            inventory.transform.GetChild(0).transform.GetChild(1).transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().sprite = ingredient.itemImage;
            i++;
        }
        */
    }
}
