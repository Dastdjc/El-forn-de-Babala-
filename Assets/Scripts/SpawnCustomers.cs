using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCustomers : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject NormalCustomer;
    static public int daylySatisfaction;
    private int CustomersNumber;
    void Start()
    {
        CustomersNumber = Random.Range(0, 10);
    }

    // Update is called once per frame
    void Update()
    {
        int NewC = Random.Range(0, 100);
        if (NewC == 1 && CustomersNumber > 1)
        {
            NewC = 0;
            CustomersNumber--;
            Instantiate(NormalCustomer);
        }
        if (CustomersNumber == 0) { Debug.Log("Ha acabado el día"); }
    }
}
