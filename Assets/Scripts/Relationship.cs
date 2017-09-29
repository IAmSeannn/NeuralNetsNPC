using UnityEngine;
using System.Collections;

public class Relationship {

    public Human Owner;
    public Human Target;

    public float Value = 0;

    public void ChangeValue(float f)
    {
        Value += f;

        if(Value > 1.0f)
        {
            Value = 1.0f;
        }
        if(Value < -1.0f)
        {
            Value = -1.0f;
        }
    }
}
