using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IWantToDie : MonoBehaviour
{
    public string FoodName;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
            if (!collision.gameObject.transform.GetComponent<CustomerController>().DeleteOnCommand(FoodName))
            {
                string[] names = { "Mona", "Sandwich", "Rosquilletas", "Fartons", "Bunyols" };
                int i = 0;
                while(i < name.Length && names[i] == FoodName) { i++; }
                FoodBar.WriteCommand(i);
            }
        Destroy(gameObject);
    }
}
