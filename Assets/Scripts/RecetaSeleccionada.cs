using UnityEngine.UI;
using UnityEngine;

public class RecetaSeleccionada : MonoBehaviour
{
    public Sprite spriteReceta;
    public Image objetoImagen;
    // Update is called once per frame
    void Update()
    {
        spriteReceta = Inventory.Instance.GetRecipe().recipeImage;
        objetoImagen.sprite = spriteReceta;
    }
}
