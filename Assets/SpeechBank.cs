using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpeechBank : MonoBehaviour {

    private static List<string> LikeStatements = new List<string>();
    private static List<string> DislikeStatements = new List<string>();
    private static List<string> NewTaskStatements = new List<string>();
    private static List<string> TaskCompleteStatements = new List<string>();
    private static List<string> TaskFailedStatements = new List<string>();

    public static string LikeStatement()
    {
        return LikeStatements[Random.Range(0, LikeStatements.Count - 1)];
    }

    public static string DislikeStatement()
    {
        return DislikeStatements[Random.Range(0, DislikeStatements.Count - 1)];
    }

    public static string NewTaskStatement()
    {
        return NewTaskStatements[Random.Range(0, NewTaskStatements.Count - 1)];
    }

    public static string TaskCompleteStatement()
    {
        return TaskCompleteStatements[Random.Range(0, TaskCompleteStatements.Count - 1)];
    }

    public static string TaskFailedStatement()
    {
        return TaskFailedStatements[Random.Range(0, TaskFailedStatements.Count - 1)];
    }

    void Start()
    {
        LikeStatements.Add("I'm quite fond of that ");
        LikeStatements.Add("You know who I like? ");

        DislikeStatements.Add("You know who really gets me mad? ");
        DislikeStatements.Add("Honestly, I don't like ");

        NewTaskStatements.Add("Okay, time to go ");
        NewTaskStatements.Add("I guess I should go ");
        NewTaskStatements.Add("Ahh, time to go ");

        TaskCompleteStatements.Add("Finally finished!");
        TaskCompleteStatements.Add("There we go");

        TaskFailedStatements.Add("Ugh... That didn't go well");
        TaskFailedStatements.Add("Damnit.");
    }
}
