using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Building : MonoBehaviour {

    public bool Work;
    public bool Fun;
    public bool Hunger;
    public bool Social;
    public bool Sleep;

    public float ActivityLength = 5.0f;

    public List<Target> Targets = new List<Target>();

	// Use this for initialization
	void Start ()
    {
        GetTargetLocations();
    }

    private void GetTargetLocations()
    {
        foreach (Transform t in transform)
        {
            if (t.name == "Target")
            {
                Targets.Add(t.GetComponent<Target>());
            }
        }
    }

    public void Task(Human h)
    {
        UpdateNeeds(h);
    }

    private void UpdateNeeds(Human h)
    {
        //if num is greater than 0.1
        if (Random.Range(0.0f, 1.0f) > 0.2f+h.GetCurrentRelationshipsModifier())
        {
            h.GetComponent<SpeechController>().CreateSpeech(SpeechBank.TaskCompleteStatement());
            if (Work)
            {
                h.WorkNeed.Value += GameManager.TaskGain;
                h.UpdateLevel(Random.Range(0.2f, -0.2f));
                h.UpdateEnergy(Random.Range(0.2f, -0.2f));
            }
            if (Fun)
            {
                h.FunNeed.Value += GameManager.TaskGain;
                h.UpdateLevel(Random.Range(0.2f, -0.2f));
                h.UpdateEnergy(Random.Range(0.2f, -0.2f));
            }
            if (Hunger)
            {
                h.HungerNeed.Value += GameManager.TaskGain;
                h.UpdateLevel(Random.Range(0.2f, -0.2f));
                h.UpdateEnergy(Random.Range(0.2f, -0.2f));
            }
            if (Social)
            {
                h.SocialNeed.Value += GameManager.TaskGain;
                h.UpdateLevel(Random.Range(0.2f, -0.2f));
                h.UpdateEnergy(Random.Range(0.2f, -0.2f));
            }
            if (Sleep)
            {
                h.SleepNeed.Value += GameManager.TaskGain;
                h.UpdateLevel(Random.Range(0.2f, -0.2f));
                h.UpdateEnergy(Random.Range(0.2f, -0.2f));
            }
        }
        else
        {
            h.GetComponent<SpeechController>().CreateSpeech(SpeechBank.TaskFailedStatement());
            Debug.Log(h.name + " just failed to do task");
        }
    }

    public bool IsFreeSpaces()
    {
        foreach(Target t in Targets)
        {
            if(!t.Occupied)
            {
                return true;
            }
        }
        return false;
    }

    public List<Human> GetOccupants()
    {
        List<Human> temp = new List<Human>();

        foreach(Target t in Targets)
        {
            if(t.OccupiedBy != null)
            {
                temp.Add(t.OccupiedBy);
            }
        }

        return temp;
    }

}
