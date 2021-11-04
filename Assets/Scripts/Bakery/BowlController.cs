using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlController : MonoBehaviour
{
    private Transform Content;
    private List<string> ingredients;
    // Start is called before the first frame update
    void Start()
    {
        Content = transform.GetChild(0);
        ingredients = new List<string>();
    }
    public void MoveContent(string name)
    {
        ingredients.Add(name);
        Content.position = Content.position + new Vector3(0, 0.1f, 0);
    }
}
