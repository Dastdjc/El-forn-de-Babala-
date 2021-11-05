using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KilnController : MonoBehaviour
{
    public string Myname;
    static public int temp;
    static public bool open = false;
    static private GameObject[] objectEntering;
    private void Start()
    {
        gameObject.name = Myname;
        temp = 10;
        objectEntering = new GameObject[6];
    }
    private void OnMouseDown()
    {
        switch (Myname)
        {
            case "Tape":
                if (open)
                {
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(0.25f, 0, 1);
                }
                else
                {
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0.3f, 0);
                }
                open = !open;
                break;
            case "CircleUp":
                if(temp < 220)
                    temp += 10;
                break;
            case "CircleDown":
                if(temp > 0)
                    temp -= 10;
                break;
        }
        //Debug.Log(temp);
    }
    private void OnMouseUp()
    {
        //if(open && objectEntering != null)
    }
    public bool GetToCook(GameObject me)
    {
        if (Myname == "Tape")
        {
            int i = 0;
            while (i < objectEntering.Length && objectEntering[i] != null) { i++; }
            if (i < objectEntering.Length)
            {
                objectEntering[i] = Instantiate(me);
                Destroy(objectEntering[i].GetComponent<IWantToDie>());
                objectEntering[i].transform.localScale = new Vector3(0.5f, 0.5f, 1);
                if (i > 2)
                    objectEntering[i].transform.position = new Vector3(i - 3 * 1 + 29, -2.5f, 0);
                else
                    objectEntering[i].transform.position = new Vector3(i * 1 + 29, -1.5f, 0);
                
                return true;
            }
        }
        return false;
    }
}
