using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCustomers : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject NormalCustomer;
    static private GameObject[] Customers = new GameObject[4];
    private int CustomersNumber = -1;
    static private bool ScenePaused;
    
    private int firstPlace = -1;
    private float CoolDown = 0;

    void Start()
    {
        if(CustomersNumber == -1)CustomersNumber = Random.Range(6, 20);
    }

    // Update is called once per frame
    void Update()
    {
        if (!ScenePaused && Time.timeScale == 1 && CustomersNumber > 0)
        {
            CoolDown += Time.deltaTime;
            if (CoolDown > 0.1f && ThereSpace())
            {
                CoolDown = 0;
                int NewC = Random.Range(1, 5);
                if (NewC == 1)
                {
                    CustomersNumber--;
                    Customers[firstPlace] = Instantiate(NormalCustomer, new Vector3(-12 - firstPlace * 4, -1.55f, 0), Quaternion.identity);
                    DontDestroyOnLoad(Customers[firstPlace]);
                }
            }
        }
        else if(CustomersNumber == 0) { Debug.Log("Se ha acabado el día"); Destroy(this); }
    }
    private bool ThereSpace()
    {
        int i = 0;
        while (i < Customers.Length) { if (Customers[i] == null) { firstPlace = i; return true; } i++; }
        firstPlace = -1;
        return false;
    }
    static public void PauseScene(bool isPaused)
    {
        ScenePaused = isPaused; 
        for(int i = 0; i < Customers.Length; i++)
        {
            Customers[i].SetActive(!ScenePaused);
        }
    }
}
