using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Recipe")]
public class Recipe : ScriptableObject
{
    public List<string> recipeType = new List<string>() 
    {
        
        "Mona de Pascua", "Fartons", "Bunyols de calabaza", "Pilotes de frare", "Farinada",
        "Flaons", "Coca de llanda", "Pasteles de boniato", "Mocadorà", "Basura"
    
    };

    public string type;
    public int amount;
    public Sprite recipeImage;
    public List<Items> ingredintsList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
