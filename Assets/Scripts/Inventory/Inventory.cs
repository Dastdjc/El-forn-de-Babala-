using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    //para poder quitar objetos del inventario
    public bool touchingTable;

    //para poder abrir y cerrar el inventario
    public GameObject inventory;
    private bool inventoryOpened;

    //0 para el de ingredientes y 1 para el de recetas
    private int inventoryType;

    //objeto que tomamos del inventario para usarlo en la cocina
    private Items givenItem;

    //selector de slot
    public GameObject selector;

    //variable usada para saber cuántos slots ocupados hay, slots != null
    private int c = 0;

    //ID se usará para saber en que slot del inventario estamos y posicionar el selector encima de éste
    private int ID;

    //esta lista, dada en el Unity, solo se usa para "robar" la imagen del objeto correcto
    public List<Items> ingrImagesList;

    //esta se usa para determinar si ya poseemos un item del tipo que vamos a añadir
    public List<Items> ingrList = new List<Items>();

    //ésta para que se sepa que tipo de item está en cada slot
    private List<Items> itemBySlotList = new List<Items>();

    //esta se usa para determinar si ya poseemos una receta del tipo que vamos a añadir
    public List<Items> recipeList = new List<Items>();

    //lista que recoge los distintos ingredientes especiales
    private List<string> specialIngredientsList = new List<string> { "Masigolem", "Huevos celestes", "Leche de dragona", "Azucar estelar", "Queso lunar", "Mantemimo", "O'Lantern", "Limoncio" };

    //------------------------------------------------------------
    //pruebas de ingredientes
    private Items harina;
    private Items masigolem;
    private Items huevos;
    private Items calabaza;
    private Items aceite;
    private Items huevosCelestes;
    private Items agua;
    private Items limoncio;
    private Items requeson;
    //----------------------------------------------------------

    private void Awake()
    {
        for (int i = 0; i < 21; i++)
        {
            //establecemos a null toda la lista
            itemBySlotList.Add(null);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

        inventoryOpened = false;
        inventoryType = 0;
        touchingTable = false;

        //pruebas de aumentar inventario
        //-------------------------------------------------------------------------------------------
        //la creación del objeto hay que hacerla así porque un Items es un scriptable object

        harina = ScriptableObject.CreateInstance<Items>();
        harina.amount = 5;
        harina.type = "Harina";
        AddIngrItem(harina);

        masigolem = ScriptableObject.CreateInstance<Items>();
        masigolem.amount = 2;
        masigolem.type = "Masigolem";
        AddIngrItem(masigolem);

        huevos = ScriptableObject.CreateInstance<Items>();
        huevos.amount = 4;
        huevos.type = "Huevos";
        AddIngrItem(huevos);

        requeson = ScriptableObject.CreateInstance<Items>();
        requeson.amount = 5;
        requeson.type = "Requesón";
        AddIngrItem(requeson);

        limoncio = ScriptableObject.CreateInstance<Items>();
        limoncio.amount = 3;
        limoncio.type = "Limoncio";
        AddIngrItem(limoncio);


        //SubstractIngrItem(masigolem, 2);

        aceite = ScriptableObject.CreateInstance<Items>();
        aceite.amount = 2;
        aceite.type = "Aceite";
        AddIngrItem(aceite);

        huevosCelestes = ScriptableObject.CreateInstance<Items>();
        huevosCelestes.amount = 1;
        huevosCelestes.type = "Huevos celestes";
        AddIngrItem(huevosCelestes);

        agua = ScriptableObject.CreateInstance<Items>();
        agua.amount = 8;
        agua.type = "Agua";
        AddIngrItem(agua);

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

        //si el inventario está abierto y hay mínimo un item
        if (inventoryOpened)
        {
            if (inventoryType == 0 && ingrList.Count > 0 || inventoryType == 1 && recipeList.Count > 0)
            {
                MoveInInventory();
            }

            //si el inventario está abierto puedes cambiar entre pestañas
            ChangeInventory();

            //también puedes seleccionar un objeto del inventario para restarle la cantidad necesaria a un ingrediente
            if (Input.GetKeyDown(KeyCode.F) && ingrList.Count > 0 && touchingTable)
            {
                givenItem = TakeItemBySelector();
                SubstractIngrItem(givenItem, 1);
            }
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

            if (recipeList.Count == 0)
            {
                inventory.transform.GetChild(2).gameObject.SetActive(false);
                inventory.transform.GetChild(4).gameObject.SetActive(false);
            }
            else
            {
                inventory.transform.GetChild(2).gameObject.SetActive(true);
                inventory.transform.GetChild(4).gameObject.SetActive(true);
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
    private void OpenCloseInventory()
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

                if (ingrList.Count > 0)
                {
                    inventory.transform.GetChild(2).gameObject.SetActive(true);
                    inventory.transform.GetChild(4).gameObject.SetActive(true);
                }

            //último inventario abierto el de recetas
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
                }
            }
        }
        else
        {
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

                //último inventario abierto el de recetas
            }
            else
            {
                inventory.transform.GetChild(0).gameObject.SetActive(false);
                inventory.transform.GetChild(6).gameObject.SetActive(false);
            }
        }
    }

    //mover selector por el inventario
    private void MoveInInventory()
    {
        int auxID = ID;

        if (Input.GetKeyDown(KeyCode.D) && ID < ingrList.Count - 1)
        {
            ID++;

            if (!inventory.transform.GetChild(2).GetComponent<AudioSource>().enabled)
            {
                inventory.transform.GetChild(2).GetComponent<AudioSource>().enabled = true;
            }

            inventory.transform.GetChild(2).GetComponent<AudioSource>().Play();
        }
        else if (Input.GetKeyDown(KeyCode.A) && ID > 0)
        {
            ID--;

            if (!inventory.transform.GetChild(2).GetComponent<AudioSource>().enabled)
            {
                inventory.transform.GetChild(2).GetComponent<AudioSource>().enabled = true;
            }

            inventory.transform.GetChild(2).GetComponent<AudioSource>().Play();
        }
        else if (Input.GetKeyDown(KeyCode.W) && ID > 6)
        {
            ID -= 7;

            if (!inventory.transform.GetChild(2).GetComponent<AudioSource>().enabled)
            {
                inventory.transform.GetChild(2).GetComponent<AudioSource>().enabled = true;
            }

            inventory.transform.GetChild(2).GetComponent<AudioSource>().Play();
        }
        else if (Input.GetKeyDown(KeyCode.S) && ingrList.Count >= (ID + 7) && (ID+7) != ingrList.Count)
        {
            ID += 7;

            if (!inventory.transform.GetChild(2).GetComponent<AudioSource>().enabled)
            {
                inventory.transform.GetChild(2).GetComponent<AudioSource>().enabled = true;
            }

            inventory.transform.GetChild(2).GetComponent<AudioSource>().Play();
        }

        //mostramos el nombre del objeto en el que está el selector
        inventory.transform.GetChild(4).transform.GetChild(0).GetComponent<TMP_Text>().text = itemBySlotList[ID].type;

        //colocamos el selector en la casilla correcta
        selector.transform.position = inventory.transform.GetChild(1).transform.GetChild(ID).transform.position;

        //mostramos las estrellas al lado del nombre si el ingrediente es especial
        for (int i = 0; i < specialIngredientsList.Count; i++)
        {
            if (itemBySlotList[ID].type == specialIngredientsList[i])
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
                        //activamos su cantidad, texto
                        inventory.transform.GetChild(1).transform.GetChild(i).transform.GetChild(1).GetComponent<TMP_Text>().text = ingrList[ingrList.Count - 1].amount.ToString();
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
                                ID = 0;
                                selector.transform.position = inventory.transform.GetChild(1).transform.GetChild(ID).transform.position;
                            }
                            //si se elimina el último ingrediente del inventario, el selector pasará ál item anterior a este
                            //si el ID es igual a la cantidad de slots ocupada
                            else if (ID == c)
                            {
                                ID = ID - 1;
                                selector.transform.position = inventory.transform.GetChild(1).transform.GetChild(ID).transform.position;
                            }

                            return;
                        }
                    }
                }
            }
        }
    }

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


    private Items TakeItemBySelector()
    {
        Items item;

        item = itemBySlotList[ID];

        return item;
    }
}
