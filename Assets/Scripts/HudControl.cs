using UnityEngine;
using System.Collections;

public class HudControl : MonoBehaviour {

    private bl_HudManager hudManager;

	// Use this for initialization
	void Start () {
        hudManager = GameObject.FindGameObjectWithTag("HUDSYS").GetComponent<bl_HudManager>();

        foreach(bl_HudInfo h in hudManager.Huds)
        {
           h.Hide = true;
        }
	}
}
