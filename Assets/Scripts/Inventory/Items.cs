using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//para crear scriptable objects BASTANTE �TIL LA VERDAD
[CreateAssetMenu (fileName = "Item", menuName = "Item ingredient")]
public class Items : ScriptableObject
{
    public List<string> itemType = new List<string>()
    {

        "Harina", "Levadura", "Leche", "Mantequilla", "Az�car", "Huevos", "Aceite", "Agua",
        "Lim�n", "Reques�n", "Almendra", "Boniato", "Calabaza", "Masigolem", "Huevos celestes", 
        "Leche de dragona", "Az�car estelar", "Queso lunar", "Mantemimo", "O'Lantern", "Limoncio"

    };

    public string type;
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
