using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeSpawner : MonoBehaviour
{
    public Sprite[] options;
    public GameObject BeeWithoutVisual;
    private Queue<GameObject> FlyingBees;
    private int pointsGroup = 0;
    // Start is called before the first frame update
    void Start()
    {
        //BeeWithoutVisual.GetComponent<Animation>().Play();
        FlyingBees = new Queue<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if( ScoreManager.comboScore - pointsGroup > 2)
        {
            pointsGroup += 2;
            GameObject aux = Instantiate(BeeWithoutVisual);
            aux.transform.parent = BeeWithoutVisual.transform;
            aux.GetComponent<SpriteRenderer>().sprite = options[Random.Range(0, 4)];
            //aux.transform.position = new Vector3(0, 0, 0);
            aux.GetComponent<Animation>().Play();
            Debug.Log(aux != null);
            FlyingBees.Enqueue(aux);
            
        }
        else if(ScoreManager.comboScore == 0) { pointsGroup = 0; }
        if (FlyingBees.Count > 0 && FlyingBees.Peek().transform.position.x < -14) { Destroy(FlyingBees.Dequeue()); }
    }
}
