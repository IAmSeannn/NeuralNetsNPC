using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class Human : MonoBehaviour {

    public Need WorkNeed = new Need();
    public Need FunNeed = new Need();
    public Need HungerNeed = new Need();
    public Need SocialNeed = new Need();
    public Need SleepNeed = new Need();

    public float Level;
    public float Energy;

    public List<Need> Needs = new List<Need>();
    public List<Relationship> Relationships = new List<Relationship>();
    private List<Relationship> CurrentRelationships = new List<Relationship>();
    
    public float DeclineRate = 0.005f;
    public string CurrentTask;

    public Target HomeLocation;
    public Target WorkLocation;

    public SeanNets Nets;

    private Building CurrentTarget;
    private Target OccupiedSpace;
    private float ReasonableDistanceToInteract = 1.0f;
    private bool IsInTask = false;

	// Use this for initialization
	void Start ()
    {
        GameManager.Humans.Add(this);

        Level = Random.Range(-1.0f, 1.0f);
        Energy = Random.Range(-1.0f, 1.0f);

        RandomizeNeeds();

        //add needs to list
        Needs.Add(WorkNeed);
        Needs.Add(FunNeed);
        Needs.Add(HungerNeed);
        Needs.Add(SocialNeed);
        Needs.Add(SleepNeed);

        ChooseTask();
        StartCoroutine(DeclineNeeds());
    }

    private void RandomizeNeeds()
    {
        WorkNeed.Value = Random.Range(0.0f, 1.0f);
        WorkNeed.Name = "Work";
        FunNeed.Value = Random.Range(0.0f, 1.0f);
        FunNeed.Name = "Fun";
        HungerNeed.Value = Random.Range(0.0f, 1.0f);
        HungerNeed.Name = "Hunger";
        SocialNeed.Value = Random.Range(0.0f, 1.0f);
        SocialNeed.Name = "Social";
        SleepNeed.Value = Random.Range(0.0f, 1.0f);
        SleepNeed.Name = "Sleep";
    }

    void ChooseTask()
    {
        List<Need> SortedList = Needs.OrderBy(o => o.Value).ToList<Need>();
        //List<Need> SortedList = Nets.ProcessNeurons(this).OrderBy(o => o.Value).ToList<Need>();

        if (CheckIfTaskPossible(SortedList[0].Name))
        {
            StartTask(SortedList[0].Name);
        }
        else if (CheckIfTaskPossible(SortedList[1].Name))
        {
            StartTask(SortedList[1].Name);
        }
        else if (CheckIfTaskPossible(SortedList[2].Name))
        {
            StartTask(SortedList[2].Name);
        }
        else if (CheckIfTaskPossible(SortedList[3].Name))
        {
            StartTask(SortedList[3].Name);
        }
        else if (CheckIfTaskPossible(SortedList[4].Name))
        {
            StartTask(SortedList[4].Name);
        }
    }

    private bool CheckIfTaskPossible(string s)
    {
        Building b = GameManager.GetBuildingOfType(s, transform.position);

        if(b == null)
        {
            return false;
        }

        if(GetFreeSpaceInBuilding(b, false) != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void StartTask(string s)
    {
        CurrentTask = s;
        Target tempTar = null;
        if (s == "Work")
        {
            tempTar = WorkLocation;
            CurrentTarget = tempTar.GetComponentInParent<Building>();
            OccupiedSpace = tempTar;
        }
        else if(s == "Sleep")
        {
            tempTar = HomeLocation;
            CurrentTarget = tempTar.GetComponentInParent<Building>();
            OccupiedSpace = tempTar;
        }
        else
        {
            CurrentTarget = GameManager.GetBuildingOfType(s, transform.position);
            tempTar = GetFreeSpaceInBuilding(CurrentTarget, true);
        }

        GetComponent<SpeechController>().CreateSpeech(SpeechBank.NewTaskStatement()+ s);
              
        StartCoroutine(MoveToLocation(tempTar.transform.position));
    }

    private Target GetFreeSpaceInBuilding(Building b, bool act)
    {
        //choose target and set occupied
        foreach (Target t in b.Targets)
        {
            if (!t.Occupied)
            {
                if(act)
                {
                    t.Occupied = true;
                    t.OccupiedBy = this;
                    OccupiedSpace = t;
                }
                return t;
            }
        }
        return null;
    }

    IEnumerator MoveToLocation(Vector3 v)
    {
        GetComponent<NavMeshAgent>().destination = v;

        while(Vector3.Distance(transform.position, v) > ReasonableDistanceToInteract)
        {
            yield return new WaitForSeconds(0.2f);
        }

        StartCoroutine(CompleteTask());
    }

    IEnumerator CompleteTask()
    {
        IsInTask = true;

        if(CurrentTarget.Social)
        {
            StartCoroutine(SocialUpdates(0.02f));
        }
        else if (!CurrentTarget.Sleep)
        {
            StartCoroutine(SocialUpdates(0.01f));
        }
            
        yield return new WaitForSeconds(CurrentTarget.ActivityLength);
        IsInTask = false;
        CurrentTarget.Task(this);
        OccupiedSpace.Occupied = false;
        OccupiedSpace.OccupiedBy = null;
        CurrentRelationships.Clear();
        ClampAllNeeds();
        ChooseTask();
    }

    IEnumerator SocialUpdates(float gain)
    {

        while(IsInTask)
        {
            //do social check
            CheckForNullRelationships();
            RandomChanceToRelationships(gain);
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator DeclineNeeds()
    {
        while(GameManager.Simulate)
        {
            yield return new WaitForSeconds(1.0f);

            WorkNeed.Value -= DeclineRate;
            FunNeed.Value -= DeclineRate;
            HungerNeed.Value -= DeclineRate;
            SocialNeed.Value -= DeclineRate;
            SleepNeed.Value -= DeclineRate;

            ClampAllNeeds();
        }
    }

    private void ClampAllNeeds()
    {
        ClampNeed(ref WorkNeed.Value);
        ClampNeed(ref FunNeed.Value);
        ClampNeed(ref HungerNeed.Value);
        ClampNeed(ref SocialNeed.Value);
        ClampNeed(ref SleepNeed.Value);
    }

    private void ClampNeed(ref float f)
    {
        if (f > 1.0f)
            f = 1.0f;
        if (f < 0.0f)
            f = 0.0f;
    }

    IEnumerator Wait()
    {
        GetComponent<NavMeshAgent>().destination = new Vector3(80, 0.5f, 89);
        yield return new WaitForSeconds(1.0f);
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.X))
        {
            foreach(Need n in Needs)
            {
                Debug.Log(n.Name + " : " + n.Value);
            }
        }
    }

    void OnMouseDown()
    {
        GameManager.SelectedHuman = this;
    }

    private void RandomChanceToRelationships(float gain)
    {
        foreach (Human other in CurrentTarget.GetOccupants())
        {
            if(other != this)
            {
                if(other.IsInTask)
                {
                    //finds relationship between h and other
                    var r =
                    from temp in Relationships
                    where temp.Target == other
                    select temp;

                    //randomly changes relationship
                    float f = Random.Range(0.0f, 1.0f);
                    if (f < 0.5f)
                    {
                        r.ElementAt<Relationship>(0).ChangeValue(gain * -1);
                        GetComponent<SpeechController>().CreateSpeech(SpeechBank.DislikeStatement() + r.ElementAt<Relationship>(0).Target.name);
                    }
                    else
                    {
                        r.ElementAt<Relationship>(0).ChangeValue(gain);
                        GetComponent<SpeechController>().CreateSpeech(SpeechBank.LikeStatement() + r.ElementAt<Relationship>(0).Target.name);
                    }

                    //add relationship to current ones during this action
                    if (!CurrentRelationships.Contains(r.ElementAt<Relationship>(0)))
                    {
                        CurrentRelationships.Add(r.ElementAt<Relationship>(0));
                    }
                } 
            }  
        }
    }

    private void CheckForNullRelationships()
    {
        //create new relationships if null
        foreach (Human other in CurrentTarget.GetOccupants())
        {
            IEnumerable<Relationship> test =
                from temp in Relationships
                where temp.Target == other
                select temp;

            if (test.Count<Relationship>() == 0)
            {   
                if(other != this)
                {
                    Relationship r = new Relationship();
                    r.Owner = this;
                    r.Target = other;
                    Relationships.Add(r);
                } 
            }
        }
    }

    public float GetCurrentRelationshipsModifier()
    {
        //returns a sum of the relationships, with bad ones adding to the value, and good ones subtracting
        float f = 0;
        foreach(Relationship r in CurrentRelationships)
        {
            if(r.Value < 0)
            {
                f += 0.05f;
            }
            else
            {
                f -= 0.05f;
            }
        }

        return f;
    }

    public void UpdateEnergy(float f)
    {
        Energy += f;
        if (Energy > 1.0f)
            Energy = 1.0f;

        if (Energy < -1.0f)
            Energy = -1.0f;
    }

    public void UpdateLevel(float f)
    {
        Level += f;
        if (Level > 1.0f)
            Level = 1.0f;

        if (Level < -1.0f)
            Level = -1.0f;
    }
}
