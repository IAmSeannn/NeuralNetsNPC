using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public static List<Building> Buildings = new List<Building>();
    public static List<Human> Humans = new List<Human>();
    public List<Fixable> Fixables = new List<Fixable>();

    public static float TaskGain = 0.2f;
    public static bool Simulate = true;
    public static Human SelectedHuman;
    public static int Score = 0;

    void Start()
    {
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("Building"))
        {
            Buildings.Add(g.GetComponent<Building>());
        }

        foreach(Fixable f in Fixables)
        {
            f.gameObject.GetComponent<bl_Hud>().Hide();
            f.gameObject.GetComponent<bl_Hud>().HudInfo.ShowDynamically = false;
        }

        ActivateRandomFixable();
    }

    public static Building GetBuildingOfType(string s, Vector3 v)
    {
        List<Building> list = new List<Building>();

        if(s == "Work")
        {
            foreach(Building b in Buildings)
            {
                if(b.Work)
                {
                         list.Add(b);
                }
            }
        }

        if (s == "Fun")
        {
            foreach (Building b in Buildings)
            {
                if (b.Fun)
                {
                    list.Add(b);
                }
            }
        }

        if (s == "Hunger")
        {
            foreach (Building b in Buildings)
            {
                if (b.Hunger)
                {
                    list.Add(b);
                }
            }
        }

        if (s == "Social")
        {
            foreach (Building b in Buildings)
            {
                if (b.Social)
                {
                    list.Add(b);
                }
            }
        }

        if (s == "Sleep")
        {
            foreach (Building b in Buildings)
            {
                if (b.Sleep)
                {
                    list.Add(b);
                }
            }
        }

        float dis = 0;
        Building best = null;

        foreach(Building b in list)
        {
            if(dis == 0)
            {   
                if(b.IsFreeSpaces())
                {
                    dis = Vector3.Distance(v, b.transform.position);
                    best = b;
                }
                    
            }
            else if(Vector3.Distance(v, b.transform.position) < dis)
            {
                if (b.IsFreeSpaces())
                {
                    dis = Vector3.Distance(v, b.transform.position);
                    best = b;
                }
            }
        }
        return best;
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.N))
        {
            Debug.Log("Name : " + SelectedHuman.name);
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            Debug.Log("Relationship Count : " + SelectedHuman.Relationships.Count);
        }

        if(Score >= 10)
        {
            Application.LoadLevel("ThanksForPlaying");
        }
    }

    public void ActivateRandomFixable()
    {
        int i = Random.Range(0, Fixables.Count);
        Fixables[i].Activate();
    }
}
