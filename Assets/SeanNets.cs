using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using NeuroNetworkAI;

public class SeanNets : MonoBehaviour {

    private Control _hungerNet = new Control(Application.dataPath + "/HungerSave.txt");
    private Control _sleepNet = new Control(Application.dataPath + "/SleepSave.txt");
    private Control _funNet = new Control(Application.dataPath+"/FunSave.txt");
    private Control _socialNet = new Control(Application.dataPath + "/SocialSave.txt");

    void Start()
    {
        //set up neural networks
        _hungerNet.LoadFromFile();
        _sleepNet.LoadFromFile();
        _funNet.LoadFromFile();
        _socialNet.LoadFromFile();
    }

	public List<Need> ProcessNeurons(Human h)
    {
        List<Need> tempList = new List<Need>();
        //foreach need, create new need with value being the output of the neural net
        RunAllValuesThroughNets(h, ref tempList);

        //add to a list, potentially check for errors (if over 1, or less than -1, return 0), then return the list
        //CheckForBadValues(tempList);

        return tempList;
    }

    private static void CheckForBadValues(List<Need> tempList)
    {
        foreach (Need n in tempList)
        {
            if (n.Value > 1.0f || n.Value < 0.0f)
            {
                n.Value = Random.Range(1.0f, 0.0f);
            }
        }
    }

    private void RunAllValuesThroughNets(Human h, ref List<Need> tempList)
    {
        foreach (Need n in h.Needs)
        {

            Need temp = new Need();
            temp.Name = n.Name;

            if (n.Name == "Hunger")
            {
                //n.value is between 0-1, convert to -1 to 1
                float[] f = new float[3] { (n.Value * 2) - 1.0f, h.Level, h.Energy };
                Vector v = new Vector(f);
                temp.Value = (_hungerNet.Manipulate(v)[0] + 1.0f) / 2;
            }

            if (n.Name == "Sleep")
            {
                //n.value is between 0-1, convert to -1 to 1
                float[] f = new float[3] { (n.Value * 2) - 1.0f, h.Level, h.Energy };
                Vector v = new Vector(f);
                temp.Value = (_sleepNet.Manipulate(v)[0] + 1.0f) / 2;
            }

            if (n.Name == "Social")
            {
                //n.value is between 0-1, convert to -1 to 1
                float[] f = new float[3] { (n.Value * 2) - 1.0f, h.Level, h.Energy };
                Vector v = new Vector(f);
                temp.Value = (_socialNet.Manipulate(v)[0] + 1.0f) / 2;
            }

            if (n.Name == "Fun")
            {
                //n.value is between 0-1, convert to -1 to 1
                float[] f = new float[3] { (n.Value * 2) - 1.0f, h.Level, h.Energy };
                Vector v = new Vector(f);
                temp.Value = (_funNet.Manipulate(v)[0] + 1.0f) / 2;
            }

            if (n.Name == "Work")
            {
                //no special case for work. work must always happen
                temp.Value = n.Value;
            }

            tempList.Add(temp);
        }
    }
}
