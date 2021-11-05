using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KilnController : MonoBehaviour
{
    public string Myname;
    static public int temp;
    static public bool open = false;
    private void Start()
    {
        gameObject.name = Myname;
        temp = 10;
    }
    private void OnMouseDown()
    {
        switch (Myname)
        {
            case "Tape":
                if (open)
                {
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(0.25f, 0, 1);
                    gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
                }
                else
                {
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0.3f, 0);
                    gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
                }
                open = !open;
                break;
            case "CircleUp":
                temp += 10;
                break;
            case "CircleDown":
                temp -= 10;
                break;
        }
        Debug.Log(temp);
    }
}
