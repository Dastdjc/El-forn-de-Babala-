using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum itemType
{
    Harina,
    Levadura,
    Leche,
    Mantequilla,
    Azúcar,
    Huevos,
    Aceite,
    Agua,
    Limón,
    Requesón,
    Almendra,
    Boniato,
    Calabaza,
    Masigolem,
    HuevosCelestes,
    LecheDragona,
    AzucarEstelar,
    QuesoLunar,
    Mantemimo,
    OLantern,
    Limoncio,
}

//para crear scriptable objects BASTANTE ÚTIL LA VERDAD
[CreateAssetMenu (fileName = "Item", menuName = "Item ingredient")]
public class Items : ScriptableObject
{
    public itemType type;
    public int amount;
    public Sprite itemImage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
