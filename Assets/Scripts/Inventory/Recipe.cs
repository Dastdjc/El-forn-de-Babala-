using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Recipe")]
public class Recipe : ScriptableObject
{
    public string type;
    public int amount;
    public Sprite recipeImage;
    public Queue<int> Coock;
}
