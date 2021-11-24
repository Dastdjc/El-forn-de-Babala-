using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeSpawner : MonoBehaviour
{
    public Sprite[] options;
    public GameObject BeeWithoutVisual;
    private int pointsGroup = 0;
    // Start is called before the first frame update
    void Start()
    {
        //BeeWithoutVisual.GetComponent<Animation>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        if( ScoreManager.comboScore - pointsGroup > 3)
        {
            pointsGroup += 3;
            /*GameObject newBee = Instantiate(BeeWithoutVisual);
            newBee.SetActive(true);
            newBee.GetComponent<SpriteRenderer>().sprite = options[Random.Range(0, 4)];
            newBee.GetComponent<Animation>().Play();*/
            BeeWithoutVisual.GetComponent<SpriteRenderer>().sprite = options[(int)Random.Range(0, 4)];
            BeeWithoutVisual.GetComponent<Animation>().Play();
        }
        else if(ScoreManager.comboScore == 0) { pointsGroup = 0; }
    }
}
