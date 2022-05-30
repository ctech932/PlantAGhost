using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planter : MonoBehaviour
{
    public int planterNumber = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        if (CompareTag("plantOrange"))
        {
            planterNumber = 1;
        }
        else if (CompareTag("plantYellow"))
        {
            planterNumber = 2;
        }
        else if (CompareTag("plantBlue"))
        {
            planterNumber = 3;
        }
        else
        {
            planterNumber = 500;
        }
    }


}
