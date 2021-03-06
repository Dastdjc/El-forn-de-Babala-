using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnCustomers : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] NormalCustomer;
    public GameObject[] SpecialCustomer;
    static public GameObject[] Customers;
    public int CustomersNumber = -1;    
    private int firstPlace = -1;
    private float CoolDown = 0;
    static public SpawnCustomers Instance;
    public bool SpawnigSpecial = false;
    static public bool specialSpawned = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        if(CustomersNumber == -1)CustomersNumber = Random.Range(6, 10);
        Customers = new GameObject[3];
        GameManager.Instance.customers = Instance.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 1 && CustomersNumber > 0 && !SpawnigSpecial)
        {
            CoolDown += Time.deltaTime;
            if (CoolDown > 2 && ThereSpace())
            {
                CoolDown = 0;
                int NewC = Random.Range(1, 5);
                if (NewC == 1)
                {
                    int which = Random.Range(0, 5);
                    CustomersNumber--;
                    //Customer[0] es el de m?s a la derecha
                    Customers[firstPlace] = Instantiate(NormalCustomer[which], new Vector3(-84 - firstPlace * 8, -7, 0), Quaternion.identity);
                    Customers[firstPlace].transform.GetChild(Customers[firstPlace].transform.childCount - 1).gameObject.SetActive(true);
                    //Customers[firstPlace].transform.GetChild(Customers[firstPlace].transform.childCount - 1).gameObject.GetComponent<CustomerController>().parent = Customers[firstPlace].transform;
                    Customers[firstPlace].transform.parent = this.transform;
                    /*DontDestroyOnLoad(Customers[firstPlace]);*/
                }
            }
        }
        else if(CustomersNumber == 0) { /*Debug.Log("Se ha acabado el d?a");*/ }
    }
    private bool ThereSpace()
    {
        int i = 0;
        while (i < Customers.Length) { if (Customers[i] == null) { firstPlace = i; return true; } i++; }
        firstPlace = -1;
        return false;
    }
    static public void PauseScene(bool pause)
    {
        if (pause) Time.timeScale = 0;
        else Time.timeScale = 1;
        for(int i = 0; i < Customers.Length; i++)
        {
            Customers[i].SetActive(pause);
        }
    }
    static public int WhichToching()
    {
        int i = 0;
        /*try
        {*/
            while (i < 4)
            {
                if (Customers[i] == null) i++;
                    if (Customers[i] != null && Customers[i].transform.GetChild(Customers[i].transform.childCount-1).GetComponent<CustomerController>().tochingPlayer) break;
 
                    if (Customers[i] != null && Customers[i].transform.GetChild(Customers[i].transform.childCount - 1).GetComponent<SpecialCustomerController>().tochingPlayer) break;
            i++;
            }
        /*}
        catch { Debug.Log(i); i = 4; }*/
        
        return i;
    }
    public void SpawnSpecial(int SpecialID)
    {
        AudioSource BG_music = GameObject.Find("BG_Music").GetComponent<AudioSource>();
        Debug.Log("Spawn especial");
        SpawnigSpecial = true;
        //bool allNull = true;
        //int i = 0;
        //CustomersNumber = 0;
        //while (i < Customers.Length && allNull) { if (Customers[i] != null) allNull = false; i++; }
        //Debug.Log(transform.childCount);
        if (this.transform.childCount-1 == 0)   // Si ya no quedan m?s clientes
        {
            StartCoroutine(AudioFadeOut.FadeOut(BG_music, 2f));
            specialSpawned = true;
            GameManager.Instance.satisfacci?nAcumulada = 0;
            Debug.Log("Special enters...");
            ThereSpace();
            Customers[firstPlace] = Instantiate(SpecialCustomer[SpecialID], new Vector3(-84 - firstPlace * 8, -7, 0), Quaternion.identity);
            Customers[firstPlace].transform.GetChild(Customers[firstPlace].transform.childCount - 1).gameObject.SetActive(true);
            Customers[firstPlace].transform.parent = this.transform;
            //Customers[firstPlace].transform.GetChild(Customers[firstPlace].transform.childCount - 1).gameObject.GetComponent<CustomerController>().parent = Customers[firstPlace].transform;
        }
    }
}
