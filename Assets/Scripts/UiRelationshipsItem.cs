using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UiRelationshipsItem : MonoBehaviour {

    public Relationship Relation;

    public Text Name;
    public Text Value;

	// Update is called once per frame
	void Update () {
        Name.text = Relation.Target.name;
        Value.text = Relation.Value.ToString();
	}
}
