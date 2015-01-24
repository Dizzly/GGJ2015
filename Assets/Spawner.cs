using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {


	public GameObject template;
	public GameObject textTemplate;
	public ScoreManager scoreMan;

	public List<Transform> spawnPos= new List<Transform>();

	public float minXSpawn;
	public float maxXSpawn;

	public float currentSpeed_=2.0f;
	public int currentScore_=100;
	// Use this for initialization
	void Start () {
		spawnChance = baseSpawnChance;
	}

	public float spawnFrequency;
	private float spawnTimer;

	public int baseSpawnChance;
	public int spawnChanceIncrment;
	int spawnChance;



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
		GameObject text=(GameObject)GameObject.Instantiate (textTemplate);


		GameObject canvas = GameObject.FindGameObjectWithTag ("Canvas");

		text.transform.SetParent(canvas.transform,false);
		Mover m = g.GetComponent<Mover> ();
		m.speed_ = currentSpeed_;
		KeyObject k = g.GetComponent<KeyObject> ();
		int key = Random.Range (0, 4);
		k.SetKey (key,false);
		Debug.Log(key);
		k.InitScore (currentScore_, scoreMan);
		TextFollow t = g.GetComponent<TextFollow> ();
		t.Init (text, key);
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
		GameObject text=(GameObject)GameObject.Instantiate (textTemplate);
		
		
		GameObject canvas = GameObject.FindGameObjectWithTag ("Canvas");


		Mover m = g.GetComponent<Mover> ();
		m.speed_ = currentSpeed_;
		KeyObject k = g.GetComponent<KeyObject> ();
		int key = Random.Range (0, 8);
		k.SetKey (key,false);
		k.Disable ();
		TextFollow t = g.GetComponent<TextFollow> ();
		t.Init (text,(int) KeyObject.KEY_REQUIREMENT.KEY_NULL_MAX);
	}

	void TrySpawnObjects()
	{
		int rand = Random.Range (0, 100);
		if (rand < spawnChance) {
						SpawnKeyObj ();
			spawnChance=baseSpawnChance;
				} else {
			spawnChance+=spawnChanceIncrment;		
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (spawnTimer > spawnFrequency) {
						spawnTimer = 0;
						TrySpawnObjects ();
				} else {
			spawnTimer+=Time.deltaTime;		
		}
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
