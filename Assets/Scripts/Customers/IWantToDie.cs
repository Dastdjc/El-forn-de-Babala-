using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IWantToDie : MonoBehaviour
{
    public GameObject Parent;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            if (collision.gameObject.transform.GetComponent<CustomerController>().DeleteOnCommand(Parent.transform.GetComponent<FoodController>().FoodName))
            {
                Parent.transform.GetComponent<FoodController>().ordered--;
            }
            else { Parent.transform.GetComponent<FoodController>().quantity++; }
            Parent.transform.GetComponent<FoodController>().PrintNumbers();
            Destroy(gameObject);
        }
        
    }
}
