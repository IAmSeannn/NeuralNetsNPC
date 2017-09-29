using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {

    public GameObject InfoScreen;
    public GameObject RelationshipsScreen;
    public ScrollableList HumanScrollList;
    public ScrollableList RelationshipScrollList;
    public Text ScoreText;

    void Start()
    {
        CreateContentForHumans();
    }

    void Update()
    {
        ScoreText.text = "Score: "+GameManager.Score.ToString();
    }

    public void OnInfoClick()
    {
        InfoScreen.SetActive(!InfoScreen.activeSelf);
    }

    public void OnRelationshipClick()
    {
        RelationshipsScreen.SetActive(!RelationshipsScreen.activeSelf);
        CreateContentForRelationships();
    }

    void CreateContentForHumans()
    {
        HumanScrollList.itemCount = GameManager.Humans.Count;
        HumanScrollList.CreateList();
    }

    void CreateContentForRelationships()
    {
        //delete all previous items
        foreach(Transform t in RelationshipScrollList.transform)
        {
            Destroy(t);
        }
        if(GameManager.SelectedHuman != null)
        {
            if (GameManager.SelectedHuman.Relationships.Count > 0)
            {
                RelationshipScrollList.itemCount = GameManager.SelectedHuman.Relationships.Count;
                RelationshipScrollList.CreateList();
            }
        }          
    }
}
