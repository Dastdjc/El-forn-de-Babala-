using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCustomers : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject NormalCustomer;
    static public bool[] positions { get; set; }
    private int CustomersNumber;
    
    private int firstPlace = -1;
    private float CoolDown = 0;

    void Start()
    {
        CustomersNumber = Random.Range(6, 20);
        positions = new bool[4];
        for(int i = 0; i < positions.Length; i++) { positions[i] = false; }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 1 && CustomersNumber > 0)
        {
            CoolDown += Time.deltaTime;
            if (CoolDown > 3 && ThereSpace())
            {
                CoolDown = 0;
                int NewC = Random.Range(0, 3);
                if (NewC == 1)
                {
                    CustomersNumber--;
                    Instantiate(NormalCustomer, new Vector3(-(firstPlace * 2 + 12), -3, 0), Quaternion.identity);
                    positions[firstPlace] = true;
                }
            }
        }
        else if(CustomersNumber == 0) { Debug.Log("Se ha acabado el día"); Destroy(this); }
    }
    public bool ThereSpace()
    {
        int i = 0;
        while(i < positions.Length && positions[i]) { i++; }
        if (i < positions.Length)
        {
            firstPlace = i;
            return true;
        }
        return false;
    }
}
