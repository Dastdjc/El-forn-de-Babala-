using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//para crear scriptable objects BASTANTE �TIL LA VERDAD
[CreateAssetMenu (fileName = "Item", menuName = "Item ingredient")]
public class Items : ScriptableObject
{
    public string type;
    public int amount;
    public Sprite itemImage;
}
