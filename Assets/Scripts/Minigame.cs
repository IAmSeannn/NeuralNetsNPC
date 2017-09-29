using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Minigame : MonoBehaviour {

    public Text Display;
    public Text Hint;
    public Button Red;
    public Button Green;
    public Button Blue;

    public int Difficulty = 3;
    public Player player;

    private List<string> CurrentSolution = new List<string>();
    private List<string> CurrentAttempt = new List<string>();

    //0 = non, 1 = showing, 2 = input
    private int Mode = 0;

    void Start()
    {
        StartCoroutine(StartMinigame());
    }

    void CreateRandomPattern()
    {
        CurrentSolution.Clear();

        for (int i = 0; i < Difficulty; i++)
        {
            float f = Random.Range(0.0f, 1.0f);

            if(f <= 0.33f)
            {
                CurrentSolution.Add("Red");
            }
            else if(f <= 0.66f)
            {
                CurrentSolution.Add("Blue");
            }
            else
            {
                CurrentSolution.Add("Green");
            }
        }
    }

    IEnumerator DisplayPattern()
    {
        CreateRandomPattern();
        Mode = 1;
        Hint.text = "The pattern is:";
        yield return new WaitForSeconds(1);

        for (int i = 0; i < CurrentSolution.Count; i++)
        {
            Display.text = CurrentSolution[i];

            yield return new WaitForSeconds(1);

            Display.text = "";

            yield return new WaitForSeconds(0.1f);
        }

        Mode = 2;
        CurrentAttempt.Clear();
        Hint.text = "Input the pattern now";
        Red.gameObject.SetActive(true);
        Green.gameObject.SetActive(true);
        Blue.gameObject.SetActive(true);

    }

    IEnumerator StartMinigame()
    {
        Hint.text = "Get Ready...";

        yield return new WaitForSeconds(3);

        StartCoroutine(DisplayPattern());
    }

    public void RedClicked()
    {
        if(Mode == 2)
        {
            Display.text = "Clicked Red";
            CurrentAttempt.Add("Red");
            CheckAnswer();
        }
    }

    public void GreenClicked()
    {
        if (Mode == 2)
        {
            Display.text = "Clicked Green";
            CurrentAttempt.Add("Green");
            CheckAnswer();
        }
    }

    public void BlueClicked()
    {
        if (Mode == 2)
        {
            Display.text = "Clicked Blue";
            CurrentAttempt.Add("Blue");
            CheckAnswer();
        }
    }

    void CheckAnswer()
    {
        for(int i = 0; i < CurrentAttempt.Count; i++)
        {
            if(CurrentAttempt[i] != CurrentSolution[i])
            {
                StartCoroutine(Lose());
            }
        }

        if(CurrentSolution.Count == CurrentAttempt.Count)
        {
            StartCoroutine(Win());
        }
    }

    IEnumerator Win()
    {
        Mode = 0;
        Hint.text = "Complete";
        Display.text = "";

        yield return new WaitForSeconds(1);

        if(player != null)
        {
            player.ReturnControl();
        }

        Destroy(gameObject);
    }

    IEnumerator Lose()
    {
        Mode = 0;
        Hint.text = "Failed. Please try again";
        Display.text = "";
        Red.gameObject.SetActive(false);
        Green.gameObject.SetActive(false);
        Blue.gameObject.SetActive(false);

        yield return new WaitForSeconds(2);


        StartCoroutine(StartMinigame());
    }
}
