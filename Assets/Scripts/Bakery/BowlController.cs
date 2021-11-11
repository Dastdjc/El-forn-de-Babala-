using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlController : MonoBehaviour
{
    private Transform Content;
    private List<string> ingredients;
    static private float Upmov = 0.05f;
    private int Mixture = 0;
    // Start is called before the first frame update
    void Start()
    {
        if(Content == null)Content = transform.GetChild(0);
        if(ingredients != null)
        {
            for (int i = 0; i < ingredients.Count; i++)
            {
                Content.transform.position -= new Vector3(0, Upmov, 0);
            }
        }
        ingredients = new List<string>();
    }
    public void MoveContent(string name)
    {
        if (Mixture != 0) { ingredients.Add(Mixture.ToString()); }
        ingredients.Add(name);
        Content.transform.position += new Vector3(0, Upmov, 0);
        Mixture = 0;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9 && gameObject.GetComponent<MouseTracker>().isHeld)
        {
            for (int i = 0; i < ingredients.Count; i++)
            {
                collision.transform.GetComponent<BowlController>().MoveContent(ingredients[i]);
            }
            Start();
        }
        else if (collision.gameObject.layer == 10 && collision.transform.GetComponent<KilnController>().ImOpen())
        {
            string[] confirmedingredients = new string[ingredients.Count];
            for (int i = 0; i < ingredients.Count; i++) { confirmedingredients[i] = ingredients[i]; }
            collision.gameObject.GetComponent<KilnController>().CookSprite(confirmedingredients);
            Start();
        }
        else if (collision.gameObject.layer == 11) { Mixture += 1; Debug.Log("+1"); }
    }
}
