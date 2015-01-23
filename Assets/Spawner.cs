using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {


	public GameObject template;

	float currentSpeed_;
	// Use this for initialization
	void Start () {
	
	}

	void SpawnKeyObj()
	{
		GameObject g=(GameObject)GameObject.Instantiate (template,
		                                                 this.transform.position,
		                                                 Quaternion.identity);
		Mover m = g.GetComponent<Mover> ();
		m.speed_ = 2;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Debug")) {
			SpawnKeyObj();		
		}
	}
}
