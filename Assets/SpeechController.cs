using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpeechController : MonoBehaviour {

    public Canvas canvas;
    public Text speechText;
    public Transform Player;
    public static float SpeechLength = 3.0f;
    public static float DistanceToActivate = 5.0f;
    public static int CounterMAX = 500;
    private bool CanTalk = true;
    private int counter = 0;


    void Start()
    {
        canvas.gameObject.SetActive(false);
    }

	public void CreateSpeech(string text)
    {
        speechText.text = text;
        CanTalk = true;
    }

    void Update()
    {
        //if(CanTalk)
        //{
        //    if (Vector3.Distance(gameObject.transform.position, Player.position) <= DistanceToActivate)
        //    {
        //        canvas.gameObject.SetActive(true);
        //        CanTalk = false;
        //        StartCoroutine(DeactivateSpeech());
        //    }
        //}
        //else
        //{
        //    counter++;
        //    if(counter >= CounterMAX)
        //    {
        //        counter = 0;

        //        Relationship r = GetComponent<Human>().Relationships[(int)Random.Range(0, GetComponent<Human>().Relationships.Count-1)];

        //        if (r.Value > 0)
        //        {
        //            speechText.text = SpeechBank.LikeStatement() + r.Target.name;
        //        }
        //        else
        //        {
        //            speechText.text = SpeechBank.DislikeStatement() + r.Target.name;
        //        }

        //        canvas.gameObject.SetActive(true);
        //        StartCoroutine(DeactivateSpeech());
        //    }
        //}    
    }

    IEnumerator DeactivateSpeech()
    {
        yield return new WaitForSeconds(SpeechLength);
        canvas.gameObject.SetActive(false);
    }
}

