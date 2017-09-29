using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class UiHumanItem : MonoBehaviour {

    public Text WorkNeed;
    public Text HungerNeed;
    public Text FunNeed;
    public Text SocialNeed;
    public Text SleepNeed;
    public Text Task;
    public Text Name;

    public Human human;

    void Update()
    {
        WorkNeed.text = human.WorkNeed.Value.ToString();
        HungerNeed.text = human.HungerNeed.Value.ToString();
        FunNeed.text = human.FunNeed.Value.ToString();
        SocialNeed.text = human.SocialNeed.Value.ToString();
        Task.text = human.CurrentTask.ToString();
        SleepNeed.text = human.SleepNeed.Value.ToString();
        Name.text = human.name;
    }
}
