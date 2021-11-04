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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hola");
        ingredients.Add(collision.transform.GetComponent<FoodController>().FoodName);
        Content.position = Content.position + new Vector3(0, 0.1f, 0);
        Debug.Log(ingredients[-1]);
    }
}
