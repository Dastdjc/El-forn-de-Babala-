using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KilnController : MonoBehaviour
{
    private bool open = false;
    private GameObject[] objectEntering = new GameObject[6];
    private int[] timeInside = new int[6];
    public GameObject FoodToCook;
    public Sprite[] FoodVisuals;
    private void OnMouseDown()
    {
        if (open)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0.25f, 0, 1);
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 4;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0.3f, 0);
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
        }
        open = !open;
    }
    private void Update()
    {
        for (int i = 0; i < objectEntering.Length; i++)
        {
            if (objectEntering[i] != null)
            {
                objectEntering[i].GetComponent<SpriteRenderer>().color += new Color(0.01f, 0.01f, 0.01f);
                timeInside[i] += 1;
            }
        }
    }
    public int GetToCook()
    {
        int i = 0;
        while (i < objectEntering.Length && objectEntering[i] != null) { i++; }
        if (i < objectEntering.Length)
        {
            objectEntering[i] = Instantiate(FoodToCook);
            objectEntering[i].gameObject.layer = 7;
            objectEntering[i].transform.localScale = new Vector3(0.5f, 0.5f, 1);
            if (i > 2)
                objectEntering[i].transform.position = new Vector3(i - 3 * 1 + 29, -2.5f, 0);
            else
                objectEntering[i].transform.position = new Vector3(i * 1 + 29, -1.5f, 0);
            objectEntering[i].GetComponent<SpriteRenderer>().sortingOrder = 3;
            timeInside[i] = 0;
            return i;
        }
        return -1;
    }
    public void CookSprite(int foodindex)
    {
        int i = GetToCook();
        if(i != -1)
        {
            //objectEntering[i].GetComponent<SpriteRenderer>().sprite = FoodVisuals[foodindex];
        }
    }
    public bool ImOpen() { return open; }
}
