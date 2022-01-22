using UnityEngine.UI;
using UnityEngine;

public class RecetaSeleccionada : MonoBehaviour
{
    public Sprite spriteReceta;
    public Image objetoImagen;
    public GameObject shadow;
    private Recipe receta;
    // Update is called once per frame
    void Update()
    {
        receta = Inventory.Instance.GetRecipe();
        if (receta.amount == 0)
            shadow.SetActive(true);
        else
            shadow.SetActive(false);

        spriteReceta = receta.recipeImage;
        objetoImagen.sprite = spriteReceta;
    }
}
