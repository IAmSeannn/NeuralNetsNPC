using UnityEngine;
using System.Collections;

public class Fixable : MonoBehaviour {

    public bool Useable = false;

    public void Activate()
    {
        Useable = true;
        GetComponent<bl_Hud>().Show();
    }

    public void Deactivate()
    {
        Useable = false;
        GetComponent<bl_Hud>().Hide();
    }
}
