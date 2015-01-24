using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Spawner : MonoBehaviour {


	public GameObject template;
	public GameObject textTemplate;
	public ScoreManager scoreMan;

	public List<Transform> spawnPos= new List<Transform>();

	//Tracking position on a sorted List
	private List<Transform> trackedPosition = new List<Transform> ();
	public float minimumSpacing = 3.0f;

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

	
	private int CompareFunction(Transform c1, Transform c2)

	{
		float x1 = c1.position.x;
		float x2 = c2.position.x;

		if (x1 > x2) {
						return 1;		
				} else if (x1 < x2) {
						return -1;
				} else
						return 0;
	}

	bool isPositionAvailable(Vector3 pos, ref int insertIndex)
	{
		bool canSpawn = false;

		if(pos.x > minXSpawn && pos.x < maxXSpawn)
		{
			if(trackedPosition.Count == 0){
				canSpawn = true;
				Debug.Log("Empty");
				insertIndex = 0;
			}
			else if(trackedPosition.Count == 1)
			{
				if(Mathf.Abs(Mathf.Abs(pos.x) - Mathf.Abs(trackedPosition[0].position.x)) >= minimumSpacing)
				{
					if(pos.x < trackedPosition[0].position.x){
						canSpawn = true;
						insertIndex = 0;
						Debug.Log("Ahead");					
					}
					else{
						canSpawn = true;
						insertIndex = 1;
						Debug.Log("Ahead");
					}
				}
			}
			else{
				for (int i = 0; i < trackedPosition.Count; i++) {
					
					//if i == 0 we just need to check against the first one if is suitable
					if (i == 0)
					{
						if(pos.x < trackedPosition[i].position.x &&
						   Mathf.Abs(Mathf.Abs(pos.x) - Mathf.Abs(trackedPosition[i].position.x)) >= minimumSpacing)
						{
							canSpawn = true;
							insertIndex = i;
							Debug.Log("Ahead");
							return canSpawn;
						}

					}
					// if i == 1 to count - 1 we need just to check if we are in good position
					// and then checking spacing against current item
					// and previous one
					else if(i < trackedPosition.Count)

					{
						if(	pos.x > trackedPosition[i-1].position.x &&
							pos.x < trackedPosition[i].position.x &&
						   	Mathf.Abs(Mathf.Abs(pos.x) - Mathf.Abs(trackedPosition[i - 1].position.x)) >= minimumSpacing &&
						   	Mathf.Abs(Mathf.Abs(pos.x) - Mathf.Abs(trackedPosition[i].position.x)) >= minimumSpacing)
						{
							canSpawn = true;
							insertIndex = i;
							Debug.Log("Insert");
							return canSpawn;
						}

					}
					//It can be after the last position
					if(i == trackedPosition.Count - 1)
					{
						if(pos.x > trackedPosition[i].position.x &&
						   Mathf.Abs(Mathf.Abs(pos.x) - Mathf.Abs(trackedPosition[i].position.x)) >= minimumSpacing)
						{
							Debug.Log("Back");
							canSpawn = true;
 							insertIndex = i + 1;
							return canSpawn;
						}
					}
				} 
			}
		}
		return canSpawn;
	}

	void SpawnKeyObj()
	{
		Vector3 pos = this.transform.position;

		if(spawnPos.Count>0)
		{
			pos=spawnPos[Random.Range(0,spawnPos.Count)].position;
		}

			                 

		//Condition on general spawning with minium space distance
		int insertIndex = -1;

		if (isPositionAvailable(pos, ref insertIndex))
		{
			//Predicting where this posSpawn will make the player		    
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


				trackedPosition.Insert(insertIndex,k.transform);
			//}
			//trackedPosition.Sort(CompareFunction);
			
			//representing the color

		}
	}

	void SpawnInactiveKeyObj()
	{
		Vector3 pos = this.transform.position;
		if(spawnPos.Count>0)
		{
			pos=spawnPos[Random.Range(0,spawnPos.Count)].position;
		}

		//Condition on general spawning with minium space distance
		int insertIndex = -1;

		if (isPositionAvailable (pos, ref insertIndex)) 
		{
			
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

				trackedPosition.Insert(insertIndex,k.transform);

			k.Disable ();

		}

	}

	

	public void DeleteDestroyingObject(Transform tr)
	{
		//Assuming the order list when this function is called from KeyObject.cs we have to delete the first element
		Debug.Log (trackedPosition.Count);
		trackedPosition.Remove (tr);
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
		// x
		if (Input.GetButtonDown ("Slowdown")) {
			currentSpeed_-=0.1f;
		}
		// z
		else if(Input.GetButtonDown("Speedup"))
		{
			currentSpeed_+=0.1f;
			DebugPrint ();
		}
	}

	private void DebugPrint(){
		foreach (var tr in trackedPosition) {
			Debug.Log(tr.position.x);
				}
		Debug.Log("");
	}
}
