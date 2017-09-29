using UnityEngine;
using System.Collections;

public class Zombi : MonoBehaviour 
{
	public float Health = 100;
	public float Speed = 3;
	public float AttackRadius = 3;
	public float AttackSpeed = 2;
	public float Damage = 18;
	private float _t = 0;
	public GameObject Bot;
	// Use this for initialization
	void Start () 
	{
		Bot = GameObject.FindGameObjectWithTag ("Player");
	}
	
	GameObject Closest()
	{
		GameObject res = null;
		float dis = 1000;
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
		if(players.Length!=0)
		{
			foreach (GameObject item in players) 
			{
				float r = (transform.position-item.transform.position).magnitude;
				if(r<dis)
				{
					dis = r;
					res = item;
				}
			}
		}
		return res;
	}
	
	void BotRotation()
	{
		Vector3 pos = Bot.transform.position-transform.position;
		pos.Set(pos.x,0,pos.z);
		transform.rotation = Quaternion.LookRotation(pos,Vector3.up);
	}
	
	void FollowBot()
	{
		BotRotation ();
		if(Vector3.Distance (transform.position,Bot.transform.position)>AttackRadius)
		{
			transform.Translate (Vector3.forward*Speed*Time.deltaTime);
		}
	}
	
	void Attack()
	{
		if(Vector3.Distance (transform.position,Bot.transform.position)<AttackRadius)
		{
			_t += Time.deltaTime;
			if(_t> 1f/AttackSpeed)
			{
				Bot.GetComponent<SmartBot>().Health -= Damage;
				_t = 0;
			}
		}
	}
	
	void UpdateTarget()
	{
		Bot = Closest ();
	}
	
	void Update () 
	{
		if(!IsInvoking ("UpdateTarget"))
		{
			Invoke ("UpdateTarget",0.25f);
		}
		if(Bot!=null)
		{
			FollowBot ();
			Attack ();
		}
		if(Health<=0)
		{
			Destroy (gameObject);
		}
	}
}
