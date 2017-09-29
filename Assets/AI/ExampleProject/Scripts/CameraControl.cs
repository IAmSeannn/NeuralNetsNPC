using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour 
{
	private GameObject[] _bots;
	private int _currentBot = 0;
	// Use this for initialization
	void Start () 
	{
		_bots = GameObject.FindGameObjectsWithTag ("Player");
	}
	
	void UpdateBots()
	{
		_bots = GameObject.FindGameObjectsWithTag ("Player");
	}
	
	void SwitchBot()
	{
		if(_bots.Length!=0)
		{
			if(Input.GetKeyDown(KeyCode.Mouse0))
			{
				if(_currentBot<_bots.Length-1)
				{
					_currentBot++;
				}
				else
				{
					_currentBot = 0;
				}
				//gameObject.GetComponent<MouseOrbit>().target = _bots[_currentBot].transform;
			}
			if(Input.GetKeyDown(KeyCode.Mouse1))
			{
				if(_currentBot>0)
				{
					_currentBot--;
				}
				else
				{
					_currentBot = _bots.Length-1;
				}
				//gameObject.GetComponent<MouseOrbit>().target = _bots[_currentBot].transform;
			}
		}
	}

	void Update () 
	{
		if(!IsInvoking ("UpdateBots"))
		{
			Invoke ("UpdateBots",1f);
		}
		SwitchBot ();
	}
}
