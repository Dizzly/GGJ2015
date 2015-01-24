using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {


	public GameObject template;
	public ScoreManager scoreMan;

	public List<Transform> spawnPos= new List<Transform>();

	public float minXSpawn;
	public float maxXSpawn;

	float currentSpeed_;
	int currentScore_;
	// Use this for initialization
	void Start () {
		currentSpeed_ = 2.0f;
		currentScore_ = 100;
	}

	void SpawnKeyObj()
	{
		Vector3 pos = this.transform.position;
		if(spawnPos.Count>0)
		{
			while(true)
			{
				pos=spawnPos[Random.Range(0,spawnPos.Count)].position;
				if(pos.x>minXSpawn&&pos.x<maxXSpawn)
				{
					break;
				}
			}
		}

			                 
		GameObject g=(GameObject)GameObject.Instantiate (template,
		                                                 pos,
		                                                 Quaternion.identity);
		Mover m = g.GetComponent<Mover> ();
		m.speed_ = currentSpeed_;
		KeyObject k = g.GetComponent<KeyObject> ();
		int key = Random.Range (0, 4);
		k.SetKey (key);
		Debug.Log(key);
		k.InitScore (currentScore_, scoreMan);
	}

	void SpawnInactiveKeyObj()
	{
		Vector3 pos = this.transform.position;
		if(spawnPos.Count>0)
		{
			while(true)
			{
				pos=spawnPos[Random.Range(0,spawnPos.Count)].position;
				if(pos.x>minXSpawn)
				{
					break;
				}
			}
		}
		
		
		GameObject g=(GameObject)GameObject.Instantiate (template,
		                                                 pos,
		                                                 Quaternion.identity);
		Mover m = g.GetComponent<Mover> ();
		m.speed_ = currentSpeed_;
		KeyObject k = g.GetComponent<KeyObject> ();
		int key = Random.Range (0, 8);
		k.SetKey (key);
		k.Disable ();

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Debug")) {
			SpawnKeyObj();		
		}
		if (Input.GetButtonDown ("Slowdown")) {
			currentSpeed_-=0.1f;
		}
		else if(Input.GetButtonDown("Speedup"))
		{
			currentSpeed_+=0.1f;
		}
	}
}
