using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeSpawner : MonoBehaviour
{
    public Sprite[] options;
    public GameObject BeeWithoutVisual;
    private int pointsGroup = 0;
    private GameObject aux;
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if( ScoreManager.comboScore - pointsGroup > 2)
        {
            pointsGroup += 2;
            aux = Instantiate(BeeWithoutVisual, this.transform);
            aux.GetComponent<SpriteRenderer>().sprite = options[Random.Range(0, 4)];
        }
        else if(ScoreManager.comboScore == 0) { pointsGroup = 0; }
        //if (FlyingBees.Count > 0) { Destroy(FlyingBees.Dequeue()); }
    }
}
