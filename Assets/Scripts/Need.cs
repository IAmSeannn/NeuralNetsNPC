using UnityEngine;
using System.Collections;

public class Need {

    public string Name;
    //check what the max is. 
    public float Value;

    void SetUpNeed(string s, float v)
    {
        Name = s;
        Value = v;
    }
}
