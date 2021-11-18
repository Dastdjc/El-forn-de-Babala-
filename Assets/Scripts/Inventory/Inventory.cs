using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    //para poder abrir y cerrar el inventario
    public GameObject inventory;
    private bool inventoryOpened;
    //0 para el de ingredientes y 1 para el de recetas
    private int inventoryType;

    //selector de slot
    public GameObject selector;

    //ID se usará para saber en que slot del inventario estamos y posicionar el selector encima de éste
    private int ID;

    //esta lista, dada en el Unity, solo se usa para "robar" la imagen del objeto correcto
    public List<Items> ingrImagesList;

    //esta se usa para determinar si ya poseemos un item del tipo que vamos a añadir
    public List<Items> ingrList = new List<Items>();

    //ésta para que se sepa que tipo de item está en cada slot
    private List<Items> itemBySlotList = new List<Items>();

    //------------------------------------------------------------
    //pruebas de ingredientes
    private Items harina;
    private Items masigolem;
    private Items huevos;
    private Items calabaza;
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

        //pruebas de aumentar inventario
        //-------------------------------------------------------------------------------------------
        //la creación del objeto hay que hacerla así porque un Items es un scriptable object
        
        harina = ScriptableObject.CreateInstance<Items>();
        harina.amount = 1;
        harina.type = itemType.Harina;
        AddIngrItem(harina);

        masigolem = ScriptableObject.CreateInstance<Items>();
        masigolem.amount = 1;
        masigolem.type = itemType.Masigolem;
        AddIngrItem(masigolem);

        huevos = ScriptableObject.CreateInstance<Items>();
        huevos.amount = 1;
        huevos.type = itemType.Huevos;
        AddIngrItem(huevos);

        SubstractIngrItem(masigolem);

        calabaza = ScriptableObject.CreateInstance<Items>();
        calabaza.amount = 1;
        calabaza.type = itemType.Calabaza;
        AddIngrItem(calabaza);

        //-------------------------------------------------------------------
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            OpenCloseInventory();
        }

        //si el inventario está abierto y hay mínimo un item
        if (inventoryOpened && ingrList.Count > 0)
        {
            MoveInInventory();
        }

        //si el inventario está abierto puedes cambiar entre pestañas
        if (inventoryOpened)
        {
            ChangeInventory();
        }
    }

    private void ChangeInventory()
    {
        if (inventoryType == 0 && Input.GetKeyDown(KeyCode.E))
        {
            inventory.transform.GetChild(0).gameObject.SetActive(false);
            inventory.transform.GetChild(5).gameObject.SetActive(true);
            inventory.transform.GetChild(2).transform.GetChild(0).GetComponent<TMP_Text>().text = "RECETARIO";
            //inventory.transform.GetChild(4).transform.position = new Vector3(inventory.transform.GetChild(4).transform.position.x*(-4), inventory.transform.GetChild(4).transform.position.y, inventory.transform.GetChild(4).transform.position.z);
            inventoryType = 1;
        }
        else if (inventoryType == 1 && Input.GetKeyDown(KeyCode.E))
        {
            inventory.transform.GetChild(0).gameObject.SetActive(true);
            inventory.transform.GetChild(5).gameObject.SetActive(false);
            inventory.transform.GetChild(2).transform.GetChild(0).GetComponent<TMP_Text>().text = "INVENTARIO";
            //inventory.transform.GetChild(4).transform.position = new Vector3(inventory.transform.GetChild(4).transform.position.x/(-4), inventory.transform.GetChild(4).transform.position.y, inventory.transform.GetChild(4).transform.position.z);
            inventoryType = 0;
        }
    }

    //abrir y cerrar inventario
    private void OpenCloseInventory()
    {
        if (!inventoryOpened)
        {
            inventoryOpened = true;
            //activamos los slots ocupados, el selector, el texto Inventario y el nombre del objeto seleccionado
            inventory.transform.GetChild(0).gameObject.SetActive(true);
            inventory.transform.GetChild(2).gameObject.SetActive(true);
            inventory.transform.GetChild(4).gameObject.SetActive(true);

            if (ingrList.Count > 0)
            {
                inventory.transform.GetChild(1).gameObject.SetActive(true);
                inventory.transform.GetChild(3).gameObject.SetActive(true);
            }
        }
        else
        {
            inventoryOpened = false;
            //los desactivamos
            inventory.transform.GetChild(0).gameObject.SetActive(false);
            inventory.transform.GetChild(2).gameObject.SetActive(false);
            inventory.transform.GetChild(4).gameObject.SetActive(false);

            if (ingrList.Count > 0)
            {
                inventory.transform.GetChild(1).gameObject.SetActive(false);
                inventory.transform.GetChild(3).gameObject.SetActive(false);
            }
        }
    }

    //mover selector por el inventario
    private void MoveInInventory()
    {
        if (Input.GetKeyDown(KeyCode.D) && ID < ingrList.Count - 1)
        {
            ID++;
        }
        else if (Input.GetKeyDown(KeyCode.A) && ID > 0)
        {
            ID--;
        }
        else if (Input.GetKeyDown(KeyCode.W) && ID > 6)
        {
            ID -= 7;
        }
        else if (Input.GetKeyDown(KeyCode.S) && ingrList.Count >= (ID + 7) && (ID+7) != ingrList.Count)
        {
            ID += 7;
        }

        //colocamos el selector en la casilla correcta
        selector.transform.position = inventory.transform.GetChild(0).transform.GetChild(ID).transform.position;

        //mostramos el nombre del objeto en el que está el selector
        inventory.transform.GetChild(3).transform.GetChild(0).GetComponent<TMP_Text>().text = itemBySlotList[ID].type.ToString();
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
                            inventory.transform.GetChild(0).transform.GetChild(p).transform.GetChild(1).GetComponent<TMP_Text>().text = ingrList[i].amount.ToString();
                            return;
                        }
                    }
                }
            }
        }

        ingrList.Add(item);

        for (int i = 0; i < 21; i++)
        {
            //cogemos el primer slot vacío que encontremos y lo activamos para el nuevo item
            //la lista de slots libres y los slots van de la mano, misma indexación
            if (itemBySlotList[i] == null)
            {
                inventory.transform.GetChild(0).transform.GetChild(i).gameObject.SetActive(true);
                itemBySlotList[i] = item;

                //buscamos la imagen correspondiente al nuevo item
                for (int j = 0; j < ingrImagesList.Count; j++)
                {
                    if (ingrImagesList[j].type == item.type)
                    {
                        //activamos para el slot la imagen correcta dependiendo del ingrediente
                        inventory.transform.GetChild(0).transform.GetChild(i).transform.GetChild(0).GetComponent<Image>().sprite = ingrImagesList[j].itemImage;
                        //activamos su cantidad, texto
                        inventory.transform.GetChild(0).transform.GetChild(i).transform.GetChild(1).GetComponent<TMP_Text>().text = ingrList[ingrList.Count - 1].amount.ToString();
                        return;
                    }
                }
            }
        }
    }

    public void SubstractIngrItem(Items item)
    {
        if(ingrList.Count > 0)
        {
            for (int i = 0; i < ingrList.Count; i++)
            {
                //buscamos el tipo
                if (ingrList[i].type == item.type)
                {
                    ingrList[i].amount -= item.amount;

                    //si aún quedan ingredientes del tipo dado
                    if(ingrList[i].amount > 0)
                    {
                        for (int p = 0; p < itemBySlotList.Count; p++)
                        {
                            //actualizamos el texto (amount) del slot adecuado
                            if (itemBySlotList[p].type == item.type)
                            {
                                inventory.transform.GetChild(0).transform.GetChild(p).transform.GetChild(1).GetComponent<TMP_Text>().text = ingrList[i].amount.ToString();
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
                                inventory.transform.GetChild(0).transform.GetChild(p).gameObject.SetActive(false);
                                itemBySlotList[p] = null;
                                return;
                            }
                        }
                    }
                }
            }
        }
    }
}
