using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {


	public GameObject template;
	public ScoreManager scoreMan;

	float currentSpeed_;
	int currentScore_;
	// Use this for initialization
	void Start () {
		currentSpeed_ = 2.0f;
		currentScore_ = 100;
	}

	void SpawnKeyObj()
	{
		GameObject g=(GameObject)GameObject.Instantiate (template,
		                                                 this.transform.position,
		                                                 Quaternion.identity);
		Mover m = g.GetComponent<Mover> ();
		m.speed_ = currentSpeed_;
		KeyObject k = g.GetComponent<KeyObject> ();
		int key = Random.Range (0, 4);
		k.SetKey (key);
		Debug.Log(key);
		k.InitScore (currentScore_, scoreMan);
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
