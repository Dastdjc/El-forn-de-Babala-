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
                Parent.transform.GetComponent<FoodController>().SumOrder(-1);
            }
            else { Parent.transform.GetComponent<FoodController>().SumQuantity(1); }
            Parent.transform.GetComponent<FoodController>().PrintNumbers();
        }
        else if (collision.gameObject.layer == 9)
        {
            collision.gameObject.transform.GetComponent<BowlController>().MoveContent(
                Parent.transform.GetComponent<FoodController>().FoodName);
        }
        Destroy(gameObject);
    }
}
