using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class eventSystem : MonoBehaviour
{
    public TextMeshProUGUI textPuntuacion;
    public GameObject pantallaFinal;
    [HideInInspector] public int puntuacion = 0;
    public GameObject spawner;
    public GameObject pantallaTutorial;

    public bool playing = false;
    private bool finish = false;

    private float startTime;

    // Ingredietes
    private int harina;
    private int levadura;
    private int aceite;
    private int mantequilla;
    private int azucar;

    void Start()
    {
        startTime = Time.time;
        MostrarTutorial();
    }

    void Update()
    {

        if (!finish && playing)
        {
            float t = Time.time - startTime;
            
            if (puntuacion < 0)
            {
                puntuacion = 0;
            }

            textPuntuacion.text = "Puntuacion: " + puntuacion.ToString();
            if (t > 30.00)
            {
                finish = true;
                playing = false;
            }
        }
        else if (finish)
        {
            Acabar();
            finish = false;
        }
        
    }
    void MostrarTutorial()
    {
        Animator anim_pantallaTutorial = pantallaTutorial.GetComponent<Animator>();
        pantallaTutorial.SetActive(true);
        anim_pantallaTutorial.SetTrigger("aparicion");

    }
    public void EsconderTutorial() 
    {
        Animator anim_pantallaTutorial = pantallaTutorial.GetComponent<Animator>();
        anim_pantallaTutorial.SetTrigger("desaparicion");
        playing = true;
    }
    private void Acabar()
    {
        textPuntuacion.gameObject.SetActive(false);
        spawner.SetActive(false);
        Destroy(GameObject.FindWithTag("Azul"));
        Destroy(GameObject.FindWithTag("Negra"));
        Destroy(GameObject.FindWithTag("Verde"));
        Destroy(GameObject.FindWithTag("Rojo"));

        RewardsToInventory();

        pantallaFinal.SetActive(true);
        pantallaFinal.GetComponent<Animator>().SetTrigger("aparicion");
        GameObject.Find("MatchingBerries(Clone)/Canvas/PantallaFinalMinijuegoMatchingBerries/Puntuación").GetComponent<TextMeshProUGUI>().text = "Puntuación: " + puntuacion.ToString();
    }

    private void RewardsToInventory() 
    {
        // Calcular recompensa
        int multiplicador = puntuacion / 450;
        harina = (int)(multiplicador * 3 * Random.Range(0.8f, 1f));
        levadura = (int)(multiplicador * 1 * Random.Range(0.8f, 1f));
        aceite = (int)(multiplicador * 2 * Random.Range(0.8f, 1f));
        mantequilla = (int)(multiplicador * 1 * Random.Range(0.8f, 1f));
        azucar = (int)(multiplicador * 3 * Random.Range(0.8f, 1f));

        // Añadir dentro del inventario
        Inventory inventario = GameObject.Find("INVENTORY/Inventory").GetComponent<Inventory>();

        Items itemHarina = ScriptableObject.CreateInstance<Items>();
        itemHarina.amount = harina;
        itemHarina.type = "Harina";
        if (itemHarina.amount > 0)
            inventario.AddIngrItem(itemHarina);

        Items itemLevadura = ScriptableObject.CreateInstance<Items>();
        itemLevadura.amount = levadura;
        itemLevadura.type = "Levadura";
        if (itemLevadura.amount > 0)
            inventario.AddIngrItem(itemLevadura);

        Items itemAceite = ScriptableObject.CreateInstance<Items>();
        itemAceite.amount = aceite;
        itemAceite.type = "Aceite";
        if (itemAceite.amount > 0)
            inventario.AddIngrItem(itemAceite);

        Items itemMantequilla = ScriptableObject.CreateInstance<Items>();
        itemMantequilla.amount = mantequilla;
        itemMantequilla.type = "Mantequilla";
        if (itemMantequilla.amount > 0)
            inventario.AddIngrItem(itemMantequilla);

        Items itemAzucar = ScriptableObject.CreateInstance<Items>();
        itemAzucar.amount = azucar;
        itemAzucar.type = "Azúcar";
        if (itemAzucar.amount > 0)
            inventario.AddIngrItem(itemAzucar);

    }
}
