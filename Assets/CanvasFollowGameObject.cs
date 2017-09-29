using UnityEngine;
using System.Collections;

public class CanvasFollowGameObject : MonoBehaviour {

    public GameObject ObjectToFollow;
	
	// Update is called once per frame
	void Update () {
        transform.rotation = ObjectToFollow.transform.rotation;
	}
}
