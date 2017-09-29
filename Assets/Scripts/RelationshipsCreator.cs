using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RelationshipsCreator : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    foreach(Human h in GameManager.Humans)
        {
            CheckForNullRelationships(h);
        }
	}

    private void CheckForNullRelationships(Human h)
    {
        //create new relationships if null
        foreach (Human other in GameManager.Humans)
        {
            if(h != other)
            {
                IEnumerable<Relationship> test =
                    from temp in h.Relationships
                    where temp.Target == other
                    select temp;

                if (test.Count<Relationship>() == 0)
                {
                    Relationship r = new Relationship();
                    r.Owner = h;
                    r.Target = other;
                    r.Value = Random.Range(-1.0f, 1.0f);         
                    h.Relationships.Add(r);
                }
            }
                
        }
    }
}
