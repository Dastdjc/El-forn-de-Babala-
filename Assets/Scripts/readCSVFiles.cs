using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class readCSVFiles : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void ReadCSV()
    {
        StreamReader strReader = new StreamReader(path);
        bool endOfFile = false;
        while (!endOfFile)
        {
            string data_String = strReader.ReadLine();
            if (data_String == null)
            {
                endOfFile = true;
                break;
            }
        }
    }
}
