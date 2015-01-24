using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {


	public GameObject template;
	public ScoreManager scoreMan;

	public List<Transform> spawnPos= new List<Transform>();

	//Tracking position on a sorted List
	private List<Transform> trackedPosition = new List<Transform> ();
	public float minimumSpacing = 3.0f;

	public float minXSpawn;
	public float maxXSpawn;

	//speed given to the negative x
	float currentSpeed_;
	int currentScore_;
	// Use this for initialization
	void Start () {
		currentSpeed_ = 2.0f;
		currentScore_ = 100;
	}

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

	void SpawnKeyObj()
	{
		Vector3 pos = this.transform.position;
		if(spawnPos.Count>0)
		{
			//Condition on general spawning
			while(true)
			{
				pos=spawnPos[Random.Range(0,spawnPos.Count)].position;
				if(pos.x>minXSpawn&&pos.x<maxXSpawn)
				{
					//Condition on position spacing
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
		Rigidbody2D rg = g.GetComponent<Rigidbody2D> ();
		rg.velocity = new Vector2 (0, Random.Range(0,8));

		trackedPosition.Add(k.transform);
		trackedPosition.Sort(CompareFunction);

		//representing the color
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

	public void DeleteDestroyingObject(Transform tr)
	{
		//Assuming the order list when this function is called from KeyObject.cs we have to delete the first element
		trackedPosition.Remove (tr);
	}

	// Update is called once per frame
	void Update () {
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
