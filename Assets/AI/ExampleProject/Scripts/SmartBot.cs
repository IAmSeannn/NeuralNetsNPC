using UnityEngine;
using System.Collections;
using NeuroNetworkAI;

public class SmartBot : MonoBehaviour 
{
	public enum Conditions {Idle,RunAway,Shoot};
	public Conditions cond = Conditions.Idle;
	public float Health = 100;
	public float RadiusView = 20;
	public float Damage = 25;
	public int NofCloseZombies = 0;
	public float AttackSpeed = 2;
	private float _t = 0;
	public float RunSpeed = 7;
	public GameObject Target;
	public float UpdateTime = 0.1f;
	GameObject[] _zombies;
	public GameObject Laser;
	private LineRenderer _laser;
	
	private Control _control; // 
	
	public string SaveFileFolder;  // example    Assets\DataAI\Study.txt 
	
	void Start () 
	{
	 	_laser = Laser.GetComponent<LineRenderer>();
		DisableLaser ();
		//---------------------------------------
		_control = new Control(SaveFileFolder);  // You create Control
        _control.LoadFromFile();                 // And load Neural Network from file.
		//---------------------------------------
	}
	
	GameObject Closest()
	{
		GameObject res = null;
		float dis = 1000;
		int n = 0;
		_zombies = GameObject.FindGameObjectsWithTag ("Zombi");
		if(_zombies.Length!=0)
		{
			foreach (GameObject item in _zombies) 
			{
				float r = (transform.position-item.transform.position).magnitude;
				if(r<RadiusView)
				{
					n++;
					if(r<dis)
					{
						dis = r;
						res = item;
					}
				}
				
			}
			NofCloseZombies = n;
		}
		return res;
	}
	
	void BotRotation()
	{
		Vector3 pos = Target.transform.position-transform.position;
		pos.Set(pos.x,0,pos.z);
		transform.rotation = Quaternion.LookRotation(pos,Vector3.up);
	}
	
	Vector3 RunAwayDir()
	{
		Vector3 res = Vector3.zero;
		_zombies = GameObject.FindGameObjectsWithTag ("Zombi");
		if(_zombies.Length!=0)
		{
			foreach (GameObject item in _zombies) 
			{
				float r = (transform.position-item.transform.position).magnitude;
				if(r<RadiusView)
				{
					res += (transform.position-item.transform.position).normalized;
				}
				
			}
		}
		return res.normalized;
	}
	
	void BotRotationAway()
	{
		transform.rotation = Quaternion.LookRotation(RunAwayDir(),Vector3.up);
	}
	
	void DoNothing()
	{
		GetComponent<Animation>().CrossFade ("idle");
	}
	
	void RunAway()
	{
		BotRotationAway ();
		if(Vector3.Distance (transform.position,Target.transform.position)<RadiusView)
		{
			GetComponent<Animation>().CrossFade ("run");
			transform.Translate (Vector3.forward*RunSpeed*Time.deltaTime);
		}
	}
	
	void Shoot()
	{
		GetComponent<Animation>().CrossFade ("idle");
		BotRotation ();
		_t += Time.deltaTime;
		if(_t>1f/AttackSpeed)
		{
			Target.GetComponent<Zombi>().Health -= Damage;
			LaserControl (Laser.transform.position,Target.transform.position);
			_t = 0;
		}
	}
	
	void DisableLaser()
	{
		_laser.enabled = false;
	}
	
	public void LaserControl(Vector3 start,Vector3 end)
	{
		_laser.enabled = true;
		_laser.useWorldSpace = true;
		_laser.SetPosition(0,start);
		_laser.SetPosition(1,end);
		if(!IsInvoking("DisableLaser"))
		{
			Invoke("DisableLaser",0.03f);
		}
	}
	
	void MainControl()
	{
		// Note:  You have to normalize your input values
		// In this example i normalized Health and Number of Zombies (i suppose max number of zombies = 20)
        float[] res = _control.Manipulate(new float[]{Health*0.01f,((float)NofCloseZombies)*0.05f}); // It's the main thing you have to use in your code
		// res - array of output values. In this case we have only one output
		
		// Here is a mechanism of changing Bot condition
		if(res[0]<0.3f)
		{
			cond = Conditions.Idle;
		}
		if(res[0]>0.3f && res[0]<0.6f)
		{
			cond = Conditions.RunAway;
		}
		if(res[0]>0.6f)
		{
			cond = Conditions.Shoot;
		}
	}
	
	void CondControl()
	{
		if(Target!=null)
		{
			if(cond == Conditions.Idle)
			{
				DoNothing ();
			}
			if(cond == Conditions.RunAway)
			{
				RunAway ();
			}
			if(cond == Conditions.Shoot)
			{
				Shoot ();
			}
		}
	}
	
	void UpdateTarget()
	{
		Target = Closest ();
	}

	void Update () 
	{
		if(!IsInvoking ("UpdateTarget"))
		{
			Invoke ("UpdateTarget",UpdateTime);
		}
		if(!IsInvoking ("MainControl"))
		{
			Invoke ("MainControl",UpdateTime);
		}
		CondControl ();
		if(Health<=0)
		{
			Destroy (gameObject);
		}
	}
}


