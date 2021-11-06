using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KilnController : MonoBehaviour
{
    public string Myname;
    static public int temp;
    static public bool open = false;
    static private GameObject[] objectEntering;
    static private int[] TimeInside;
    private void Start()
    {
        gameObject.name = Myname;
        temp = 10;
        objectEntering = new GameObject[6];
        TimeInside = new int[6];
        for(int i = 0; i < 6; i++)
        {
            TimeInside[i] = -1;
        }
    }
    private void OnMouseDown()
    {
        switch (Myname)
        {
            case "Tape":
                if (open)
                {
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(0.25f, 0, 1);
                    gameObject.GetComponent<SpriteRenderer>().sortingOrder = 3;
                }
                else
                {
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0.3f, 0);
                    gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
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
        Debug.Log(temp);
    }
    private void FixedUpdate()
    {
        for (int i = 0; i < TimeInside.Length; i++)
        {
            if (/*TimeInside[i] != -1*/ objectEntering[i] != null)
            {
                Color c = objectEntering[i].GetComponent<SpriteRenderer>().color;
                //TimeInside[i] += 1;
                objectEntering[i].GetComponent<SpriteRenderer>().color = new Color(c.r - temp * 0.00001f, c.g - temp * 0.00001f, c.b - temp * 0.00001f);
            }
        }
    }
    private void OnMouseUp()
    {
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
                Destroy(objectEntering[i].GetComponent<FoodController>());
                objectEntering[i].transform.localScale = new Vector3(0.5f, 0.5f, 1);
                if (i > 2)
                    objectEntering[i].transform.position = new Vector3(i - 3 * 1 + 29, -2.5f, 0);
                else
                    objectEntering[i].transform.position = new Vector3(i * 1 + 29, -1.5f, 0);
                objectEntering[i].GetComponent<SpriteRenderer>().sortingOrder = 2;
                TimeInside[i] = 0;
                return true;
            }
        }
        return false;
    }
}
