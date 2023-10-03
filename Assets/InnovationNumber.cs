using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnovationNumber : MonoBehaviour
{
    public static InnovationNumber instance;
    private int innovation_number = 26;

    private void Awake()
    {
        if (instance == null)   
            instance = this;
    }

    public int GetNumber()
    {
        return innovation_number++;
    }
    public int Num()
    {
        return innovation_number;
    }
}
