using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour 
{
	public GameObject Pref;
	public Transform SpawnPoint;
	public float Dispersion;
	public float dt = 10;
	private float _t = 0;
	
	void Start () 
	{
		Application.runInBackground = true;
	}
	
	void Update () 
	{
		_t += Time.deltaTime;
		if(_t>dt)
		{
			float x = SpawnPoint.position.x + Random.Range (-Dispersion,Dispersion);
			float z = SpawnPoint.position.z + Random.Range (-Dispersion,Dispersion);
			Instantiate(Pref,SpawnPoint.position+new Vector3(x,0,z),SpawnPoint.rotation);
			_t = 0;
		}
	}
}
