using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IWantToDie : MonoBehaviour
{
    public string FoodName;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
            collision.gameObject.transform.GetComponent<CustomerController>().DeleteOnCommand(FoodName);
        Destroy(gameObject);
    }
}
