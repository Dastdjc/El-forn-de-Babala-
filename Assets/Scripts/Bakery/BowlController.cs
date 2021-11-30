using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlController : MonoBehaviour
{
    static private Transform Content;
    static private int[] ingredients = new int[13];
    static private float Upmov = 0.05f;
    void Start()
    {
        if(Content == null)Content = transform.GetChild(0);
        if(Content.position.y > -0.85f)
        {
            for(int i = 0; i < 14; i++)
            {
                for(int j = 0; j < ingredients[i]; j++)
                {
                    Content.position -= new Vector3(0, Upmov, 0);
                }
            }
        }
        ingredients = new int[13];
    }
    static public void MoveContent(int q, int index)
    {
        ingredients[index] += q;
        Content.transform.position += new Vector3(0, Upmov, 0);
    }
    public int DeterminateFood()
    {
        /*
         * 1)Mona
         * 2)Fartons
         * 3)Farinada
         * 4)Bunyols
         * 5)Pilotes de Frare
         * 6)Flaons
         * 7)Coca de llanda
         * 8)Pasteles de boniato
         * 9)Mocadorá
         */
        int[] recipe = { 5, 1, 1, 2, 2, 1 };
        if (IsEnough(recipe)) { return 0; }
        recipe = new int[]{ 5, 2, 0, 0, 2, 3, 2, 2 };
        if (IsEnough(recipe)) { return 1; }
        recipe = new int[] { 5, 1, 0, 0, 3, 3, 2, 2, 1, 1 };
        if (IsEnough(recipe)) { return 2; }
        recipe = new int[] { 5, 0, 2, 0, 2, 2, 0, 0, 1, 0, 0, 0, 1 };
        if (IsEnough(recipe)) { return 3; }
        recipe = new int[] { 5, 2, 4, 0, 2, 1, 2, 0, 1 };
        if (IsEnough(recipe)) { return 4; }
        recipe = new int[] { 5, 0, 0, 0, 2, 2, 4, 0, 0, 4, 2 };
        if (IsEnough(recipe)) { return 5; }
        recipe = new int[] { 5, 0, 8, 0, 6, 5, 1, 0, 1 };
        if (IsEnough(recipe)) { return 6; }
        recipe = new int[] { 0, 0, 0, 0, 3, 1, 2, 0, 0, 0, 0, 4 };
        if (IsEnough(recipe)) { return 7; }
        recipe = new int[] { 0, 0, 0, 0, 6, 1, 0, 2, 1, 6, 6 };
        if (IsEnough(recipe)) { return 8; }
        return -1;
    }
    private bool IsEnough(int[] q) 
    {
        bool isOnbowl = true;
        int i = 0;
        while(i < q.Length && isOnbowl)
        {
            if (ingredients[i] < q[i]) isOnbowl = false;
            i++;
        }
        return isOnbowl; 
    }
    public void Resset() { Start(); }
}
