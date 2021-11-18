using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IWantToDie : MonoBehaviour
{
    public GameObject Parent;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(gameObject.layer != 7)
        {
            if (collision.gameObject.layer == 3)
            {
                if (collision.gameObject.transform.GetComponent<CustomerController>().DeleteOnCommand(Parent.transform.GetComponent<FoodController>().FoodName))
                {
                    Parent.transform.GetComponent<FoodController>().SumOrder(-1);
                }
                else { Parent.transform.GetComponent<FoodController>().SumQuantity(1); }
            }
            else if (collision.gameObject.layer == 9)
            {
                collision.gameObject.transform.GetComponent<BowlController>().MoveContent(1,
                    Parent.transform.GetComponent<FoodController>().index);
            }
            /*else if (collision.gameObject.layer == 10)
            {
                if (!collision.transform.GetComponent<KilnController>().GetToCook(gameObject))
                    Parent.transform.GetComponent<FoodController>().SumQuantity(1);
            }*/
            else
            {
                Parent.transform.GetComponent<FoodController>().SumQuantity(1);
            }
            Parent.transform.GetComponent<FoodController>().PrintNumbers();
            Destroy(gameObject);
        }
    }
    private void OnMouseDown()
    {
        Destroy(gameObject);
    }
}
